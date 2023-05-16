using System;
using System.Collections.Generic;

public class GameController
{
    private Dealer _dealer;
    private List<Player> _players;
    private bool _gameIsOver;
    private int _turnCount;
    private int _currentPlayerIndex;
    private readonly int _turnLimit;
    private readonly GameWriter _gameWriter;

    public GameController(int turnLimit = 100)
    {
        _turnLimit = turnLimit;
        _gameWriter = new GameWriter();

        SetupAndDeal();
    }

    public void Play()
    {
        while (ShouldContinuePlaying())
        {
            Player currentPlayer = _players[_currentPlayerIndex];

            GameWriter.PrintDeckAndPileStatus(_dealer, _turnCount, _players);
            GameWriter.PrintTurnStart(currentPlayer, _turnCount);

            if (OutOfCards())
            {
                _gameIsOver = true;
                break;
            }

            _dealer.RecyclePileIntoDeck();

            Player playerWhoDrew = null;
            PlayersVieForTopDiscard(_currentPlayerIndex, currentPlayer, ref playerWhoDrew);
            PlayerDraws(currentPlayer, playerWhoDrew);
            PlayerDiscards(currentPlayer);

            _currentPlayerIndex = RotateToNextPlayer(_currentPlayerIndex);
        }
    }

    private void PlayersVieForTopDiscard(
        int currentPlayerIndex,
        Player currentPlayer,
        ref Player playerWhoDrew)
    {
        int playerCount = _players.Count;

        // Normalize the startIndex within the range of the list size
        int startIndex = (currentPlayerIndex % playerCount + playerCount) % playerCount;

        for (int i = startIndex; i < startIndex + playerCount; i++)
        {
            int index = i % playerCount;
            Player vyingPlayer = _players[index];
            bool playerDecision = Player.DecideWhetherToTakePenalty();
            if (playerDecision)
            {
                _dealer.RecyclePileIntoDeck();

                playerWhoDrew = vyingPlayer;
                Card drawnCard = _dealer.GiveCardFrom(DrawSource.Pile);
                Card penalty = null;
                vyingPlayer.AddToHand(drawnCard);

                if (vyingPlayer != currentPlayer)
                {
                    // take penalty
                    penalty = _dealer.GiveCardFrom(DrawSource.Deck);
                    vyingPlayer.AddToHand(penalty);
                }

                GameWriter.PrintPenaltyAction(vyingPlayer, drawnCard, penalty);

                return;
            }
            Console.WriteLine($"{vyingPlayer.Name} passes on pile");
        }
    }

    private bool OutOfCards()
    {
        return _dealer.DeckCardCount() == 0 && _dealer.PileCardCount() == 0;
    }

    private bool ShouldContinuePlaying()
    {
        if (_turnCount == _turnLimit)
        {
            _gameIsOver = true;
        }

        return !_gameIsOver;
    }

    private int RotateToNextPlayer(int currentPlayerIndex)
    {
        _turnCount++;
        currentPlayerIndex = (currentPlayerIndex == _players.Count - 1) ? 0 : (currentPlayerIndex + 1);
        return currentPlayerIndex;
    }

    private void PlayerDraws(Player player, Player playerWhoDrew)
    {
        // GameWriter.PrintPlayerDrawsTopCard(playerWhoDrew);
        
        if (playerWhoDrew != player)
        {
            _dealer.RecyclePileIntoDeck();
            Card drawnCard = _dealer.GiveCardFrom(DrawSource.Deck);
            player.AddToHand(drawnCard);
            GameWriter.PrintDrawAction(player, DrawSource.Deck, drawnCard);
        }
    }

    private void PlayerDiscards(Player player)
    {
        Card discard = player.DiscardFromHand();
        _dealer.ReceiveDiscardFromPlayer(discard);
    }

    private void SetupAndDeal()
    {
        // Setup
        _players = new PlayerStub().CreatePlayers(_gameWriter);
        _dealer = new Dealer(new Deck(), _players);

        _turnCount = 1;
        _currentPlayerIndex = 0;
        _gameIsOver = false;

        // Deal
        _dealer.Deal();
    }

    public bool IsFinished()
    {
        return _gameIsOver;
    }

    public int TurnCount()
    {
        return _turnCount;
    }
}