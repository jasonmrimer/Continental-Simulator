using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework;

[TestFixture]
public class DashitaTest
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
    private CardList _run02Cto05C;
    private CardList _run07Dto10D;
    private CardList _atamaJacks;

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

        _run02Cto05C = new CardList
        {
            _card02C,
            _card03C,
            _card04C,
            _card05C
        };

        _run07Dto10D = new CardList()
        {
            _card07D,
            _card08D,
            _card09D,
            _card10D
        };

        _atamaJacks = new CardList()
        {
            _cardJaH1,
            _cardJaH2,
            _cardJaS,
        };
    }

    [Test]
    public void Equals()
    {
        Dashita dashita1 = new Dashita(
            new List<CardList> { _run02Cto05C, _run07Dto10D },
            _atamaJacks
        );

        Dashita dashita2 = new Dashita(
            new List<CardList> { _run02Cto05C, _run07Dto10D },
            _atamaJacks
        );

        Assert.AreEqual(dashita1, dashita2);
    }

    [Test]
    public void NotEquals()
    {
        CardList run02Cto06C = new(_run02Cto05C);
        run02Cto06C.Add(new Card(Rank.Six, Suit.Clubs));

        Dashita dashita1 = new Dashita(
            new List<CardList> { run02Cto06C, _run07Dto10D },
            _atamaJacks
        );

        Dashita dashita2 = new Dashita(
            new List<CardList> { _run02Cto05C, _run07Dto10D },
            _atamaJacks
        );

        Assert.AreNotEqual(dashita1, dashita2);
    }

    [Test]
    public void EqualsWithRunOrderChange()
    {
        Dashita dashita1 = new Dashita(
            new List<CardList> { _run07Dto10D, _run02Cto05C },
            _atamaJacks
        );

        Dashita dashita2 = new Dashita(
            new List<CardList> { _run02Cto05C, _run07Dto10D },
            _atamaJacks
        );

        Assert.AreEqual(dashita1, dashita2);
    }

    [Test]
    public void EqualsWithAtamaOrderChange()
    {
        CardList atamaJacksMix = new()
        {
            _cardJaH2, _cardJaS, _cardJaH1
        };

        Dashita dashita1 = new Dashita(
            new List<CardList> { _run07Dto10D, _run02Cto05C },
            _atamaJacks
        );

        Dashita dashita2 = new Dashita(
            new List<CardList> { _run02Cto05C, _run07Dto10D },
            atamaJacksMix
        );

        Assert.AreEqual(dashita1, dashita2);
    }
}