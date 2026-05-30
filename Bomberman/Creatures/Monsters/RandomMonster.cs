using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace Bomberman
{
    public class RandomMonster : Monster
    {
        public override string GetImageFileName() => "RandomMonster.png";
        private Point? direction;
        private const double msBeforeGo = 500;
        private readonly Random random = new Random();
        
        public override CreatureCommand Act(int x, int y)
        {
            Position = new Point(x, y);
            if (Timer.ElapsedMilliseconds < msBeforeGo)
            {
                Game.WantToMoveMonster[x, y] = true;
                return new CreatureCommand();
            }

            Timer = Stopwatch.StartNew();
            Game.WantToMoveMonster[x, y] = false;
            var command = GetOptimalMove(x, y);
            Game.WantToMoveMonster[x + command.DeltaX, y + command.DeltaY] = true;
            Position = new Point(x + command.DeltaX, y + command.DeltaY);

            return command;
        }

        private CreatureCommand GetOptimalMove(int x, int y)
        {
            direction ??= AllDirections[random.Next(4)];

            if (random.NextDouble() > 0.5 && CanMoveFinal(GetNewPosition(new Point(x, y), direction.Value)))
                return new CreatureCommand {DeltaX = direction.Value.X, DeltaY = direction.Value.Y};

            var possibleMoves = new List<Point>{ new Point(0, 0) };

            foreach (var newDirection in AllDirections)
            {
                if (CanMoveFinal(GetNewPosition(new Point(x, y), newDirection)))
                    possibleMoves.Add(newDirection);
            }

            direction = possibleMoves[random.Next(possibleMoves.Count)];
            
            return new CreatureCommand {DeltaX = direction.Value.X, DeltaY = direction.Value.Y};
        }

        private static Point GetNewPosition(Point old, Point direction) => new Point(old.X + direction.X, old.Y + direction.Y);

        private static bool CanMove(Point point)
        {
            return point.X >= 0
                   && point.X < Game.MapWidth
                   && point.Y >= 0 && point.Y < Game.MapHeight
                   && !Game.Map[point.X, point.Y].ContainsForceField()
                   && !Game.Map[point.X, point.Y].ContainsObstaclesOrBomb();
        }
        
        private static bool CanMoveFinal(Point point)
        {
            return CanMove(point)
                   && !Game.Map[point.X, point.Y].ContainsMonster()
                   && !Game.WantToMoveMonster[point.X, point.Y];
        }

        private readonly Point[] AllDirections = {
            new Point(-1, 0), new Point(1, 0), new Point(0, -1), new Point(0, 1)
        };
    }
}