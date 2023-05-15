using System.Collections.Generic;

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

    public void Deal()
    {
        _players.ForEach(DealStartingHand);
        _discardPile.Add(_deck.DrawCard());
    }

    private void DealStartingHand(Player player)
    {
        for (int i = 0; i < 11; i++)
        {
            Card card = _deck.DrawCard();
            player.addToHand(card);
        }
    }

    public int DiscardPileCount()
    {
        return _discardPile.Count;
    }
}