using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
public class RunFinderTest
{
    private Card _card02C;
    private Card _card03C;
    private Card _card04C;
    private Card _card05C;
    private Card _card06C;
    private Card _card08C;
    private Card _card09C;
    private Card _card10C;
    private Card _cardJaC;
    private Card _cardQuC;
    private Card _card03H;
    private Card _card04H;
    private Card _card05H;
    private Card _card06H;
    private Card _card08H;
    private Card _card09H;
    private Card _card10H;
    private Card _cardJaH;
    private Card _cardQuH;
    
    private List<Card> _expectedRun2Cto5C;
    private List<Card> _expectedRun3Cto6C;
    private List<Card> _expectedRun2Cto6C;
    private List<Card> _expectedRun8CtoJaC;
    private List<Card> _expectedRun9CtoQuC;
    private List<Card> _expectedRun8CtoQuC;

    private List<Card> _expectedRun3Hto6H;
    private List<Card> _expectedRun8HtoJaH;
    private List<Card> _expectedRun9HtoQuH;
    private List<Card> _expectedRun8HtoQuH;

    [SetUp]
    public void SetUp()
    {
        _card02C = new Card(Rank.Two, Suit.Clubs);
        _card03C = new Card(Rank.Three, Suit.Clubs);
        _card04C = new Card(Rank.Four, Suit.Clubs);
        _card05C = new Card(Rank.Five, Suit.Clubs);
        _card06C = new Card(Rank.Six, Suit.Clubs);
        _card08C = new Card(Rank.Eight, Suit.Clubs);
        _card09C = new Card(Rank.Nine, Suit.Clubs);
        _card10C = new Card(Rank.Ten, Suit.Clubs);
        _cardJaC = new Card(Rank.Jack, Suit.Clubs);
        _cardQuC = new Card(Rank.Queen, Suit.Clubs);
        
        _card03H = new Card(Rank.Three, Suit.Hearts);
        _card04H = new Card(Rank.Four, Suit.Hearts);
        _card05H = new Card(Rank.Five, Suit.Hearts);
        _card06H = new Card(Rank.Six, Suit.Hearts);
        _card08H = new Card(Rank.Eight, Suit.Hearts);
        _card09H = new Card(Rank.Nine, Suit.Hearts);
        _card10H = new Card(Rank.Ten, Suit.Hearts);
        _cardJaH = new Card(Rank.Jack, Suit.Hearts);
        _cardQuH = new Card(Rank.Queen, Suit.Hearts);


        _expectedRun2Cto5C = new List<Card>()
        {
            _card02C, _card03C, _card04C, _card05C
        };

        _expectedRun3Cto6C = new List<Card>()
        {
            _card03C, _card04C, _card05C, _card06C
        };

        _expectedRun2Cto6C = new List<Card>()
        {
            _card02C, _card03C, _card04C, _card05C, _card06C
        };

        _expectedRun8CtoJaC = new List<Card>()
        {
            _card08C, _card09C, _card10C, _cardJaC,
        };

        _expectedRun9CtoQuC = new List<Card>()
        {
            _card09C, _card10C, _cardJaC, _cardQuC
        };

        _expectedRun8CtoQuC = new List<Card>()
        {
            _card08C, _card09C, _card10C, _cardJaC, _cardQuC
        };
        
        
        
        _expectedRun3Hto6H = new List<Card>()
        {
            _card03H, _card04H, _card05H, _card06H
        };

        _expectedRun8HtoJaH = new List<Card>()
        {
            _card08H, _card09H, _card10H, _cardJaH,
        };

        _expectedRun9HtoQuH = new List<Card>()
        {
            _card09H, _card10H, _cardJaH, _cardQuH
        };

        _expectedRun8HtoQuH = new List<Card>()
        {
            _card08H, _card09H, _card10H, _cardJaH, _cardQuH
        };
    }

    [Test]
    public void Finds3AvailableRuns()
    {
        List<Card> cards = new List<Card>()
        {
            _card02C, _card03C, _card04C, _card05C, _card06C
        };


        List<List<Card>> actualRuns = RunFinder.FindPossibleRuns(cards);

        Assert.AreEqual(3, actualRuns.Count);
        Assert.Contains(_expectedRun2Cto5C, actualRuns);
        Assert.Contains(_expectedRun3Cto6C, actualRuns);
        Assert.Contains(_expectedRun2Cto6C, actualRuns);
    }

    [Test]
    public void Finds3AvailableRunsWithDistractors()
    {
        List<Card> cards = new()
        {
            _card02C, _card03C, _card04C, _card05C, _card06C,
            new Card(Rank.Two, Suit.Hearts), new Card(Rank.Queen, Suit.Diamonds),
        };


        List<List<Card>> actualRuns = RunFinder.FindPossibleRuns(cards);

        Assert.AreEqual(3, actualRuns.Count);
        Assert.Contains(_expectedRun2Cto5C, actualRuns);
        Assert.Contains(_expectedRun3Cto6C, actualRuns);
        Assert.Contains(_expectedRun2Cto6C, actualRuns);
    }

    [Test]
    public void Finds4AvailableRunsWithGap()
    {
        List<Card> cards = new()
        {
            _card02C, _card03C, _card04C, _card05C, _card06C,
            new Card(Rank.Two, Suit.Hearts), new Card(Rank.Queen, Suit.Diamonds),
            _card08C, _card09C, _card10C, _cardJaC,
        };


        List<List<Card>> actualRuns = RunFinder.FindPossibleRuns(cards);

        Assert.AreEqual(4, actualRuns.Count);
        Assert.Contains(_expectedRun2Cto5C, actualRuns);
        Assert.Contains(_expectedRun3Cto6C, actualRuns);
        Assert.Contains(_expectedRun2Cto6C, actualRuns);
        Assert.Contains(_expectedRun8CtoJaC, actualRuns);
    }

    [Test]
    public void Finds6AvailableRunsWithGapAndExtension()
    {
        List<Card> cards = new()
        {
            _card02C, _card03C, _card04C, _card05C, _card06C,
            new Card(Rank.Two, Suit.Hearts), new Card(Rank.Queen, Suit.Diamonds),
            _card08C, _card09C, _card10C, _cardJaC, _cardQuC
        };


        List<List<Card>> actualRuns = RunFinder.FindPossibleRuns(cards);

        Assert.AreEqual(6, actualRuns.Count);
        Assert.Contains(_expectedRun2Cto5C, actualRuns);
        Assert.Contains(_expectedRun3Cto6C, actualRuns);
        Assert.Contains(_expectedRun2Cto6C, actualRuns);
        Assert.Contains(_expectedRun8CtoJaC, actualRuns);
        Assert.Contains(_expectedRun9CtoQuC, actualRuns);
        Assert.Contains(_expectedRun8CtoQuC, actualRuns);
    }
    
    [Test]
    public void Finds6AvailableRunsWithMulitpleSuits()
    {
        List<Card> cards = new()
        {
            _card02C, _card03C, _card04C, _card05C, _card06C,
            new Card(Rank.Two, Suit.Diamonds), new Card(Rank.Queen, Suit.Diamonds),
            _card08C, _card09C, _card10C, _cardJaC, _cardQuC,
            _card03H, _card04H, _card05H, _card06H,
            _card08H, _card09H, _card10H, _cardJaH, _cardQuH,
        };


        List<List<Card>> actualRuns = RunFinder.FindPossibleRuns(cards);

        Assert.AreEqual(10, actualRuns.Count);
        Assert.Contains(_expectedRun2Cto5C, actualRuns);
        Assert.Contains(_expectedRun3Cto6C, actualRuns);
        Assert.Contains(_expectedRun2Cto6C, actualRuns);
        Assert.Contains(_expectedRun8CtoJaC, actualRuns);
        Assert.Contains(_expectedRun9CtoQuC, actualRuns);
        Assert.Contains(_expectedRun8CtoQuC, actualRuns); 
        
        Assert.Contains(_expectedRun3Hto6H, actualRuns);
        Assert.Contains(_expectedRun8HtoJaH, actualRuns);
        Assert.Contains(_expectedRun9HtoQuH, actualRuns);
        Assert.Contains(_expectedRun8HtoQuH, actualRuns);
    }
    
    
}