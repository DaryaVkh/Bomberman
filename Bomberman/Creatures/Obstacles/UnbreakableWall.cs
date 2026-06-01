namespace Bomberman
{
    public class UnbreakableWall : ICreature
    {
        public string GetImageFileName() => "UnbreakableWall.png";

        public int GetDrawingPriority() => 1;

        public CreatureCommand Act(int x, int y) => new CreatureCommand();

        public bool DeadInConflict(ICreature conflictedObject) => false;
    }
}