using NUnit.Framework;

namespace Game.Tests_Unit
{
    [TestFixture]
    public class GameTest
    {
        [Test]
        public void PlayUntilAllCardsAreDrawn()
        {
            Game game = new Game();
            game.Play();
            
            // Assert.IsTrue(game.isFinished());
         // start game
         // draw until exhausted
         // check if finished
        }
    }
}