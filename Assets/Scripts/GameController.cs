using System;
using System.Collections.Generic;
using System.Linq;

public class GameController
{
    private Dealer _dealer;
    private Deck _deck;
    private List<Player> _players;
    private List<Card> _discardPile;
    private bool _gameIsOver;
    private readonly bool _drawChoiceEnabled;
    private int _turnCount;
    private int _currentPlayerIndex;

    public GameController(bool drawChoiceEnabled = false)
    {
        _drawChoiceEnabled = drawChoiceEnabled;
        SetupAndDeal();
    }

    public void Play()
    {
        while (!_gameIsOver)
        {
            if (_deck.CardCount() == 0)
            {
                _gameIsOver = true;
                break;
            }

            Player currentPlayer = _players[_currentPlayerIndex];

            PlayerDraws(currentPlayer);
            PlayerDiscards(currentPlayer);

            _currentPlayerIndex = RotateToNextPlayer(_currentPlayerIndex);
        }
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
        Card drawnCard = null;
        
        if (!_drawChoiceEnabled)
        {
            drawnCard = _deck.DrawCard();
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

        if (randomValue < 0.5)
        {
            // Draw from the Deck
            source = "pile";
        }

        return source;
    }

    private Card ChooseCardFromDeckOrPile(string drawSource)
    {
        Card drawnCard = null;
        
        if (drawSource == "deck")
        {
            // Draw from the Deck
            drawnCard = _deck.DrawCard();
        }
        else
        {
            // Draw from the Discard Pile
            drawnCard = _discardPile.Last();
            _discardPile.Remove(drawnCard);
        }

        return drawnCard;
    }

    private void PlayerDiscards(Player player)
    {
        Card discard = player.DiscardFromHand();
        _discardPile.Add(discard);
        Console.WriteLine($"{player.Name} discards {discard.Printable()}");
    }

    private void SetupAndDeal()
    {
        // Setup
        _deck = new Deck();
        _discardPile = new List<Card>();
        _players = new PlayerStub().CreatePlayers();
        _dealer = new Dealer(_deck, _players);

        _turnCount = 1;
        _currentPlayerIndex = 0;
        _gameIsOver = false;

        // Deal
        _dealer.Deal();
        _discardPile.Add(_deck.DrawCard());
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