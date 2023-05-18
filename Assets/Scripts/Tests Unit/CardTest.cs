using NUnit.Framework;

[TestFixture]
public class CardTest
{
    [Test]
    public void PrintableFormat()
    {
        Card numberCard = new Card(Rank.Five, Suit.Clubs);
        Card faceCard = new Card(Rank.Queen, Suit.Hearts);
        Card jokerCard = new Card(Rank.Joker, Suit.Wild);
        
        Assert.AreEqual("5♣", numberCard.Printable());
        Assert.AreEqual("Q♥", faceCard.Printable());
        Assert.AreEqual("Jo★", jokerCard.Printable());
    }
}