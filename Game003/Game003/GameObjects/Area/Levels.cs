using System;
using System.Collections.Generic;
using System.Linq;

namespace Game003.Area
{



    class AreaLevels
    {
        public List<AreaLevel> levelsSource;
        public int blockSize;

        public AreaLevels(String AreaName, int blockSize)
        {
            levelsSource = new List<AreaLevel>();
            this.blockSize = blockSize;

            List<String> splitLine;
            foreach (string line in GlobalFuncs.ReadAllLines("Content/" + AreaName + "_levels.csv"))
            {
                splitLine = line.Split(';').ToList();
                if (int.TryParse(splitLine.ElementAt(0), out int zpos) & int.TryParse(splitLine.ElementAt(1), out int x) & int.TryParse(splitLine.ElementAt(2), out int y))
                    levelsSource.Add(new AreaLevel(zpos, x, y));
            }
        }


        public AreaLevel_ZPos GetAreaLevel_ZPos(AreaLevel_ZPos zp, int x, int y)
        {
            if ((from item in levelsSource where item.ZPos == zp && item.XVal == x && item.YVal == y select item).Count() > 0)
                return zp;
            return AreaLevel_ZPos.NONE;
        }

    }

    class AreaLevel
    {
        public AreaLevel_ZPos ZPos;
        public int XVal, YVal;

        public AreaLevel(AreaLevel_ZPos zpos, int xVal, int yVal)
        {
            ZPos = zpos;
            XVal = xVal;
            YVal = yVal;
        }
        public AreaLevel(int zpos, int xVal, int yVal)
        {
            ZPos = IntToZPos(zpos);
            XVal = xVal;
            YVal = yVal;
        }

        public static AreaLevel_ZPos IntToZPos(int i)
        {
            switch (i)
            {
                case 0:
                    return AreaLevel_ZPos.GROUND;
                case 1:
                    return AreaLevel_ZPos.PLAYER;
                case 2:
                    return AreaLevel_ZPos.SKY;
                default:
                    return AreaLevel_ZPos.NONE;
            }
        }

        public static int ZPosToInt(AreaLevel_ZPos i)
        {
            return (int)i;
        }
    }

}
