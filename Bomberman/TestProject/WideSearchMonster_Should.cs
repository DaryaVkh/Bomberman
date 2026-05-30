using System;
using System.Diagnostics;
using System.Linq;
using Bomberman;
using FluentAssertions;
using NUnit.Framework;

namespace TestProject
{
    [TestFixture]
    public class WideSearchMonster_Should
    {
        private const double MonsterThinkingTime = 0.21;
        private const double TimeGap = 0.05;

        private static GameState CreateGameState(string map) => new GameState(map);

        [Test]
        public void WideSearchMonster_GetImageFileName_RightImageName()
        {
            var monster = new WideSearchMonster();
            monster.GetImageFileName().Should().Be("WideSearchMonster.png");
        }

        [TestCase("#####\r\n#3 P#\r\n#####", 1, 1, 2, 1)]
        [TestCase("#####\r\n#P 3#\r\n#####", 3, 1, 2, 1)]
        public void WideSearchMonster_PlayerNearby_MonsterMovesTowardsPlayer(
            string testMap, int xWas, int yWas, int x, int y)
        {
            var gameState = CreateGameState(testMap);
            var timer = Stopwatch.StartNew();

            while (timer.Elapsed <= TimeSpan.FromSeconds(MonsterThinkingTime + TimeGap))
            {
                gameState.BeginAct();
                gameState.EndAct();
            }

            Game.Map[xWas, yWas].Should().BeEmpty();
            Game.Map[x, y].OfType<WideSearchMonster>().Should().HaveCount(1);
        }

        // Робот заперт стенами — игрок в отдельном отсеке, недостижим
        [TestCase("#######\r\n###3###\r\n#######\r\n###P###\r\n#######")]
        [TestCase("#######\r\nWWW3WWW\r\n#######\r\n###P###\r\n#######")]
        public void WideSearchMonster_SurroundedByWalls_MonsterStaysInPlace(string testMap)
        {
            var gameState = CreateGameState(testMap);
            var timer = Stopwatch.StartNew();

            while (timer.Elapsed <= TimeSpan.FromSeconds(MonsterThinkingTime * 2 + TimeGap))
            {
                gameState.BeginAct();
                gameState.EndAct();
            }

            Game.Map[3, 1].Length.Should().Be(1);
            Game.Map[3, 1].First().Should().BeAssignableTo<WideSearchMonster>();
        }

        [Test]
        public void WideSearchMonster_HitByFire_MonsterDies()
        {
            var gameState = CreateGameState("#######\r\n# 3  P#\r\n#######");
            Game.Map[2, 1] = new ICreature[] { new Fire(1, Direction.Right) };
            var timer = Stopwatch.StartNew();

            while (timer.Elapsed <= TimeSpan.FromSeconds(TimeGap))
            {
                gameState.BeginAct();
                gameState.EndAct();
            }

            Game.Map[2, 1].OfType<WideSearchMonster>().Should().BeEmpty();
        }

        [Test]
        public void WideSearchMonster_ObstacleBetween_MonsterGoesAround()
        {
            var testMap =
                "#####\r\n" +
                "#3W #\r\n" +
                "#  P#\r\n" +
                "#####";

            var gameState = CreateGameState(testMap);
            var timer = Stopwatch.StartNew();

            while (timer.Elapsed <= TimeSpan.FromSeconds(MonsterThinkingTime + TimeGap))
            {
                gameState.BeginAct();
                gameState.EndAct();
            }

            Game.Map[1, 1].OfType<WideSearchMonster>().Should().BeEmpty();
        }

        [Test]
        public void WideSearchMonster_TwoPaths_MonsterTakesShorterOne()
        {
            var testMap =
                "#####\r\n" +
                "#3  #\r\n" +
                "#   #\r\n" +
                "#P  #\r\n" +
                "#####";

            var gameState = CreateGameState(testMap);
            var timer = Stopwatch.StartNew();

            while (timer.Elapsed <= TimeSpan.FromSeconds(MonsterThinkingTime + TimeGap))
            {
                gameState.BeginAct();
                gameState.EndAct();
            }

            Game.Map[1, 2].OfType<WideSearchMonster>().Should().HaveCount(1);
        }
    }
}
