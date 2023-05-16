using System;
using System.Collections.Generic;

public class GameController
{
    private Dealer _dealer;
    private List<Player> _players;
    private bool _gameIsOver;
    private readonly bool _drawChoiceEnabled;
    private int _turnCount;
    private int _currentPlayerIndex;
    private int _turnLimit;
    private GameWriter _gameWriter;

    public GameController(bool drawChoiceEnabled = false, int turnLimit = 100)
    {
        _drawChoiceEnabled = drawChoiceEnabled;
        _turnLimit = turnLimit;
        _gameWriter = new GameWriter();

        SetupAndDeal();
    }

    public void Play()
    {
        while (ShouldContinuePlaying())
        {
            if (_dealer.DeckCardCount() == 0 && _dealer.PileCardCount() == 0)
            {
                _gameIsOver = true;
                break;
            }

            Player currentPlayer = _players[_currentPlayerIndex];

            _dealer.RecyclePileIntoDeck();
            PlayerDraws(currentPlayer);
            PlayerDiscards(currentPlayer);

            _currentPlayerIndex = RotateToNextPlayer(_currentPlayerIndex);
            _gameWriter.DeckAndPileStatus(_dealer);
        }
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

        string drawSource = "deck";
        Card drawnCard;

        if (!_drawChoiceEnabled)
        {
            drawnCard = _dealer.DrawFromDeck();
        }
        else
        {
            drawSource = ChooseDrawSource();
            drawnCard = ChooseCardFromDeckOrPile(drawSource);
        }

        player.AddToHand(drawnCard);
        _gameWriter.DeckAndPileStatus(_dealer);
        _gameWriter.DrawAction(player, drawSource, drawnCard);
    }

    private string ChooseDrawSource()
    {
        string source = "deck";

        Random random = new Random();
        double randomValue = random.NextDouble();

        if (randomValue < 0.5 && _dealer.PileCardCount() > 0)
        {
            source = "pile";
        }

        return source;
    }

    private Card ChooseCardFromDeckOrPile(string drawSource)
    {
        return drawSource == "deck" ? _dealer.DrawFromDeck() : _dealer.DrawFromPile();
    }

    private void PlayerDiscards(Player player)
    {
        Card discard = player.DiscardFromHand();
        _dealer.AddToPile(discard);
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