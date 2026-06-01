using System.Windows.Forms;

namespace Bomberman
{
    public static class Game
    {
        public static ICreature[,][] Map;
        public static bool IsOver;
        public static bool CanGoToNextLevel;
        public static bool IsRemoteControl;
        public static bool RemoteControlInMap;

        public static bool Hint1;
        public static bool Hint2;
        public static bool Hint3;
        public static bool Hint4;
        public static bool Hint5;
        public static bool Hint6;
        public static bool Hint7;
        
        public static Keys KeyPressed;
        public static int MapWidth => Map.GetLength(0);
        public static int MapHeight => Map.GetLength(1);
        public static bool[,] WantToMoveMonster;
        public static int MonstersCount;
        public static int ButtonsCount;
        public static int Level;
        public static bool IsPlayerDead;
        public static bool IsHint;

        public static void CreateMap(string map)
        {
            MonstersCount = 0;
            ButtonsCount = 0;
            Map = MapParser.GetMapFromText(map);
            WantToMoveMonster = new bool[MapWidth, MapHeight];
        }
    }
}