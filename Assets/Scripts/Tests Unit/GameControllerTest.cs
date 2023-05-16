using System;
using NUnit.Framework;

public class GameControllerTest
{
    private GameController _gameController;

    [Test]
    public void PlayHundredTurnsDeckOnlyTestRecycle()
    {
        _gameController = new GameController(
            drawChoiceEnabled: false,
            turnLimit: 100
        );
        _gameController.Play();
        
        Assert.IsTrue(
            _gameController.IsFinished(),
            "Should conclude after 100 turns."
        );
        Assert.AreEqual(
            100,
            _gameController.TurnCount(),
            "Expect an error from depleting the deck or pile."
        );
    }

    [Test]
    public void PlayWithDrawChoiceUntilTurnOneHundred()
    {
        _gameController = new GameController(
            drawChoiceEnabled: true,
            turnLimit: 100
        );
        _gameController.Play();
        Assert.IsTrue(
            _gameController.IsFinished(),
            "Should conclude after 100 turns."
        );
        Assert.AreEqual(
            100,
            _gameController.TurnCount(),
            "Random-choice deck/pile set to 100 turns."
        );
    }
}