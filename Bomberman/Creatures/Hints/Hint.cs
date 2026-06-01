namespace Bomberman
{
    public class Hint : ICreature
    {
        public readonly int number;
        
        public Hint(int number)
        {
            this.number = number;
        }

        public string GetImageFileName() => "Hint.png";
        
        public int GetDrawingPriority() => 100;

        public CreatureCommand Act(int x, int y) => new CreatureCommand();

        public bool DeadInConflict(ICreature conflictedObject)
        {
            var result = conflictedObject is Player;

            if (result)
            {
                Game.IsHint = true;
                Game.Hint1 = number == 1;
                Game.Hint2 = number == 2;
                Game.Hint3 = number == 3;
                Game.Hint4 = number == 4;
                Game.Hint5 = number == 5;
                Game.Hint6 = number == 6;
                Game.Hint7 = number == 7;
            }

            return result;
        }
    }
}