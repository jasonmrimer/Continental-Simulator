using System.Collections.Generic;
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

    private string FormatHandForPrint()
    {
        string printableHand = "";
        foreach (Card card in _cards)
        {
            printableHand += card.Printable() + " | ";
        }

        return printableHand;
    }

    public Card ChooseDrawCard(Card cardFromDeck, Card cardFromDiscardPile)
    {
        return cardFromDeck;
    }
}