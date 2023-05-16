using System;
using NUnit.Framework;

public class GameControllerTest
{
    private GameController _gameController;

    [Test]
    public void PlayThousandTurnsDeckOnlyTestRecycle()
    {
        _gameController = new GameController(turnLimit: 1000);
        _gameController.Play();

        Assert.IsTrue(
            _gameController.IsFinished(),
            "Should conclude after 1000 turns."
        );
        Assert.AreEqual(
            1000,
            _gameController.TurnCount(),
            "Expect an error from depleting the deck or pile."
        );
    }

    [Test]
    public void PlayWithDrawChoiceUntilTurnOneHundred()
    {
        _gameController = new GameController(turnLimit: 100);
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