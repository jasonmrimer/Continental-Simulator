using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class Game
    {
        private Dealer _dealer;
        private Deck _deck;
        private List<Player> _players;
        private List<Card> _discardPile;
        bool _gameIsOver = false;

        public Game()
        {
            SetupAndDeal();

            int currentPlayerIndex = 0;

            while (!gameIsOver)
            {
                Player currentPlayer = _players[currentPlayerIndex];
                PlayerDraw(currentPlayer);
                PlayerDiscard(currentPlayer);
            }
        }

        private void PlayerDiscard(Player player)
        {
            throw new System.NotImplementedException();
        }

        private void PlayerDraw(Player player)
        {
            string cardSource = "deck";

            // Print the current player's hand
            Debug.Log($"Player {player.Name}'s hand before draw:");
            player.PrintHand();

            // Draw a card from the deck or the discard pile
            Card drawnCard = null;
            CheckCardAvailabilityForGameEnd();
            if (DeckEmptyWhileDiscardPileFull())
            {
                ShuffleDiscardsIntoDeck();
            } 
            
            if (CardsAreAvailable())
            {
            }
            else
            {
                _gameIsOver = true;
            }

            if (_deck.CardCount() > 0)
            {
                Debug.Log("Drawing a card from the deck...");
                drawnCard = player.ChooseDrawCard(
                    _deck.TopCard(),
                    _discardPile.Last()
                );

                if (drawnCard == _deck.TopCard())
                {
                    drawnCard = _deck.DrawCard();
                }
                else if (drawnCard == _discardPile.Last())
                {
                    _discardPile.Remove(drawnCard);
                    cardSource = "discard pile";
                }
            }
            else if (_discardPile.Count == 0 && _deck.CardCount() == 0)
            {
                Debug.Log("The deck is empty and the discard pile has only one card left. The game is a draw.");
                return true;
                // Debug.Log("Shuffling the discard pile into the deck...");
                // _deck.AddCards(_discardPile);
                // _deck.Shuffle();
                // _discardPile.Clear();

                // Debug.Log("Drawing a card from the newly shuffled deck...");
                // drawnCard = deck.Draw();
            }

            Debug.Log($"Player {player.Name} drew {drawnCard.Printable()} from {cardSource}.");
            return false;
        }

        private void CheckCardAvailabilityForGameEnd()
        {
            if (_deck.CardCount() == 0 && _discardPile.Count == 0)
            {
                _gameIsOver = true;
            }
        }

        private bool CardsAreAvailable()
        {
            return ((_deck.CardCount() > 0) && (true));
        }

        private void SetupAndDeal()
        {
            _deck = new Deck();
            _discardPile = new List<Card>();
            _players = new List<Player>(4)
            {
                new("Alice"),
                new("Bob"),
                new("Chad"),
                new("Dani")
            };
            _dealer = new Dealer(_deck, _players);
            _dealer.Deal();
            _discardPile.Add(_deck.DrawCard());
        }
    }
}