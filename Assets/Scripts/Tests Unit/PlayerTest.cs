using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
public class PlayerTest
{
    private Player _player;

    [SetUp]
    public void SetUp()
    {
        _player = new Player("Alice");
        _player.AddToHand(new Card(Rank.Two, Suit.Clubs));
        _player.AddToHand(new Card(Rank.Seven, Suit.Diamonds));
        _player.AddToHand(new Card(Rank.Queen, Suit.Hearts));
        _player.AddToHand(new Card(Rank.Ace, Suit.Spades));
        _player.AddToHand(new Card(Rank.Joker, Suit.Wild));
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

    [Test]
    public void ChooseDrawSourceAtRandom()
    {
        // Card topOfPile = new Card(CardValue.Eight, Suit.Spades);
        HashSet<DrawSource> chosenSources = new HashSet<DrawSource>();
        int choiceCount = 1;

        while (chosenSources.Count < 2 && choiceCount < 100)
        {
            chosenSources.Add(Player.ChooseDrawSource(pileIsAvailable:true));
            choiceCount++;
        }

        Assert.Less(
            choiceCount,
            100,
            "Player did not randomly select both within 100 tries--unlikely result indicative of failure"
        );
    }
    
    [Test]
    public void DiscardRemovesFromHand()
    {
        _player.DiscardFromHand();
        Assert.AreEqual(4, _player.CardCount());
    }
}