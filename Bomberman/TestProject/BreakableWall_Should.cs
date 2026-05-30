using Bomberman;
using FluentAssertions;
using NUnit.Framework;

namespace TestProject
{
    [TestFixture]
    public class BreakableWall_Should
    {
        private static GameState CreateGameState(string map) => new GameState(map);
        
        [Test]
        public void BreakableWall_GetImageFileName_RightImageName()
        {
            var wall = new BreakableWall();
            wall.GetImageFileName().Should().Be("BreakableWall.png");
        }
        
        [Test]
        public void BreakableWall_ConflictedObjectFire_BreakableWallBroke()
        {

            var testMap = @"
####
#W #
#P #
####";
            
            var gameState = CreateGameState(testMap);
            Game.Map[2, 1] = new ICreature[] { new Fire(1, Direction.Left) };
            
            gameState.BeginAct();
            gameState.EndAct();

            Game.Map[1, 1].Should().BeEmpty();
        }
    }
}