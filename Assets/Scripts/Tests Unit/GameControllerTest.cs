using System;
using NUnit.Framework;

public class GameControllerTest
{
    private GameController gameController;

    [SetUp]
    public void SetUp()
    {
    }

    [Test]
    public void PlayUntilAllCardsAreDrawnFromDeck()
    {
        gameController = new GameController(drawChoiceEnabled: false);
        gameController.Play();
        Assert.IsTrue(
            gameController.IsFinished(),
            "Should conclude after all cards drawn."
        );
        Assert.AreEqual(
            63,
            gameController.TurnCount(),
            "A 4-player game should have 63 cards in deck thus 63 draws without Discard Pile"
        );
    }

    [Test]
    public void PlayHundredTurnsDeckOnlyTestRecycle()
    {
        gameController = new GameController(
            drawChoiceEnabled: false,
            turnLimit: 100
        );
        gameController.Play();
        
        Assert.IsTrue(
            gameController.IsFinished(),
            "Should conclude after 100 turns."
        );
        Assert.AreEqual(
            100,
            gameController.TurnCount(),
            "Expect an error from depleting the deck or pile."
        );
    }

    [Test]
    public void PlayWithDrawChoiceUntilTurnOneHundred()
    {
        gameController = new GameController(
            drawChoiceEnabled: true,
            turnLimit: 100
        );
        gameController.Play();
        Assert.IsTrue(
            gameController.IsFinished(),
            "Should conclude after 100 turns."
        );
        Assert.AreEqual(
            100,
            gameController.TurnCount(),
            "Random-choice deck/pile set to 100 turns."
        );
    }
}