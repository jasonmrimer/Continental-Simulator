using System;
using System.Collections.Generic;
using System.Linq;
using Game;

public class Deck
{
    private List<Card> _cards;

    public Deck()
    {
        _cards = new List<Card>();
        for (int i = 0; i < 2; i++)
        {
            foreach (Suit suit in System.Enum.GetValues(typeof(Suit)))
            {
                if (suit == Suit.Wild)
                {
                    Card joker1 = new Card(CardValue.Joker, suit);
                    Card joker2 = new Card(CardValue.Joker, suit);
                    _cards.Add(joker1);
                    _cards.Add(joker2);
                }
                else
                {
                    for (int j = 1; j <= 13; j++)
                    {
                        CardValue cardValue = (CardValue)j;
                        Card card = new Card(cardValue, suit);
                        _cards.Add(card);
                    }
                }
            }
        }
        
        Shuffle();
    }

    public List<Card> GetCards()
    {
        return _cards;
    }

    public void Shuffle()
    {
        // Shuffle the deck using Fisher-Yates algorithm
        Random random = new Random();
        int n = _cards.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            (_cards[k], _cards[n]) = (_cards[n], _cards[k]);
        }
    }
    
    public int CardCount()
    {
        return _cards.Count;
    }

    public Card DrawCard()
    {
        Card drawnCard = _cards.Last();
        _cards.Remove(drawnCard);
        return drawnCard;
    }

    public Card TopCard()
    {
        return _cards.Last();
    }

    public void AddCards(List<Card> discardPile)
    {
        _cards.AddRange(discardPile);
    }
}