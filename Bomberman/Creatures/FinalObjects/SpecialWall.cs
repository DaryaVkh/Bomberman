namespace Bomberman
{
    public class SpecialWall : ICreature
    {
        public string GetImageFileName() => "SpecialWall.png";
        
        public int GetDrawingPriority() => 5;
        
        public CreatureCommand Act(int x, int y) => new CreatureCommand();

        public bool DeadInConflict(ICreature conflictedObject) => false;
    }
}