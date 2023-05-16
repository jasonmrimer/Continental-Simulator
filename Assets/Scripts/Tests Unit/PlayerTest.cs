using NUnit.Framework;

[TestFixture]
public class PlayerTest
{
    [Test]
    public void PrintHand()
    {
        Player player = new Player("Alice");
        player.addToHand(new Card(CardValue.Two, Suit.Clubs));
        player.addToHand(new Card(CardValue.Seven, Suit.Diamonds));
        player.addToHand(new Card(CardValue.Queen, Suit.Hearts));
        player.addToHand(new Card(CardValue.Ace, Suit.Spades));
        player.addToHand(new Card(CardValue.Joker, Suit.Wild));

        Assert.AreEqual(
            "2♣ | 7♦ | Q♥ | A♠ | Jo★",
            player.FormatHandForPrint()
        );
    }
}