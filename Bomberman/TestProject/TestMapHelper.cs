using Bomberman;

namespace TestProject
{
    public static class TestMapHelper
    {
        // Создаёт карту, автоматически добавляя игрока если его нет.
        // Если в карте есть свободная клетка — ставит P туда,
        // иначе добавляет новую строку "#P#...#" нужной ширины.
        public static void CreateMap(string map)
        {
            if (!map.Contains('P'))
            {
                var lastSpace = map.LastIndexOf(' ');
                if (lastSpace >= 0)
                {
                    map = map.Remove(lastSpace, 1).Insert(lastSpace, "P");
                }
                else
                {
                    var firstNewline = map.IndexOf('\n');
                    var lineEnd = firstNewline >= 1 && map[firstNewline - 1] == '\r' ? firstNewline - 1 : firstNewline;
                    var width = lineEnd;
                    map += "\r\n#P" + new string('#', width - 2);
                }
            }
            Game.CreateMap(map);
        }
    }
}
