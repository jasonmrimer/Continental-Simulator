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
}