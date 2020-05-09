using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameDll.GameEnums;
using static GameDll.GameFuncs;

namespace GameDll
{
    public class AreaEventList
    {
        public List<AreaEvent> eventsSource;
        private int Block;



        /// <summary>
        /// Init List with event from a file
        /// </summary>
        /// <param name="filePath">fullfile name, ex : "Content/PeterCity_events.csv"</param>
        public AreaEventList(string filePath, int Block)
        {
            eventsSource = new List<AreaEvent>();
            this.Block = Block;

            List<String> splitLine;
            foreach (string line in ReadAllLines(filePath))
            {
                splitLine = line.Split(';').ToList();
                if (int.TryParse(splitLine.ElementAt(3), out int x) & int.TryParse(splitLine.ElementAt(4), out int y))
                    eventsSource.Add(new AreaEvent(splitLine.ElementAt(0), splitLine.ElementAt(1), splitLine.ElementAt(2), x, y));
            }
        }
        public AreaEventList(int Block)
        {
            eventsSource = new List<AreaEvent>();
            this.Block = Block;
        }

        public List<AreaEvent> GetAreaEvent(AreaEvent_Type t, Direction d, String n, int x, int y)
        {

            IEnumerable<AreaEvent> ie = from item in eventsSource
                                         where (item.AType == t | t == AreaEvent_Type.NONE)
                                         & (item.Dir == d | d ==Direction.NONE)
                                         & (item.Name == n | string.IsNullOrWhiteSpace(n))
                                         & (item.XVal == x | x == -1)
                                         & (item.YVal == y | y == -1)
                                         select item;
            if (ie.Count() > 0)
                return ie.ToList();
            return new List<AreaEvent>();

        }

    }


}
