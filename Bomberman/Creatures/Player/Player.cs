using System.IO;
using System.Media;
using System.Windows.Forms;

namespace Bomberman
{
    public class Player : ICreature
    {
        public int BombsLimit = 1;
        public int SplashLimit = 1;
        public int CurrentBombs;
        private string ImageName = "running-right.png";
        private static readonly string soundFile = Path.Combine(Program.SoundsPath, "death.wav");
        
        private void ChangeImageName(Keys keys)
        {
            if (keys == Keys.None)
                return;
            var direction = (keys == Keys.Right || keys == Keys.Down)
                ? "right" 
                : "left";
            ImageName = $"running-{direction}.png";
        }

        public string GetImageFileName() => ImageName;

        public CreatureCommand Act(int x, int y)
        {
            var result = new CreatureCommand();
            ChangeImageName(Game.KeyPressed);
            switch (Game.KeyPressed)
            {
                case Keys.Right:
                    if (x + 1 < Game.MapWidth && !Game.Map[x + 1, y].ContainsObstaclesOrBomb()
                                              && !Game.Map[x + 1, y].ContainsForceField())
                        result.DeltaX = 1;
                    break;
                case Keys.Left:
                    if (x > 0 && !Game.Map[x - 1, y].ContainsObstaclesOrBomb()
                              && !Game.Map[x - 1, y].ContainsForceField())
                        result.DeltaX = -1;
                    break;
                case Keys.Down:
                    if (y + 1 < Game.MapHeight && !Game.Map[x, y + 1].ContainsObstaclesOrBomb()
                                               && !Game.Map[x, y + 1].ContainsForceField())
                        result.DeltaY = 1;
                    break;
                case Keys.Up:
                    if (y > 0 && !Game.Map[x, y - 1].ContainsObstaclesOrBomb()
                              && !Game.Map[x, y - 1].ContainsForceField())
                        result.DeltaY = -1;
                    break;
                case Keys.Space:
                    if (CurrentBombs < BombsLimit && !Game.Map[x,y].ContainsObstaclesOrBomb() && !Game.WantToMoveMonster[x, y])
                    {
                        Game.WantToMoveMonster[x, y] = true;
                        result.TransformTo = new ICreature[] {this, new Bomb(this)};
                        CurrentBombs++;
                    }
                    break;
            }
            return result;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            Game.IsPlayerDead = conflictedObject is Fire ||
                                conflictedObject is Monster || 
                                conflictedObject is Block;

            if (Game.IsPlayerDead && Program.EnableSound && File.Exists(soundFile))
            {
                new SoundPlayer(soundFile).Play();
            }
            
            return Game.IsPlayerDead;
        }

        public int GetDrawingPriority() => 4;
    }
}