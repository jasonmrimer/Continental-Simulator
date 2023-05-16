using System.Collections.Generic;
using System.Linq;

public class Dealer
{
    private List<Player> _players;
    private Deck _deck;
    private List<Card> _discardPile;

    public Dealer(Deck deck, List<Player> players)
    {
        _deck = deck;
        _players = players;
        _discardPile = new List<Card>();
    }

    public void RecyclePileIntoDeck()
    {
        if (DeckCardCount() == 0)
        {
            Card topOfPile = _discardPile.Last();
            _discardPile.Remove(topOfPile);
            _deck.AddCards(_discardPile);
            _discardPile.Clear();
            _discardPile.Add(topOfPile);
            _deck.Shuffle();
        }
    }

    public void Deal()
    {
        _deck.Shuffle();
        _players.ForEach(DealStartingHand);
        _discardPile.Add(_deck.DrawCard());
    }

    private void DealStartingHand(Player player)
    {
        for (int i = 0; i < 11; i++)
        {
            Card card = _deck.DrawCard();
            player.AddToHand(card);
        }
    }

    public void AddToPile(Card discard)
    {
        _discardPile.Add(discard);
    }

    public int PileCardCount()
    {
        return _discardPile.Count;
    }

    public int DeckCardCount()
    {
        return _deck.CardCount();
    }

    public Card DrawFromPile()
    {
        Card drawnCard = _discardPile.Last();
        _discardPile.Remove(drawnCard);
        return drawnCard;
    }

    public Card DrawFromDeck()
    {
        return _deck.DrawCard();
    }
}