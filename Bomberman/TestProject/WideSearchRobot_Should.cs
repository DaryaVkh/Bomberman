using System;
using System.Diagnostics;
using System.Linq;
using Bomberman;
using FluentAssertions;
using NUnit.Framework;

namespace TestProject
{
    [TestFixture]
    public class WideSearchRobot_Should
    {
        private const double RobotThinkingTime = 0.21;
        private const double TimeGap = 0.05;

        [Test]
        public void WideSearchRobot_GetImageFileName_RightImageName()
        {
            var robot = new WideSearchRobot();
            robot.GetImageFileName().Should().Be("WideSearchRobot.png");
        }

        [TestCase("#####\r\n#3 P#\r\n#####", 1, 1, 2, 1)]
        [TestCase("#####\r\n#P 3#\r\n#####", 3, 1, 2, 1)]
        public void WideSearchRobot_PlayerNearby_RobotMovesTowardsPlayer(
            string testMap, int xWas, int yWas, int x, int y)
        {
            Game.CreateMap(testMap);
            var gameState = new GameState();
            var timer = Stopwatch.StartNew();

            while (timer.Elapsed <= TimeSpan.FromSeconds(RobotThinkingTime + TimeGap))
            {
                gameState.BeginAct();
                gameState.EndAct();
            }

            Game.Map[xWas, yWas].Should().BeEmpty();
            Game.Map[x, y].OfType<WideSearchRobot>().Should().HaveCount(1);
        }

        // Робот заперт стенами — игрок в отдельном отсеке, недостижим
        [TestCase("#######\r\n###3###\r\n#######\r\n###P###\r\n#######")]
        [TestCase("#######\r\nWWW3WWW\r\n#######\r\n###P###\r\n#######")]
        public void WideSearchRobot_SurroundedByWalls_RobotStaysInPlace(string testMap)
        {
            Game.CreateMap(testMap);
            var gameState = new GameState();
            var timer = Stopwatch.StartNew();

            while (timer.Elapsed <= TimeSpan.FromSeconds(RobotThinkingTime * 2 + TimeGap))
            {
                gameState.BeginAct();
                gameState.EndAct();
            }

            Game.Map[3, 1].Length.Should().Be(1);
            Game.Map[3, 1].First().Should().BeAssignableTo<WideSearchRobot>();
        }

        // Игрок в стороне, робот на позиции с огнём — должен умереть
        [Test]
        public void WideSearchRobot_HitByFire_RobotDies()
        {
            Game.CreateMap("#######\r\n# 3  P#\r\n#######");
            Game.Map[2, 1] = new ICreature[] { new Fire(1, Direction.Right) };
            var gameState = new GameState();
            var timer = Stopwatch.StartNew();

            while (timer.Elapsed <= TimeSpan.FromSeconds(TimeGap))
            {
                gameState.BeginAct();
                gameState.EndAct();
            }

            Game.Map[2, 1].OfType<WideSearchRobot>().Should().BeEmpty();
        }

        [Test]
        public void WideSearchRobot_ObstacleBetween_RobotGoesAround()
        {
            var testMap =
                "#####\r\n" +
                "#3W #\r\n" +
                "#  P#\r\n" +
                "#####";

            Game.CreateMap(testMap);
            var gameState = new GameState();
            var timer = Stopwatch.StartNew();

            while (timer.Elapsed <= TimeSpan.FromSeconds(RobotThinkingTime * 3 + TimeGap))
            {
                gameState.BeginAct();
                gameState.EndAct();
            }

            Game.Map[1, 1].OfType<WideSearchRobot>().Should().BeEmpty();
        }

        [Test]
        public void WideSearchRobot_TwoPaths_RobotTakesShorterOne()
        {
            var testMap =
                "#####\r\n" +
                "#3  #\r\n" +
                "#P  #\r\n" +
                "#####";

            Game.CreateMap(testMap);
            var gameState = new GameState();
            var timer = Stopwatch.StartNew();

            while (timer.Elapsed <= TimeSpan.FromSeconds(RobotThinkingTime + TimeGap))
            {
                gameState.BeginAct();
                gameState.EndAct();
            }

            Game.Map[1, 2].OfType<WideSearchRobot>().Should().HaveCount(1);
        }
    }
}
