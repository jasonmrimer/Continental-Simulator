using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    private Card _cardJaD;
    private Card _cardJaH1;
    private Card _cardJaH2;
    private Card _cardJaS;
    private Card _cardKiC;
    private Card _cardKiD;
    private Card _cardKiS;
    private Run _run02Cto05C;
    private Run _run07Dto10D;
    private Atama _atamaJacksHHS;
    private Atama _atamaKings;
    private Run _run07DtoJaD;
    private Run _run08DtoJaD;
    private Atama _atamaJacksDHH;
    private Atama _atamaJacksDHS;
    private Atama _atamaJacksDHHS;

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

        _cardJaD = new Card(Rank.Jack, Suit.Diamonds);
        _cardJaH1 = new Card(Rank.Jack, Suit.Hearts);
        _cardJaH2 = new Card(Rank.Jack, Suit.Hearts);
        _cardJaS = new Card(Rank.Jack, Suit.Spades);

        _cardKiC = new Card(Rank.King, Suit.Clubs);
        _cardKiD = new Card(Rank.King, Suit.Diamonds);
        _cardKiS = new Card(Rank.King, Suit.Spades);

        _run02Cto05C = new Run { _card02C, _card03C, _card04C, _card05C };

        _run07Dto10D = new Run() { _card07D, _card08D, _card09D, _card10D };
        _run07DtoJaD = new Run() { _card07D, _card08D, _card09D, _card10D, _cardJaD };
        _run08DtoJaD = new Run() { _card08D, _card09D, _card10D, _cardJaD };

        _atamaJacksDHH = new Atama() { _cardJaD, _cardJaH1, _cardJaH2, };
        _atamaJacksDHS = new Atama() { _cardJaD, _cardJaH2, _cardJaS, };
        _atamaJacksHHS = new Atama() { _cardJaH1, _cardJaH2, _cardJaS, };
        _atamaJacksDHHS = new Atama() { _cardJaD, _cardJaH1, _cardJaH2, _cardJaS, };

        _atamaKings = new Atama() { _cardKiC, _cardKiD, _cardKiS };
    }

    [Test]
    public void GeneratesSimplestHand()
    {
        CardList hand = new()
        {
            _card02C, _card03C, _card04C, _card05C,
            _card07D, _card08D, _card09D, _card10D,
            _cardJaH1, _cardJaH2, _cardJaS,
        };

        Dashita expectedDashita = new Dashita(
            new List<Run> { _run02Cto05C, _run07Dto10D },
            _atamaJacksHHS
        );


        HashSet<Dashita> dashitaOptions = DashitaGenerator.GenerateOptions(hand);


        Assert.AreEqual(1, dashitaOptions.Count);
        Assert.IsTrue(dashitaOptions.Contains(expectedDashita));
    }

    [Test]
    public void TestDashitaFromMultipleRunsAvailableAndSameAtama()
    {
        Card card06C = new Card(Rank.Six, Suit.Clubs);

        CardList hand = new()
        {
            _card02C, _card03C, _card04C, _card05C, card06C,
            _card07D, _card08D, _card09D, _card10D,
            _cardJaH1, _cardJaH2, _cardJaS,
        };

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
            _atamaJacksHHS
        );
        Dashita dashitaWith3Cto6C = new Dashita(
            new List<Run>() { run3Cto6C, _run07Dto10D },
            _atamaJacksHHS
        );

        Dashita dashitaWith2Cto6C = new Dashita(
            new List<Run>() { run2Cto6C, _run07Dto10D },
            _atamaJacksHHS
        );

        HashSet<Dashita> dashitaOptions = DashitaGenerator.GenerateOptions(hand);

        Assert.AreEqual(
            3,
            dashitaOptions.Count,
            "Did not find correct number of dashita options"
        );

        List<Dashita> dashitaList = new(dashitaOptions);
        Assert.Contains(dashitaWith2Cto5C, dashitaList);
        Assert.Contains(dashitaWith3Cto6C, dashitaList);
        Assert.Contains(dashitaWith2Cto6C, dashitaList);
    }

    [Test]
    public void TwoRunsAndTwoAtamaChoices()
    {
        CardList hand = new()
        {
            _card02C, _card03C, _card04C, _card05C,
            _card07D, _card08D, _card09D, _card10D,
            _cardJaH1, _cardJaH2, _cardJaS,
            _cardKiC, _cardKiD, _cardKiS
        };

        Dashita expectedDashitaWithJacks = new Dashita(_run02Cto05C, _run07Dto10D, _atamaJacksHHS);
        Dashita expectedDashitaWithKings = new Dashita(_run02Cto05C, _run07Dto10D, _atamaKings);

        HashSet<Dashita> dashitaOptions = DashitaGenerator.GenerateOptions(hand);

        Assert.AreEqual(
            2,
            dashitaOptions.Count,
            "Did not find correct number of dashita options"
        );

        List<Dashita> dashitaList = new(dashitaOptions);
        Assert.Contains(expectedDashitaWithJacks, dashitaList);
        Assert.Contains(expectedDashitaWithKings, dashitaList);
    }

    [Test]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public void RunsThatTouchAtama()
    {
        CardList hand = new()
        {
            _card02C, _card03C, _card04C, _card05C,
            _card07D, _card08D, _card09D, _card10D,
            _cardJaD,
            _cardJaH1, _cardJaH2, _cardJaS,
        };

        Dashita expectedDashita07Dto10DJacksDHH = new Dashita(_run02Cto05C, _run07Dto10D, _atamaJacksDHH);
        Dashita expectedDashita07Dto10DJacksDHS = new Dashita(_run02Cto05C, _run07Dto10D, _atamaJacksDHS);
        Dashita expectedDashita07Dto10DJacksHHS = new Dashita(_run02Cto05C, _run07Dto10D, _atamaJacksHHS);
        Dashita expectedDashita07Dto10DJacksDHHS = new Dashita(_run02Cto05C, _run07Dto10D, _atamaJacksDHHS);
        Dashita expectedDashita07DtoJaDJacksHHS = new Dashita(_run02Cto05C, _run07DtoJaD, _atamaJacksHHS);
        Dashita expectedDashita08DtoJaDJacksHHS = new Dashita(_run02Cto05C, _run08DtoJaD, _atamaJacksHHS);

        HashSet<Dashita> dashitaOptions = DashitaGenerator.GenerateOptions(hand);

        Assert.AreEqual(
            6,
            dashitaOptions.Count,
            "Did not find correct number of dashita options"
        );

        List<Dashita> dashitaList = new(dashitaOptions);
        Assert.Contains(expectedDashita07Dto10DJacksDHH, dashitaList);
        Assert.Contains(expectedDashita07Dto10DJacksDHS, dashitaList);
        Assert.Contains(expectedDashita07Dto10DJacksHHS, dashitaList);
        Assert.Contains(expectedDashita07Dto10DJacksDHHS, dashitaList);
        Assert.Contains(expectedDashita07DtoJaDJacksHHS, dashitaList);
        Assert.Contains(expectedDashita08DtoJaDJacksHHS, dashitaList);
    }

    [Test]
    public void SameRuns()
    {
        CardList hand = new()
        {
            _card02C, _card03C, _card04C, _card05C,
            _card02C, _card03C, _card04C, _card05C,
            _cardJaH1, _cardJaH2, _cardJaS,
        };

        Dashita expectedDashita = new Dashita(_run02Cto05C, _run02Cto05C, _atamaJacksHHS);


        HashSet<Dashita> dashitaOptions = DashitaGenerator.GenerateOptions(hand);


        Assert.AreEqual(1, dashitaOptions.Count);
        Assert.IsTrue(dashitaOptions.Contains(expectedDashita));
    }
}