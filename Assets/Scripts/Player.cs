using System.Collections.Generic;

namespace DefaultNamespace
{
    public class Player
    {
        private List<Card> _cards;

        public Player()
        {
            _cards = new List<Card>();
        }

        public int CardCount()
        {
            return _cards.Count;
        }

        public void addToHand(Card card)
        {
            _cards.Add(card);
        }
        
        public List<Card> getHand()
        {
            return _cards;
        }
    }
}