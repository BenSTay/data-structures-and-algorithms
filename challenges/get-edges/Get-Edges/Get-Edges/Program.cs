using Graphs.Classes;
using System;
using System.Collections.Generic;

namespace Get_Edges
{
    public class Program
    {
        /// <summary>
        /// Determines the total cost of a route through the map (if possible).
        /// </summary>
        /// <param name="map">The map of locations.</param>
        /// <param name="itinerary">The route information.</param>
        /// <returns>The total cost, or 0 if the route is impossible.</returns>
        public static int GetEdges(Graph<string> map, string[] itinerary)
        {
            if (map.Size() < 2 || itinerary.Length < 2)
            {
                return 0;
            }

            int cost = 0;
            Dictionary<string, int> neighbors;

            for (int i = 0; i < itinerary.Length - 1; i++)
            {
                neighbors = map.GetNeighbors(itinerary[i]);
                if (neighbors is null)
                {
                    return 0;
                }
                else if (!neighbors.ContainsKey(itinerary[i + 1]))
                {
                    return 0;
                }
                else
                {
                    cost += neighbors.GetValueOrDefault(itinerary[i + 1]);
                }
            }

            return cost;
        }

        /// <summary>
        /// Sets up the default graph for testing.
        /// </summary>
        /// <returns>The default graph.</returns>
        public static Graph<string> SetupTest()
        {
            Graph<string> map = new Graph<string>();
            string[] locations = new string[] { "Pandora", "Arendelle",
                "Metroville", "Monstropolis", "Narnia", "Naboo"};

            for (int i = 0; i < locations.Length; i++)
            {
                map.AddNode(locations[i]);
            }

            map.AddEdge("Pandora", "Arendelle", 150);
            map.AddEdge("Pandora", "Metroville", 82);
            map.AddEdge("Arendelle", "Metroville", 99);
            map.AddEdge("Arendelle", "Monstropolis", 42);
            map.AddEdge("Metroville", "Narnia", 37);
            map.AddEdge("Metroville", "Naboo", 26);
            map.AddEdge("Metroville", "Monstropolis", 105);
            map.AddEdge("Monstropolis", "Naboo", 73);
            map.AddEdge("Narnia", "Naboo", 250);

            return map;
        }

        static void Main(string[] args)
        {
            Graph<string> map = SetupTest();
            Console.WriteLine(string.Join(", ", map.BreadthFirst("Monstropolis")));
        }
    }
}
