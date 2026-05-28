using System;
using System.Diagnostics;
using System.Linq;
using Bomberman;
using FluentAssertions;
using NUnit.Framework;

namespace TestProject
{
    [TestFixture]
    public class DijkstraRobot_Should
    {
        private const double RobotThinkingTime = 0.2;
        private const double TimeGap = 0.05;

        [Test]
        public void DijkstraRobot_GetImageFileName_RightImageName()
        {
            var robot = new DijkstraRobot();
            robot.GetImageFileName().Should().Be("DijkstraRobot.png");
        }

        // Робот должен двигаться к игроку по прямому пути
        [TestCase("#####\r\n#4 P#\r\n#####", 1, 1, 2, 1)]
        [TestCase("#####\r\n#P 4#\r\n#####", 3, 1, 2, 1)]
        public void DijkstraRobot_PlayerNearby_RobotMovesTowardsPlayer(
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
            Game.Map[x, y].OfType<DijkstraRobot>().Should().HaveCount(1);
        }

        // Робот не должен проходить сквозь стены
        [TestCase("###\r\n#4#\r\n###")]
        [TestCase("#W#\r\nW4W\r\n#W#")]
        public void DijkstraRobot_SurroundedByWalls_RobotStaysInPlace(string testMap)
        {
            TestMapHelper.CreateMap(testMap);
            var gameState = new GameState();
            var timer = Stopwatch.StartNew();

            while (timer.Elapsed <= TimeSpan.FromSeconds(RobotThinkingTime * 2 + TimeGap))
            {
                gameState.BeginAct();
                gameState.EndAct();
            }

            Game.Map[1, 1].Length.Should().Be(1);
            Game.Map[1, 1].First().Should().BeAssignableTo<DijkstraRobot>();
        }

        // Робот должен умереть, попав в огонь
        [Test]
        public void DijkstraRobot_HitByFire_RobotDies()
        {
            TestMapHelper.CreateMap("#####\r\n# 4 #\r\n#####");
            Game.Map[2, 1] = new ICreature[] { new Fire(1, Direction.Right) };
            var gameState = new GameState();
            var timer = Stopwatch.StartNew();

            while (timer.Elapsed <= TimeSpan.FromSeconds(TimeGap))
            {
                gameState.BeginAct();
                gameState.EndAct();
            }

            Game.Map[2, 1].OfType<DijkstraRobot>().Should().BeEmpty();
        }

        // Робот должен обойти препятствие, чтобы добраться до игрока
        [Test]
        public void DijkstraRobot_ObstacleBetween_RobotGoesAround()
        {
            // Карта: робот слева, стена посередине, игрок справа — путь снизу
            var testMap =
                "#####\r\n" +
                "#4W #\r\n" +
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

            // Робот должен покинуть стартовую позицию
            Game.Map[1, 1].OfType<DijkstraRobot>().Should().BeEmpty();
        }
    }
}
