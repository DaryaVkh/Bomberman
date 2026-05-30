using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace Bomberman
{
    public class WideSearchRobot : Robot
    {
        public override string GetImageFileName() => "WideSearchRobot.png";
        internal const double MsBeforeGo = 200;
        private static readonly Random random = new Random();

        public override CreatureCommand Act(int x, int y)
        {
            Position = new Point(x, y);
            if (Timer.ElapsedMilliseconds < MsBeforeGo)
            {
                Game.WantToMoveRobot[x, y] = true;
                return new CreatureCommand();
            }

            Timer = Stopwatch.StartNew();
            Game.WantToMoveRobot[x, y] = false;
            var newPosition = GetOptimalMove(x, y);
            if (!CanMoveFinal(newPosition))
            {
                Game.WantToMoveRobot[x, y] = true;
                return new CreatureCommand();
            }
            Game.WantToMoveRobot[newPosition.X, newPosition.Y] = true;
            var command = new CreatureCommand{DeltaX = newPosition.X - x, DeltaY = newPosition.Y - y};
            Position = newPosition;

            return command;
        }

        private Point GetOptimalMove(int x, int y)
        {
            var queue = new Queue<(Point, Point?)>();
            queue.Enqueue((new Point(x, y), null));
            var visited = new HashSet<Point> {new Point(x, y)};

            while (queue.Count > 0)
            {
                var (point, first) = queue.Dequeue();
                
                if (Game.Map[point.X, point.Y].Any(c => c is Player))
                    return first.Value;

                foreach (var direction in AllDirections)
                {
                    var newPoint = GetNewPosition(point, direction);

                    if (visited.Contains(newPoint) || !CanMove(newPoint)) continue;

                    visited.Add(newPoint);
                    
                    if (Game.Map[newPoint.X, newPoint.Y].Any(c => c is Player))
                        return first ?? newPoint;
                        
                    queue.Enqueue((newPoint, first ?? newPoint));
                }
            }

            return GetNewPosition(new Point(x, y), AllDirections[random.Next(4)]);
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
                   && !Game.Map[point.X, point.Y].ContainsRobot()
                   && !Game.WantToMoveRobot[point.X, point.Y];
        }

        private readonly Point[] AllDirections = {
            new Point(-1, 0), new Point(1, 0), new Point(0, -1), new Point(0, 1)
        };
    }
}