using System;
using NUnit.Framework;

public class GameControllerTest
{
    [Test]
    public void Play1000TurnsOrUntilDiscardPileEmpty()
    {
        _gameController = new GameController(turnLimit: 1000);
        _gameController.Play();

        Assert.IsTrue(
            _gameController.IsFinished(),
            "Should conclude after 1000 turns."
        );
        
        if (_gameController.Dealer.PileCardCount() > 1 || _gameController.Dealer.DeckCardCount() > 1)
        {
            
            Assert.LessOrEqual(
                1000,
                _gameController.TurnCount(),
                "Expect an error from depleting the deck or pile."
            ); 
        }
        else
        {
            Assert.AreEqual(
                1000,
                _gameController.TurnCount(),
                "Expect an error from depleting the deck or pile."
            ); 
        }
       
    }

    private GameController _gameController;

    // players take random action

    // until game is over

    // game can end by player discarding or playing final card

    // game can end by only 1 remaining discard pile and all players get one turn

    // or 1000 turns without discard pile empty


    [Test]
    [Ignore("")]
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