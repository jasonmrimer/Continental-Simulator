using System;
using NUnit.Framework;

public class GameControllerTest
{
    private GameController _gameController;

    [SetUp]
    public void SetUp()
    {
        _gameController = new GameController();
    }

    [Test]
    public void PlayUntilAllCardsAreDrawnFromDeck()
    {
        _gameController.Play();
        Assert.IsTrue(
            _gameController.IsFinished(),
            "Should conclude after all cards drawn."
        );
        Assert.AreEqual(
            63,
            _gameController.TurnCount(),
            "A 4-player game should have 63 cards in deck thus 63 draws without Discard Pile"
        );
    }

    [Test]
    public void PlayWithDrawChoiceUntilTurnOneHundred()
    {
        _gameController = new GameController(drawChoiceEnabled: true);
        _gameController.Play();
    }
}