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
            GameWriter.PrintDeckAndPileStatus(_dealer, _turnCount);
            Player currentPlayer = _players[_currentPlayerIndex];

            if (outOfCards())
            {
                _gameIsOver = true;
                break;
            }

            _dealer.RecyclePileIntoDeck();
            PlayersVieForTopDiscard(_currentPlayerIndex, currentPlayer);
            PlayerDraws(currentPlayer);
            PlayerDiscards(currentPlayer);

            _currentPlayerIndex = RotateToNextPlayer(_currentPlayerIndex);
        }
    }

    private void PlayersVieForTopDiscard(int currentPlayerIndex, Player currentPlayer)
    {
        int playerCount = _players.Count;
        
        // Normalize the startIndex within the range of the list size
        int startIndex = (currentPlayerIndex % playerCount + playerCount) % playerCount;
        
        for (int i = startIndex; i < startIndex + playerCount; i++)
        {
            int index = i % playerCount;
            Player vyingPlayer = _players[index];
            bool playerDecision = vyingPlayer.DecideWhetherToTakePenalty();
            if (playerDecision)
            {
                Card drawnCard = _dealer.GiveCardFrom(DrawSource.Pile);
                Card penalty = null;
                vyingPlayer.AddToHand(drawnCard);

                if (vyingPlayer != currentPlayer)
                {
                    // take penalty
                    penalty = _dealer.GiveCardFrom(DrawSource.Deck);
                    vyingPlayer.AddToHand(penalty);
                }

                _gameWriter.PrintPenaltyAction(vyingPlayer, drawnCard, penalty);
                
                return;
            }
        }
    }

    private bool outOfCards()
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

    private void PlayerDraws(Player player)
    {
        _gameWriter.TurnStart(player, _turnCount);

        DrawSource drawSource = player.ChooseDrawSource(_dealer.TopDiscard != null);
        Card drawnCard = _dealer.GiveCardFrom(drawSource);
        player.AddToHand(drawnCard);
        
        _gameWriter.DrawAction(player, drawSource, drawnCard);
    }
    
    private void PlayerDiscards(Player player)
    {
        Card discard = player.DiscardFromHand();
        _dealer.TakeDiscard(discard);
        _gameWriter.DiscardAction(player, discard);
    }

    private void SetupAndDeal()
    {
        // Setup
        _players = new PlayerStub().CreatePlayers();
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