using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game003.GameObjects
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
            foreach (string line in GlobalFuncs.ReadAllLines("Content/Maps/" + AreaName + "_levels.csv"))
            {
                splitLine = line.Split(';').ToList();
                if (int.TryParse(splitLine.ElementAt(0), out int zpos) & int.TryParse(splitLine.ElementAt(1), out int x) & int.TryParse(splitLine.ElementAt(2), out int y))
                    levelsSource.Add(new AreaLevel(zpos, x, y));
            }
        }

        public AreaLevel_ZPos GetAreaLevel_ZPos(int x, int y)
        {
            AreaLevel area;
            foreach (AreaLevel_ZPos zp in Enum.GetValues(typeof(AreaLevel_ZPos)).Cast<AreaLevel_ZPos>())
            {
                area = new AreaLevel(zp, x, y);
                if (levelsSource.Contains(area))
                    return zp;
            }
            return AreaLevel_ZPos.NONE;
        }

        public AreaLevel GetAreaLevel(int x, int y)
        {
            AreaLevel area;
            foreach (AreaLevel_ZPos zp in Enum.GetValues(typeof(AreaLevel_ZPos)).Cast<AreaLevel_ZPos>())
            {
                area = new AreaLevel(zp, x, y);
                if (levelsSource.Contains(area))
                    return area;
            }
            return new AreaLevel(AreaLevel_ZPos.NONE,x,y);
        }
    }

    class AreaLevel
    {
        public AreaLevel_ZPos ZPos;
        public int XVal,YVal;

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
