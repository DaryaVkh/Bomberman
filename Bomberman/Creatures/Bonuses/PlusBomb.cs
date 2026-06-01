namespace Bomberman
{
    public class PlusBomb : ICreature
    {
        public string GetImageFileName() => "PlusBombIcon.png";
        
        public int GetDrawingPriority() => 5;
        
        public CreatureCommand Act(int x, int y) => new CreatureCommand();

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Player)
            {
                (conflictedObject as Player).BombsLimit++;
                Window.Bombs++;
            }
            return true;
        }
    }
}