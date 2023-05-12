using System.Collections.Generic;
using DefaultNamespace;

public class Dealer
{
    private List<Player> _players;
        
    public Dealer(List<Player> players)
    {
        this._players = players;
    }

    public void Deal()
    {
        _players.ForEach(DealStartingHand);
    }

    private void DealStartingHand(Player player)
    {
        for (int i = 0; i < 11; i++)
        {
            Card card = new Card();
            player.addToHand(card);
        }
    }
}