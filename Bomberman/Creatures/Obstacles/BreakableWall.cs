using System.IO;
using System.Media;

namespace Bomberman
{
    public class BreakableWall : ICreature
    {
        private static readonly string soundFile = Path.Combine(Program.SoundsPath, "wall.wav");
        
        public string GetImageFileName() => "BreakableWall.png";

        public int GetDrawingPriority() => 5;

        public CreatureCommand Act(int x, int y) => new CreatureCommand();

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Fire && Program.EnableSound && File.Exists(soundFile))
            {
                new SoundPlayer(soundFile).Play();
            }
            
            return conflictedObject is Fire;
        }
    }
}