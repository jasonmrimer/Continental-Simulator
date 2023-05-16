using System;
using System.Collections.Generic;
using System.Linq;

public class GameController
{
    private Dealer _dealer;
    private List<Player> _players;
    private bool _gameIsOver;
    private readonly bool _drawChoiceEnabled;
    private int _turnCount;
    private int _currentPlayerIndex;
    private int _turnLimit;

    public GameController(bool drawChoiceEnabled = false, int turnLimit = 100)
    {
        _drawChoiceEnabled = drawChoiceEnabled;
        _turnLimit = turnLimit;

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
            Console.WriteLine($"Cards left in Deck: {_dealer.DeckCardCount()} & Pile: {_dealer.PileCardCount()}");
        }
    }

    private bool ShouldContinuePlaying()
    {
        if (_drawChoiceEnabled)
        {
            if (_turnCount == _turnLimit)
            {
                _gameIsOver = true;
            }
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
        Console.WriteLine($"{player.Name} begins turn {_turnCount} with: {player.FormatHandForPrint()}");

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
        Console.WriteLine($"{player.Name} draws {drawnCard.Printable()} from {drawSource}");
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
        Console.WriteLine($"{player.Name} discards {discard.Printable()}");
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