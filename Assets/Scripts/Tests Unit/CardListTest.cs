using NUnit.Framework;

[TestFixture]
public class CardListTest
{
    private Card _card02C;
    private Card _card03C;
    private Card _card04C;

    private Card _card05C;

    // private Card _card07D;
    // private Card _card08D;
    // private Card _card09D;
    // private Card _card10D;
    // private Card _cardJaH1;
    // private Card _cardJaH2;
    // private Card _cardJaS;
    // private CardList _run02Cto05C;
    // private CardList _run07Dto10D;
    // private CardList _atamaJacks;
    //
    [SetUp]
    public void SetUp()
    {
        _card02C = new Card(Rank.Two, Suit.Clubs);
        _card03C = new Card(Rank.Three, Suit.Clubs);
        _card04C = new Card(Rank.Four, Suit.Clubs);
        _card05C = new Card(Rank.Five, Suit.Clubs);
    }

    [Test]
    public void RemovesOneCopyOfCard()
    {
        CardList run2To5 = new() { _card02C, _card03C, _card04C, _card05C, };

        CardList hand = new(run2To5);
        
        hand.AddRange(new[] { _card02C, _card03C, _card04C, _card05C });

        Assert.AreEqual(8, hand.Count);

        hand.RemoveRange(run2To5);

        Assert.AreEqual(4, hand.Count);
        Assert.AreEqual(run2To5, hand);
    }
}