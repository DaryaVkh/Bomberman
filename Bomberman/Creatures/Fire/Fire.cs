using System;

namespace Bomberman
{
    public class Fire : ICreature
    {
        public readonly Direction direction;
        private readonly int splashLimit;
        private int splashLengthNow;

        public Fire(int splashLimit, Direction direction)
        {
            this.splashLimit = splashLimit;
            this.direction = direction;
        }

        public string GetImageFileName() => "Fire.png";

        public int GetDrawingPriority() => 2;
        
        public CreatureCommand Act(int x, int y)
        {
            if (splashLengthNow == splashLimit)
                return new CreatureCommand { TransformTo = new ICreature[] { } };

            splashLengthNow++;

            return direction switch
            {
                Direction.Right => new CreatureCommand{DeltaX = 1},
                Direction.Left => new CreatureCommand{DeltaX = -1},
                Direction.Up => new CreatureCommand{DeltaY = -1},
                Direction.Down => new CreatureCommand{DeltaY = 1},
                _ => throw new ArgumentException("Unknown direction")
            };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Bomb 
                   || conflictedObject is Dynamite 
                   || conflictedObject is BreakableWall
                   || conflictedObject is UnbreakableWall 
                   || conflictedObject is Block
                   || conflictedObject is SpecialWall;
        }
    }
}