using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameDll.GameEnums;

namespace GameDll
{
    public class AreaLevel
    {
        public AreaLevel_ZPos ZPos;
        public int XVal, YVal;

        public AreaLevel()
        {
            ZPos = AreaLevel_ZPos.NONE;
            XVal = -1;
            YVal = -1;
        }

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

        public string ToData()
        {
            return (int)ZPos + ";" + XVal + ";" + YVal;
        }
    }
}
