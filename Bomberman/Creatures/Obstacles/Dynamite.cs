using System.IO;
using System.Media;

namespace Bomberman
{
    public class Dynamite : ICreature
    {
        private bool shouldExplode;
        private static readonly string soundFile = Path.Combine(Program.SoundsPath, "bomb.wav");

        public string GetImageFileName() => "Dynamite.png";
        
        public int GetDrawingPriority() => 5;

        public CreatureCommand Act(int x, int y)
        {
            if (shouldExplode)
            {
                return 
                    new CreatureCommand { TransformTo =
                        new[] {
                            new Fire(100, Direction.Up), 
                            new Fire(100, Direction.Down),
                            new Fire(100, Direction.Right), 
                            new Fire(100, Direction.Left) } };
            }
            return new CreatureCommand();
        }
        
        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Fire && Program.EnableSound && File.Exists(soundFile))
            {
                new SoundPlayer(soundFile).Play();
                
                shouldExplode = true;
            }
            
            return false;
        }
    }
}