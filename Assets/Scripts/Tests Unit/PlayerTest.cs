using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
public class PlayerTest
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
        _player = new Player("Alice");
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

        _run02Cto05C = new Run { _card02C, _card03C, _card04C, _card05C };
        _run07Dto10D = new Run { _card07D, _card08D, _card09D, _card10D };
        _atamaJacks = new Atama { _cardJaH1, _cardJaH2, _cardJaS, };
    }

    [Test]
    public void PrintHand()
    {
        GivePlayerSmallDisconnectedHand();
        Assert.AreEqual(
            "2♣ | 7♦ | Q♥ | A♠ | Jo★",
            _player.FormatHandForPrint()
        );
    }

    private void GivePlayerSmallDisconnectedHand()
    {
        _player.AddToHand(new Card(Rank.Two, Suit.Clubs));
        _player.AddToHand(new Card(Rank.Seven, Suit.Diamonds));
        _player.AddToHand(new Card(Rank.Queen, Suit.Hearts));
        _player.AddToHand(new Card(Rank.Ace, Suit.Spades));
        _player.AddToHand(new Card(Rank.Joker, Suit.Wild));
    }

    [Test]
    public void Discard()
    {
        GivePlayerSmallDisconnectedHand();
        Assert.AreEqual(5, _player.CardCount());
        _player.DiscardFromHand();
        Assert.AreEqual(4, _player.CardCount());
    }

    [Test]
    public void ChooseDrawSourceAtRandom()
    {
        _player.AddToHand(new Card(Rank.Two, Suit.Clubs));
        _player.AddToHand(new Card(Rank.Seven, Suit.Diamonds));
        _player.AddToHand(new Card(Rank.Queen, Suit.Hearts));
        _player.AddToHand(new Card(Rank.Ace, Suit.Spades));
        _player.AddToHand(new Card(Rank.Joker, Suit.Wild));

        // Card topOfPile = new Card(CardValue.Eight, Suit.Spades);
        HashSet<DrawSource> chosenSources = new HashSet<DrawSource>();
        int choiceCount = 1;

        while (chosenSources.Count < 2 && choiceCount < 100)
        {
            chosenSources.Add(Player.ChooseDrawSource(pileIsAvailable: true));
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
        GivePlayerSmallDisconnectedHand();
        _player.DiscardFromHand();
        Assert.AreEqual(4, _player.CardCount());
    }

    [Test]
    public void PlayerWillDashitaRandomly()
    {
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

        Dashita expectedDashita = new(
            new List<Run> { _run02Cto05C, _run07Dto10D },
            _atamaJacks
        );


        int choiceCount = 0;
        Dashita playedDashita = null;
        while (playedDashita == null && choiceCount < 100)
        {
            playedDashita = _player.PlayDecision();
            choiceCount++;
        }

        Assert.IsTrue(_player.HasPlayedDashita);
        Assert.AreEqual(
            expectedDashita,
            playedDashita
        );
        /*
         setup hand with dashita possible
         run loop until dashita played
         check that dashita played TRUE
         */
    }
}