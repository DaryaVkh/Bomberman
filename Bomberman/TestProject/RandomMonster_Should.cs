using System;
using System.Diagnostics;
using System.Linq;
using Bomberman;
using FluentAssertions;
using NUnit.Framework;

namespace TestProject
{
    [TestFixture]
    public class RandomMonster_Should
    {
        private const double MonsterThinkingTime = 0.5;
        private const double TimeGap = 0.05;

        private static GameState CreateGameState(string map) => new GameState(map);

        [Test]
        public void RandomMonster_GetImageFileName_RightImageName()
        {
            var monster = new RandomMonster();
            monster.GetImageFileName().Should().Be("RandomMonster.png");
        }
        
        [Test]
        public void RandomMonster_ConflictedObjectFire_MonsterDied()
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

            Game.Map[1, 1].Should().BeEmpty();
            Game.Map[2, 1].Should().BeEmpty();
            Game.Map[3, 1].Should().BeEmpty();
        }

        [TestCase("###\r\n#1#\r\n###\r\n#P#\r\n###")]
        [TestCase("#W#\r\nW1W\r\nWWW\r\nWPW\r\n#W#")]
        public void RandomMonster_GoThroughWalls_MonsterCantGoThroughWalls(string testMap)
        {
            var gameState = CreateGameState(testMap);
            var timer = Stopwatch.StartNew();

            while (timer.Elapsed <= TimeSpan.FromSeconds(MonsterThinkingTime * 2 + TimeGap))
            {
                gameState.BeginAct();
                gameState.EndAct();
            }

            Game.Map[1, 1].Length.Should().Be(1);
            Game.Map[1, 1].First().Should().BeAssignableTo<RandomMonster>();
        }
    }
}
