using NUnit.Framework;

[TestFixture]
public class PlayerTest
{
    private Player _player;

    [SetUp]
    public void SetUp()
    {
        _player = new Player("Alice");
        _player.AddToHand(new Card(CardValue.Two, Suit.Clubs));
        _player.AddToHand(new Card(CardValue.Seven, Suit.Diamonds));
        _player.AddToHand(new Card(CardValue.Queen, Suit.Hearts));
        _player.AddToHand(new Card(CardValue.Ace, Suit.Spades));
        _player.AddToHand(new Card(CardValue.Joker, Suit.Wild));
    }

    [Test]
    public void PrintHand()
    {
        Assert.AreEqual(
            "2♣ | 7♦ | Q♥ | A♠ | Jo★",
            _player.FormatHandForPrint()
        );
    }

    [Test]
    public void Discard()
    {
        Assert.AreEqual(5, _player.CardCount());
        _player.DiscardFromHand();
        Assert.AreEqual(4, _player.CardCount());
    }
}