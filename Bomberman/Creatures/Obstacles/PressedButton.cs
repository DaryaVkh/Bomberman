namespace Bomberman
{
    public class PressedButton : ICreature
    {
        public string GetImageFileName() => "ClickedButton.png";
        
        public CreatureCommand Act(int x, int y) => new CreatureCommand();

        public bool DeadInConflict(ICreature conflictedObject) => false;
        
        public int GetDrawingPriority() => 100;
    }
}