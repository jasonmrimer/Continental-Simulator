using System.Collections.Generic;
using Game;
using NUnit.Framework;

[TestFixture]
public class DealerTest
{
    private List<Player> _players;
    private Deck _deck;
    private Dealer _dealer;

    [SetUp]
    public void SetUp()
    {
        _players = new PlayerStub().CreatePlayers();
        _deck = new Deck();
        _dealer = new Dealer(_deck, _players);
    }

    [Test]
    public void DealElevenCardsToFourPlayers()
    {
        _dealer.Deal();

        _players.ForEach(AllCardsAndUnique);
        Assert.AreEqual(
            63,
            _deck.CardCount(),
            "There should be 63 cards: 108 - 44 dealt - 1 discard pile."
        );
    }

    [Test]
    public void FacilitatesADiscardPile()
    {
        _dealer.Deal();

        Assert.AreEqual(
            1,
            _dealer.PileCardCount(),
            "After dealing, the discard pile should have 1 card"
        );
    }

    [Test]
    public void RecyclesPileIntoDeck()
    {
        
    }
    
    private void AllCardsAndUnique(Player player)
    {
        Assert.AreEqual(11, player.CardCount());
        CollectionAssert.AllItemsAreUnique(player.Hand());
    }
}