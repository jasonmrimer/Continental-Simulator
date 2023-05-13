using System.Collections.Generic;

public class Dealer
{
    private List<Player> _players;
    private Deck _deck;

    public Dealer(Deck deck, List<Player> players)
    {
        _deck = deck;
        _players = players;
    }

    public void Deal()
    {
        _players.ForEach(DealStartingHand);
    }

    private void DealStartingHand(Player player)
    {
        for (int i = 0; i < 11; i++)
        {
            Card card = _deck.DrawCard();
            player.addToHand(card);
        }
    }
}