using System.Collections.Generic;

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

    public List<Card> Hand()
    {
        return _cards;
    }
}