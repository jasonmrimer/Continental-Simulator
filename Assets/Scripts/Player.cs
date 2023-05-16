using System;
using System.Collections.Generic;
using System.Linq;

public class Player
{
    private List<Card> _cards;

    public Player(string name)
    {
        _cards = new List<Card>();
        Name = name;
    }

    public string Name { get; set; }

    public int CardCount()
    {
        return _cards.Count;
    }

    public void AddToHand(Card card)
    {
        _cards.Add(card);
    }

    public List<Card> Hand()
    {
        return _cards;
    }

    public string FormatHandForPrint()
    {
        string printableHand = "";
        
        foreach (Card card in _cards)
        {
            printableHand += card.Printable();
            
            if (card != _cards.Last())
            {
                printableHand += " | ";
            }
        }

        return printableHand;
    }

    public Card DiscardFromHand()
    {
        Card discard = ChooseDiscard();
        _cards.Remove(discard);
        return discard;
    }

    private Card ChooseDiscard()
    {
        Random random = new Random();
        Card card = _cards[random.Next(_cards.Count)];
        return card;
    }

    public void TakeTurn(Card topOfPile)
    {
        
    }

    public DrawSource ChooseDrawSource(bool pileIsAvailable)
    {
        DrawSource source = DrawSource.Deck;

        Random random = new Random();
        double randomValue = random.NextDouble();

        if (randomValue < 0.5 && pileIsAvailable)
        {
            source = DrawSource.Pile;
        }

        return source;
    }

    public bool DecideWhetherToTakePenalty()
    {
        return false;
    }
}