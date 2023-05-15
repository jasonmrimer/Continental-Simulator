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
                    Card joker1 = new Card(suit, CardValue.Joker);
                    Card joker2 = new Card(suit, CardValue.Joker);
                    _cards.Add(joker1);
                    _cards.Add(joker2);
                }
                else
                {
                    for (int j = 1; j <= 13; j++)
                    {
                        CardValue cardValue = (CardValue)j;
                        Card card = new Card(suit, cardValue);
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
        int cardCount = _cards.Count;

        // Create a range from 0 to cardCount (exclusive)
        Range range = new Range(0, cardCount);

        // Shuffle the deck using Fisher-Yates algorithm
        System.Random random = new System.Random();
        _cards.Sort((a, b) => random.Next(range.Start.Value, range.End.Value));
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