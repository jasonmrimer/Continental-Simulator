using System.Collections.Generic;
using NUnit.Framework;

public class DealerTest
{
    [Test]
    public void DealElevenCardsToFourPlayers()
    {
        List<Player> players = new List<Player>(4)
        {
            new(),
            new(),
            new(),
            new()
        };
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