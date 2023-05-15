using System.Collections.Generic;
using Game;
using NUnit.Framework;

public class DealerTest
{
    [Test]
    public void DealElevenCardsToFourPlayers()
    {
        List<Player> players = new PlayerFactory().CreatePlayers();
        Deck deck = new Deck();

        Dealer dealer = new Dealer(deck, players);

        dealer.Deal();

        players.ForEach(AllCardsAndUnique);
        Assert.AreEqual(64, deck.CardCount());
    }

    private void AllCardsAndUnique(Player player)
    {
        Assert.AreEqual(11, player.CardCount());
        CollectionAssert.AllItemsAreUnique(player.Hand());
    }
}