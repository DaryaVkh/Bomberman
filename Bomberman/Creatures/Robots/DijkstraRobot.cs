using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace Bomberman
{
    public class DijkstraRobot : Robot
    {
        public override string GetImageFileName() => "DijkstraRobot.png";
        private const double MsBeforeGo = 180;

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

            var nextStep = FindNextStep(x, y);

            if (!CanMoveFinal(nextStep))
            {
                Game.WantToMoveRobot[x, y] = true;
                return new CreatureCommand();
            }

            Game.WantToMoveRobot[nextStep.X, nextStep.Y] = true;
            Position = nextStep;
            return new CreatureCommand { DeltaX = nextStep.X - x, DeltaY = nextStep.Y - y };
        }

        private static Point FindNextStep(int startX, int startY)
        {
            var start = new Point(startX, startY);
            var dist = new int[Game.MapWidth, Game.MapHeight];
            var prev = new Point?[Game.MapWidth, Game.MapHeight];

            for (var x = 0; x < Game.MapWidth; x++)
                for (var y = 0; y < Game.MapHeight; y++)
                    dist[x, y] = int.MaxValue;

            dist[startX, startY] = 0;

            var pq = new PriorityQueue<Point, int>();
            pq.Enqueue(start, 0);

            Point? playerPos = null;

            while (pq.Count > 0)
            {
                pq.TryDequeue(out var current, out var currentCost);

                if (currentCost > dist[current.X, current.Y])
                    continue;

                if (Game.Map[current.X, current.Y].OfType<Player>().Any())
                {
                    playerPos = current;
                    break;
                }

                foreach (var dir in Directions)
                {
                    var next = new Point(current.X + dir.X, current.Y + dir.Y);
                    if (!CanMove(next)) continue;

                    var newCost = dist[current.X, current.Y] + GetCellCost(next);
                    if (newCost >= dist[next.X, next.Y]) continue;

                    dist[next.X, next.Y] = newCost;
                    prev[next.X, next.Y] = current;
                    pq.Enqueue(next, newCost);
                }
            }

            if (playerPos == null)
                return start;

            // Идём назад по prev[], чтобы найти первый шаг от start
            var step = playerPos.Value;
            while (prev[step.X, step.Y].HasValue && prev[step.X, step.Y].Value != start)
                step = prev[step.X, step.Y].Value;

            return prev[step.X, step.Y].HasValue ? step : start;
        }

        // Клетки с огнём или рядом с бомбой дороже — робот предпочитает безопасные маршруты
        private static int GetCellCost(Point p)
        {
            var cell = Game.Map[p.X, p.Y];
            if (cell.OfType<Fire>().Any())
                return 20;

            foreach (var dir in Directions)
            {
                var nx = p.X + dir.X;
                var ny = p.Y + dir.Y;
                if (nx < 0 || nx >= Game.MapWidth || ny < 0 || ny >= Game.MapHeight) continue;
                var neighbor = Game.Map[nx, ny];
                if (neighbor.OfType<Bomb>().Any() || neighbor.OfType<Fire>().Any())
                    return 5;
            }

            return 1;
        }

        private static bool CanMove(Point p) =>
            p.X >= 0 && p.X < Game.MapWidth
            && p.Y >= 0 && p.Y < Game.MapHeight
            && !Game.Map[p.X, p.Y].ContainsObstaclesOrBomb()
            && !Game.Map[p.X, p.Y].ContainsForceField();

        private static bool CanMoveFinal(Point p) =>
            CanMove(p)
            && !Game.Map[p.X, p.Y].ContainsRobot()
            && !Game.WantToMoveRobot[p.X, p.Y];

        private static readonly Point[] Directions =
        {
            new Point(-1, 0), new Point(1, 0), new Point(0, -1), new Point(0, 1)
        };
    }
}
