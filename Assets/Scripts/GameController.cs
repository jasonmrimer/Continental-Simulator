using System.Collections.Generic;

public class GameController
{
    private Dealer _dealer;
    private Deck _deck;
    private List<Player> _players;
    private List<Card> _discardPile;
    private bool _gameIsOver = false;
    private int _turnCount = 1;

    public GameController()
    {
        SetupAndDeal();
    }

    public void Play()
    {
        int currentPlayerIndex = 0;

        while (!_gameIsOver)
        {
            if (_deck.CardCount() == 0)
            {
                _gameIsOver = true;
                break; 
            }

            Player currentPlayer = _players[currentPlayerIndex];
            currentPlayer.addToHand(_deck.DrawCard());
            _discardPile.Add(currentPlayer.discardFromHand());

            _turnCount++;
        }
    }

    private void SetupAndDeal()
    {
        // Setup
        _deck = new Deck();
        _discardPile = new List<Card>();
        _players = new PlayerFactory().CreatePlayers();
        _dealer = new Dealer(_deck, _players);

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