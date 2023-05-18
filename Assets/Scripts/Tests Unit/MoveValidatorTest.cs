using System.Collections.Generic;
using Game;
using NUnit.Framework;

[TestFixture]
public class MoveValidatorTest
{
    private Player _player;

    [SetUp]
    public void SetUp()
    {
        _player = new Player("Tester");
    }

    [Test]
    public void DetectsTwoRunsAndAtama()
    {
        Card card02C = new Card(Rank.Two, Suit.Clubs);
        Card card03C = new Card(Rank.Three, Suit.Clubs);
        Card card04C = new Card(Rank.Four, Suit.Clubs);
        Card card05C = new Card(Rank.Five, Suit.Clubs);
        
        Card card07D = new Card(Rank.Seven, Suit.Diamonds);
        Card card08D = new Card(Rank.Eight, Suit.Diamonds);
        Card card09D = new Card(Rank.Nine, Suit.Diamonds);
        Card card10D = new Card(Rank.Ten, Suit.Diamonds);
        
        Card cardJaH1 = new Card(Rank.Ten, Suit.Hearts);
        Card cardJaH2 = new Card(Rank.Ten, Suit.Hearts);
        Card cardJaS = new Card(Rank.Ten, Suit.Spades);
        
        _player.AddToHand(card02C);
        _player.AddToHand(card03C);
        _player.AddToHand(card04C);
        _player.AddToHand(card05C);
        _player.AddToHand(card07D);
        _player.AddToHand(card08D);
        _player.AddToHand(card09D);
        _player.AddToHand(card10D);
        _player.AddToHand(cardJaH1);
        _player.AddToHand(cardJaH2);
        _player.AddToHand(cardJaS);

        List<Card> expectedRunOne = new List<Card>()
        {
            card02C,
            card03C,
            card04C,
            card05C
        };
        
        List<Card> expectedRunTwo = new List<Card>()
        {
            card07D,
            card08D,
            card09D,
            card10D
        };
        
        List<Card> expectedAtama = new List<Card>()
        {
            cardJaH1,
            cardJaH2,
            cardJaS,
        };
        

        Dashita dashita = MoveValidator.CheckAndCreateDashita(_player);
        List<Card> actualRunOne = dashita.RunOne;
        List<Card> actualRunTwo = dashita.RunTwo;
        List<Card> actualAtama = dashita.Atama;

        if (actualRunOne.Contains(card02C))
        {
            Assert.AreEqual(expectedRunOne, actualRunOne);
            Assert.AreEqual(expectedRunTwo, actualRunTwo);
        }
        else
        {
            Assert.AreEqual(expectedRunOne, actualRunTwo);
            Assert.AreEqual(expectedRunTwo, actualRunOne);
        }

        Assert.AreEqual(expectedAtama, actualAtama);
        
    }
}