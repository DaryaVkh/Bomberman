using System.IO;
using System.Media;

namespace Bomberman
{
    public class OpenDoor : ICreature
    {
        private static readonly string soundFile = Path.Combine(Program.SoundsPath, "level.wav");

        public string GetImageFileName() => "OpenDoor.png";

        public int GetDrawingPriority() => 100;

        public CreatureCommand Act(int x, int y) => new CreatureCommand();

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Player)
            {
                Game.CanGoToNextLevel = true;
                if (Program.EnableSound && File.Exists(soundFile))
                {
                    new SoundPlayer(soundFile).Play();
                }
            }
            return false;
        }
    }
}