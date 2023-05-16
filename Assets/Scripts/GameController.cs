using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController
{
    private Dealer _dealer;
    private Deck _deck;
    private List<Player> _players;
    private List<Card> _discardPile;
    private bool _gameIsOver;
    private int _turnCount;
    private int _currentPlayerIndex;

    public GameController()
    {
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

    private void PlayerDiscards(Player player)
    {
        Card discard = player.discardFromHand();
        _discardPile.Add(discard);
        Console.WriteLine($"{player.Name} discards {discard.Printable()}");
    }

    private void PlayerDraws(Player player)
    {
        Console.WriteLine($"{player.Name} begins turn {_turnCount} with: {player.FormatHandForPrint()}");
        Card drawCard = _deck.DrawCard();
        player.addToHand(drawCard);
        Console.WriteLine($"{player.Name} draws {drawCard.Printable()}");
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
        return this._turnCount;
    }
}