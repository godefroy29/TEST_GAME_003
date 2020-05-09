
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using static GameDll.GameEnums;
using static GameDll.GameFuncs;

namespace GameDll
{
    public class AreaLevelList
    {
        public List<AreaLevel> levelsSource;
        public int blockSize;
        private int bLOCK;

        public AreaLevelList(int bLOCK)
        {
            levelsSource = new List<AreaLevel>();
            this.bLOCK = bLOCK;
        }

        public AreaLevelList(String filePath, int blockSize)
        {
            this.blockSize = blockSize;
            levelsSource = new List<AreaLevel>();

            List<String> splitLine;
            foreach (string line in ReadAllLines(filePath))
            {
                splitLine = line.Split(';').ToList();
                if (int.TryParse(splitLine.ElementAt(0), out int zpos) & int.TryParse(splitLine.ElementAt(1), out int x) & int.TryParse(splitLine.ElementAt(2), out int y))
                    levelsSource.Add(new AreaLevel(zpos, x, y));
            }
        }


        public List<AreaLevel> GetAeraLevel(AreaLevel_ZPos zp)
        {
            IEnumerable<AreaLevel> ie = from item in levelsSource where item.ZPos == zp select item;
            if (ie.Count() > 0)
                return ie.ToList();
            return new List<AreaLevel>();
        }

        public AreaLevel GetAreaLevel(int x, int y)
        {
            IEnumerable<AreaLevel> ie = from item in levelsSource where item.XVal == x && item.YVal == y select item;
            if(ie.Count() > 0)
                return ie.First();
            return new AreaLevel();
        }

        public void EditZPos(AreaLevel_ZPos z, int x, int y)
        {
            levelsSource.Remove(GetAreaLevel(x,y));
            levelsSource.Add(new AreaLevel(z, x, y));
        }

    }
}
