using System;
using NUnit.Framework;

public class GameControllerTest
{
    [Test]
    public void PlayUntilAllCardsAreDrawnFromDeck()
    {
        GameController gameController = new GameController();
        gameController.Play();
        Console.Write(("hwwwwwww"));
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
}