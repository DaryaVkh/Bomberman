using Bomberman;
using FluentAssertions;
using NUnit.Framework;

namespace TestProject
{
    [TestFixture]
    public class MapParser_Should
    {
        [Test]
        public void MapParser_CreateMap_MapCreatedCorrectly()
        {
            var test = @"
######
#PW0 #
######";
            var map = MapParser.GetMapFromText(test);
            
            map.GetLength(0).Should().Be(6);
            map.GetLength(1).Should().Be(3);
            map[0, 0].Length.Should().Be(1);
            map[0, 0].Should().ContainItemsAssignableTo<UnbreakableWall>();
            map[1, 1].Length.Should().Be(1);
            map[1, 1].Should().ContainItemsAssignableTo<Player>();
            map[2, 1].Length.Should().Be(1);
            map[2, 1].Should().ContainItemsAssignableTo<BreakableWall>();
            map[3, 1].Length.Should().Be(1);
            map[3, 1].Should().ContainItemsAssignableTo<PredictableMonster>();
            map[4, 1].Should().BeEmpty();
        }
    }
}