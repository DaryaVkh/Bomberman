using System;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using Bomberman;
using FluentAssertions;
using NUnit.Framework;

namespace TestProject
{
    [TestFixture]
    public class Bomb_Should
    {
        private const double SecondsBeforeExplosion = Bomb.secondsBeforeExplosion;
        private const double TimeGap = 0.05;
        
        private static GameState CreateGameState(string map) => new GameState(map);
        
        [Test]
        public void Bomb_GetImageFileName_RightImageName()
        {
            var bomb = new Bomb(new Player());
            bomb.GetImageFileName().Should().Be("Bomb.png");
        }

        [Test]
        public void Bomb_BombExplosion_BombExploded()
        {
            var testMap = @"
#####
#P  #
#   #
#   #
#####";
            var gameState = CreateGameState(testMap);
            var bomb = new Bomb(new Player());
            Game.Map[2, 2] = new ICreature[] { bomb };
            
            Game.Map[2, 2].Length.Should().Be(1);
            Game.Map[2, 2].First().Should().BeAssignableTo<Bomb>();
            
            var timer = Stopwatch.StartNew();
            var testTime = TimeGap + SecondsBeforeExplosion;
            
            while (timer.Elapsed <= TimeSpan.FromSeconds(testTime))
            {
                gameState.BeginAct();
                gameState.EndAct();
            }

            Game.Map[2, 2].Should().BeEmpty();
            Game.Map[2, 2].Should().NotContain(bomb);
        }
        
        [Test]
        public void Bomb_BombExplosion_BombTransformToFiresArray()
        {
            var testMap = @"
#####
#P  #
#   #
#   #
#####";
            var gameState = CreateGameState(testMap);
            
            var bomb = new Bomb(new Player());
            Game.Map[2, 2] = new ICreature[] { bomb };

            var timer = Stopwatch.StartNew();
            var explosionDeadline = TimeSpan.FromSeconds(TimeGap + SecondsBeforeExplosion);
            
            while (Game.Map[2, 2].OfType<Bomb>().Any() && timer.Elapsed <= explosionDeadline)
            {
                gameState.BeginAct();
                gameState.EndAct();
            }

            Game.Map[2, 2].Length.Should().Be(4);
            Game.Map[2, 2].Should().NotContain(bomb);
            Game.Map[2, 2].Should().AllBeAssignableTo<Fire>();
        }
        
        [Test]
        public void Bomb_BombExplosion_BombTransformToFires()
        {
            var testMap = @"
#####
#P  #
#   #
#   #
#####";
            var gameState = CreateGameState(testMap);
            Game.Map[2, 2] = new ICreature[] { new Bomb(new Player()) };

            var timer = Stopwatch.StartNew();
            var explosionDeadline = TimeSpan.FromSeconds(TimeGap + SecondsBeforeExplosion);
            
            while (Game.Map[2, 2].OfType<Bomb>().Any() && timer.Elapsed <= explosionDeadline)
            {
                gameState.BeginAct();
                gameState.EndAct();
            }
            
            gameState.BeginAct();
            gameState.EndAct();

            Game.Map[2, 2].Should().BeEmpty();
            Game.Map[2, 1].Length.Should().Be(1);
            Game.Map[2, 3].Length.Should().Be(1);
            Game.Map[1, 2].Length.Should().Be(1);
            Game.Map[3, 2].Length.Should().Be(1);
            Game.Map[2, 1].First().Should().BeAssignableTo<Fire>();
            Game.Map[2, 3].First().Should().BeAssignableTo<Fire>();
            Game.Map[1, 2].First().Should().BeAssignableTo<Fire>();
            Game.Map[3, 2].First().Should().BeAssignableTo<Fire>();
        }

        [Test]
        public void Bomb_BombConflictedObjectFire_BombExploded()
        {
            var testMap = @"
#######
#    P#
#######";
            var gameState = CreateGameState(testMap);
            Game.Map[1, 1] = new ICreature[] { new Fire(1, Direction.Right) };
            Game.Map[2, 1] = new ICreature[] { new Bomb(new Player()) };

            var timer = Stopwatch.StartNew();
            var explosionDeadline = TimeSpan.FromSeconds(TimeGap + SecondsBeforeExplosion);
            
            while (Game.Map[2, 1].OfType<Bomb>().Any() && timer.Elapsed <= explosionDeadline)
            {
                gameState.BeginAct();
                gameState.EndAct();
            }
            
            gameState.BeginAct();
            gameState.EndAct();

            Game.Map[2, 1].Should().BeEmpty();
            Game.Map[1, 1].Length.Should().Be(1);
            Game.Map[1, 1].First().Should().BeAssignableTo<Fire>();
            Game.Map[3, 1].Length.Should().Be(1);
            Game.Map[3, 1].First().Should().BeAssignableTo<Fire>();
        }
    }
}