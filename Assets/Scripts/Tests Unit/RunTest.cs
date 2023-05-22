using NUnit.Framework;

[TestFixture]
public class RunTest
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
    //
    //     _card07D = new Card(Rank.Seven, Suit.Diamonds);
    //     _card08D = new Card(Rank.Eight, Suit.Diamonds);
    //     _card09D = new Card(Rank.Nine, Suit.Diamonds);
    //     _card10D = new Card(Rank.Ten, Suit.Diamonds);
    //
    //     _cardJaH1 = new Card(Rank.Jack, Suit.Hearts);
    //     _cardJaH2 = new Card(Rank.Jack, Suit.Hearts);
    //     _cardJaS = new Card(Rank.Jack, Suit.Spades);

    [Test]
    public void EqualsWhenSameObjects()
    {
        CardList cardList01 = new() { _card02C, _card03C, _card04C, _card05C };
        CardList cardList02 = new() { _card02C, _card03C, _card04C, _card05C };
        
        Assert.AreEqual(cardList01, cardList02);
    }

    [Test]
    public void EqualsWhenSameCardsButDifferentObjects()
    {
        Card card02Cv2 = new Card(Rank.Two, Suit.Clubs);
        Card card03Cv2 = new Card(Rank.Three, Suit.Clubs);
        Card card04Cv2 = new Card(Rank.Four, Suit.Clubs);
        Card card05Cv2 = new Card(Rank.Five, Suit.Clubs);

        CardList cardList01 = new() { _card02C, _card03C, _card04C, _card05C };
        CardList cardList02 = new() { card02Cv2, card03Cv2, card04Cv2, card05Cv2 };

        Assert.AreEqual(cardList01, cardList02);
    }

    [Test]
    public void NotEqualWhenDifferentOrder()
    {
        CardList cardList01 = new() { _card02C, _card03C, _card04C, _card05C };
        CardList cardList02 = new() { _card03C, _card02C, _card04C, _card05C };

        Assert.AreNotEqual(cardList01, cardList02);
    }

    [Test]
    public void NotEqualWhenDifferentSuit()
    {
        Card card02H = new Card(Rank.Two, Suit.Hearts);
        Card card03H = new Card(Rank.Three, Suit.Hearts);
        Card card04H = new Card(Rank.Four, Suit.Hearts);
        Card card05H = new Card(Rank.Five, Suit.Hearts);

        CardList cardList01 = new() { _card02C, _card03C, _card04C, _card05C };
        CardList cardList02 = new() { card02H, card03H, card04H, card05H };

        Assert.AreNotEqual(cardList01, cardList02);
    }
}