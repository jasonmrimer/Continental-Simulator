using NUnit.Framework;

[TestFixture]
public class CardTest
{
    [Test]
    public void PrintableFormat()
    {
        Card numberCard = new Card(CardValue.Five, Suit.Clubs);
        Card faceCard = new Card(CardValue.Queen, Suit.Hearts);
        Card jokerCard = new Card(CardValue.Joker, Suit.Wild);
        
        Assert.AreEqual("5♣", numberCard.Printable());
        Assert.AreEqual("Q♥", faceCard.Printable());
        Assert.AreEqual("Jo★", jokerCard.Printable());
    }
}