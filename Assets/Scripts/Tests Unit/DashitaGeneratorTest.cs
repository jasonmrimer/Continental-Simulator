using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
public class DashitaGeneratorTest
{
    private Player _player;
    private Card _card02C;
    private Card _card03C;
    private Card _card04C;
    private Card _card05C;
    private Card _card07D;
    private Card _card08D;
    private Card _card09D;
    private Card _card10D;
    private Card _cardJaH1;
    private Card _cardJaH2;
    private Card _cardJaS;
    private Run _run02Cto05C;
    private Run _run07Dto10D;
    private Atama _atamaJacks;

    [SetUp]
    public void SetUp()
    {
        _card02C = new Card(Rank.Two, Suit.Clubs);
        _card03C = new Card(Rank.Three, Suit.Clubs);
        _card04C = new Card(Rank.Four, Suit.Clubs);
        _card05C = new Card(Rank.Five, Suit.Clubs);

        _card07D = new Card(Rank.Seven, Suit.Diamonds);
        _card08D = new Card(Rank.Eight, Suit.Diamonds);
        _card09D = new Card(Rank.Nine, Suit.Diamonds);
        _card10D = new Card(Rank.Ten, Suit.Diamonds);

        _cardJaH1 = new Card(Rank.Jack, Suit.Hearts);
        _cardJaH2 = new Card(Rank.Jack, Suit.Hearts);
        _cardJaS = new Card(Rank.Jack, Suit.Spades);

        _player = new Player("Tester");
        _player.AddToHand(_card02C);
        _player.AddToHand(_card03C);
        _player.AddToHand(_card04C);
        _player.AddToHand(_card05C);

        _player.AddToHand(_card07D);
        _player.AddToHand(_card08D);
        _player.AddToHand(_card09D);
        _player.AddToHand(_card10D);

        _player.AddToHand(_cardJaH1);
        _player.AddToHand(_cardJaH2);
        _player.AddToHand(_cardJaS);

        _run02Cto05C = new Run { _card02C, _card03C, _card04C, _card05C };

        _run07Dto10D = new Run()
        {
            _card07D,
            _card08D,
            _card09D,
            _card10D
        };

        _atamaJacks = new Atama()
        {
            _cardJaH1,
            _cardJaH2,
            _cardJaS,
        };
    }

    [Test]
    public void GeneratesSimplestHand()
    {
        List<Card> hand = new()
        {
            _card02C, _card03C, _card04C, _card05C,
            _card07D, _card08D, _card09D, _card10D,
            _cardJaH1, _cardJaH2, _cardJaS,
        };

        Dashita expectedDashita = new Dashita(
            new List<Run> { _run02Cto05C, _run07Dto10D },
            _atamaJacks
        );


        List<Dashita> dashitaOptions = DashitaGenerator.GenerateOptions(hand);


        Assert.AreEqual(1, dashitaOptions.Count);
        Dashita actualDashita = dashitaOptions[0];
        
        Assert.Contains(_run02Cto05C, actualDashita.Runs);
        Assert.Contains(_run07Dto10D, actualDashita.Runs);
        Assert.AreEqual(_atamaJacks, actualDashita.Atama);
        Assert.AreEqual(expectedDashita, actualDashita);
        // Assert.IsTrue(actualDashita.Equals(expectedDashita));
    }


    [Test]
    [Ignore("")]
    public void DetectsTwoRunsAndAtama()
    {
        Dashita dashita = DashitaGenerator.CheckAndCreateDashita(_player);
        List<Card> actualRunOne = dashita.Runs[0];
        List<Card> actualRunTwo = dashita.Runs[1];
        List<Card> actualAtama = dashita.Atama;

        if (actualRunOne.Contains(_card02C))
        {
            Assert.AreEqual(_run02Cto05C, actualRunOne);
            Assert.AreEqual(_run07Dto10D, actualRunTwo);
        }
        else
        {
            Assert.AreEqual(_run02Cto05C, actualRunTwo);
            Assert.AreEqual(_run07Dto10D, actualRunOne);
        }

        Assert.AreEqual(_atamaJacks, actualAtama);
    }

    [Test]
    [Ignore("")]
    public void TestAvailableDashitaOptions()
    {
        Card card06C = new Card(Rank.Six, Suit.Clubs);
        _player.AddToHand(card06C);

        Run run3Cto6C = new Run()
        {
            _card03C, _card04C, _card05C, card06C
        };

        Run run2Cto6C = new Run()
        {
            _card02C, _card03C, _card04C, _card05C, card06C
        };

        Dashita dashitaWith2Cto5C = new Dashita(
            new List<Run>() { _run02Cto05C, _run07Dto10D },
            _atamaJacks
        );
        Dashita dashitaWith3Cto6C = new Dashita(
            new List<Run>() { run3Cto6C, _run07Dto10D },
            _atamaJacks
        );

        Dashita dashitaWith2Cto6C = new Dashita(
            new List<Run>() { run2Cto6C, _run07Dto10D },
            _atamaJacks
        );

        List<Dashita> dashitaOptions = DashitaGenerator.DashitaOptions();

        Assert.AreEqual(
            3,
            dashitaOptions.Count,
            "Did not find correct number of dashita options"
        );

        Assert.Contains(dashitaWith2Cto5C, dashitaOptions);
        Assert.Contains(dashitaWith3Cto6C, dashitaOptions);
        Assert.Contains(dashitaWith2Cto6C, dashitaOptions);
    }
}