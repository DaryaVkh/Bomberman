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

        // Робот должен двигаться к игроку по прямому пути
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

        // Робот не должен проходить сквозь стены
        [TestCase("#####\r\n#3#P#\r\n#####")]
        [TestCase("####\r\nW3W#\r\n#P##")]
        public void WideSearchRobot_SurroundedByWalls_RobotStaysInPlace(string testMap)
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
            Game.Map[1, 1].First().Should().BeAssignableTo<WideSearchRobot>();
        }

        // Робот должен умереть, попав в огонь
        [Test]
        public void WideSearchRobot_HitByFire_RobotDies()
        {
            TestMapHelper.CreateMap("#####\r\n# 3 #\r\n#####");
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

        // Робот должен обойти препятствие — ключевое для BFS
        [Test]
        public void WideSearchRobot_ObstacleBetween_RobotGoesAround()
        {
            // Карта: робот слева, стена посередине, игрок справа — путь снизу
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

            // Робот должен покинуть стартовую позицию
            Game.Map[1, 1].OfType<WideSearchRobot>().Should().BeEmpty();
        }

        // BFS всегда находит кратчайший путь: робот не должен делать лишних шагов
        [Test]
        public void WideSearchRobot_TwoPaths_RobotTakesShorterOne()
        {
            // Два пути к игроку: короткий (вниз 1 шаг) и длинный (вправо 3 шага)
            // Робот должен пойти вниз
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

            // После одного хода робот должен быть прямо над игроком (1,2)
            Game.Map[1, 2].OfType<WideSearchRobot>().Should().HaveCount(1);
        }
    }
}
