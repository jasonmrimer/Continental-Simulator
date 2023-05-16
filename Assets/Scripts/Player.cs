using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    public void addToHand(Card card)
    {
        _cards.Add(card);
    }

    public List<Card> Hand()
    {
        return _cards;
    }

    public void PrintHand()
    {
        string cardList = FormatHandForPrint();
        Debug.Log(cardList);
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

    public Card ChooseDrawCard(Card cardFromDeck, Card cardFromDiscardPile)
    {
        return cardFromDeck;
    }

    public Card discardFromHand()
    {
        Card discard = _cards.Last();
        _cards.Remove(discard);
        return discard;
    }
}