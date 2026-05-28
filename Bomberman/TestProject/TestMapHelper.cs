using Bomberman;

namespace TestProject
{
    public static class TestMapHelper
    {
        // Создаёт карту, автоматически добавляя игрока если его нет.
        // Игрок помещается перед последним символом последней строки (обычно '#').
        public static void CreateMap(string map)
        {
            if (!map.Contains('P'))
            {
                var lastHash = map.LastIndexOf('#');
                map = map.Insert(lastHash, "P");
            }
            Game.CreateMap(map);
        }
    }
}
