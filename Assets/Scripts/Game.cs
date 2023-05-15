using System.Collections.Generic;

namespace Game
{
    public class Game
    {
        private Dealer _dealer;
        private Deck _deck;
        private List<Player> _players;
        private List<Card> _discardPile;
        private bool _gameIsOver = false;

        public Game()
        {
            SetupAndDeal();
        }

        public void Play()
        {
            int currentPlayerIndex = 0;

            // while (!gameIsOver)
            {
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
    }
}