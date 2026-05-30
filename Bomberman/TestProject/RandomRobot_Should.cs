using System;
using System.Diagnostics;
using System.Linq;
using Bomberman;
using FluentAssertions;
using NUnit.Framework;

namespace TestProject
{
    [TestFixture]
    public class RandomRobot_Should
    {
        private const double RobotThinkingTime = 0.5;
        private const double TimeGap = 0.05;

        private static GameState CreateGameState(string map) => new GameState(map);

        [Test]
        public void RandomRobot_GetImageFileName_RightImageName()
        {
            var robot = new RandomRobot();
            robot.GetImageFileName().Should().Be("RandomRobot.png");
        }
        
        [Test]
        public void RandomRobot_ConflictedObjectFire_RobotDied()
        {
            var testMap = @"
#######
# 1  P#
#######";
            var gameState = CreateGameState(testMap);
            Game.Map[1, 1] = new ICreature[] { new Fire(1, Direction.Right) };

            var timer = Stopwatch.StartNew();
            while (timer.Elapsed <= TimeSpan.FromSeconds(TimeGap))
            {
                gameState.BeginAct();
                gameState.EndAct();
            }

            HasRandomRobot().Should().BeFalse();
        }

        [TestCase("###\r\n#1#\r\n###\r\n#P#\r\n###")]
        [TestCase("#W#\r\nW1W\r\nWWW\r\nWPW\r\n#W#")]
        public void RandomRobot_GoThroughWalls_RobotCantGoThroughWalls(string testMap)
        {
            var gameState = CreateGameState(testMap);
            var timer = Stopwatch.StartNew();

            while (timer.Elapsed <= TimeSpan.FromSeconds(RobotThinkingTime * 2 + TimeGap))
            {
                gameState.BeginAct();
                gameState.EndAct();
            }

            Game.Map[1, 1].Length.Should().Be(1);
            Game.Map[1, 1].First().Should().BeAssignableTo<RandomRobot>();
        }
    }
}
