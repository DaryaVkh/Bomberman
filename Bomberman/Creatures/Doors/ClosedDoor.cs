namespace Bomberman
{
    public class ClosedDoor : ICreature
    {
        public string GetImageFileName() => "ClosedDoor.png";

        public CreatureCommand Act(int x, int y)
        {
            return Game.MonstersCount == 0 && Game.PlatesCount == 0 && !Game.RemoteControlInMap
                ? new CreatureCommand { TransformTo = new[] { new OpenDoor() } } 
                : new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject) => false;
        
        public int GetDrawingPriority() => 100;
    }
}