using System;
using System.Diagnostics;
using System.IO;
using System.Media;

namespace Bomberman
{
    public class Bomb : ICreatureWithTimer
    {
        private readonly Player player;
        private readonly Stopwatch timer;
        private bool shouldExplode;
        public const double secondsBeforeExplosion = 2;
        private static readonly string soundFile = Path.Combine(Program.SoundsPath, "bomb.wav");
        
        public Bomb(Player player)
        {
            this.player = player;
            timer = Stopwatch.StartNew();
        }

        public string GetImageFileName() => "Bomb.png";

        public CreatureCommand Act(int x, int y)
        {
            if (timer.Elapsed >= TimeSpan.FromSeconds(secondsBeforeExplosion))
                shouldExplode = true;
            if (shouldExplode)
            {
                if (Program.EnableSound && File.Exists(soundFile))
                {
                    new SoundPlayer(soundFile).Play();
                }
                
                Game.WantToMoveMonster[x, y] = false;
                player.CurrentBombs--;
                return 
                    new CreatureCommand { TransformTo =
                        new[] {
                            new Fire(player.SplashLimit, Direction.Up), 
                            new Fire(player.SplashLimit, Direction.Down), 
                            new Fire(player.SplashLimit, Direction.Right), 
                            new Fire(player.SplashLimit, Direction.Left) } };
            }
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Fire)
                shouldExplode = true;
            return false;
        }

        public void Pause() => timer.Stop();

        public void Unpause() => timer.Start();

        public int GetDrawingPriority() => 6;
    }
}