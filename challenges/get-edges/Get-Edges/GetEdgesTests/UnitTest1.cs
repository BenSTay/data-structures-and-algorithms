using Get_Edges;
using Graphs.Classes;
using Xunit;

namespace GetEdgesTests
{
    public class UnitTest1
    {
        [Fact]
        public void CanGetEdges()
        {
            Graph<string> map = Program.SetupTest();
            string[] itinerary = new string[] { "Arendelle", "Monstropolis", "Naboo" };
            Assert.Equal(115, Program.GetEdges(map, itinerary));
        }

        [Fact]
        public void ImpossibleRouteReturnsZero()
        {
            Graph<string> map = Program.SetupTest();
            string[] itinerary = new string[] { "Naboo", "Pandora", "Metroville" };
            Assert.Equal(0, Program.GetEdges(map, itinerary));
        }

        [Fact]
        public void ItineraryLengthOneReturnsZero()
        {
            Graph<string> map = Program.SetupTest();
            string[] itinerary = new string[] { "Narnia" };
            Assert.Equal(0, Program.GetEdges(map, itinerary));
        }
    }
}
