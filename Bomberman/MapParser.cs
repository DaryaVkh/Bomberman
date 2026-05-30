using System;
using System.Collections.Generic;

namespace Bomberman
{
    public static class MapParser
    {
        private static readonly HashSet<char> MonstersAsSymbols = new HashSet<char>{'0', '1', '2', '3'};
    
        public static ICreature[,][] GetMapFromText(string text)
        {
            var lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var map = new ICreature[lines[0].Length, lines.Length][];
            var playersCount = 0;

            for (var y = 0; y < lines.Length; y++)
            {
                for (var x = 0; x < lines[0].Length; x++)
                {
                    if (lines[y][x] == 'P')
                        playersCount++;
                    if (MonstersAsSymbols.Contains(lines[y][x]))
                        Game.MonstersCount++;
                    if (lines[y][x] == 'X')
                        Game.PlatesCount++;
                    if (lines[y][x] == 'R')
                        Game.RemoteControlInMap = true;
                    
                    map[x, y] = lines[y][x] switch
                    {
                        'H' => Helpers.Array<ForceField>(),
                        'X' => Helpers.Array<Plate>(),
                        'B' => Helpers.Array<Block>(),
                        'P' => Helpers.Array<Player>(),
                        'D' => Helpers.Array<Dynamite>(),
                        'O' => Helpers.Array<OpenDoor>(),
                        'C' => Helpers.Array<ClosedDoor>(),
                        'S' => Helpers.Array<SpecialWall>(),
                        'R' => Helpers.Array<RemoteControl>(),
                        'W' => Helpers.Array<BreakableWall>(),
                        '#' => Helpers.Array<UnbreakableWall>(),
                        '0' => Helpers.Array<PredictableMonster>(),
                        '1' => Helpers.Array<RandomMonster>(),
                        '2' => Helpers.Array<WideSearchMonster>(),
                        '3' => Helpers.Array<DijkstraMonster>(),
                        'Q' => new ICreature[] {new BreakableWall(), new ClosedDoor()},
                        'b' => Helpers.Array<PlusBomb>(),
                        's' => Helpers.Array<PlusSplash>(),
                        'm' => new ICreature[] {new Hint(1)},
                        'n' => new ICreature[] {new Hint(2)},
                        'x' => new ICreature[] {new Hint(3)},
                        'y' => new ICreature[] {new Hint(4)},
                        'z' => new ICreature[] {new Hint(5)},
                        ' ' => new ICreature[] { },
                         _  => throw new ArgumentException($"wrong character for map {lines[y][x]}")
                    };
                }
            }
            
            if (playersCount != 1)
                throw new ArgumentException("There must be exactly 1 player on the map");

            return map;
        }
    }
}