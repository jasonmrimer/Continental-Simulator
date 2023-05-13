using System.Collections.Generic;
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
    }

    public List<Card> GetCards()
    {
        return _cards;
    }

    public int CardCount()
    {
        return _cards.Count;
    }

    public Card DrawCard()
    {
        Card drawnCard = _cards[^1];
        _cards.RemoveAt(_cards.Count - 1);
        return drawnCard;
    }
}