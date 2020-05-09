using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameDll.GameEnums;

namespace GameDll
{
    public class AreaEvent
    {
        public AreaEvent_Type AType;
        public Direction Dir;
        public string Name;
        public int XVal, YVal;

        public AreaEvent()
        {
            AType = AreaEvent_Type.NONE;
            Dir = Direction.NONE;
            Name = "";
            XVal = -1;
            YVal = -1;
        }

        public AreaEvent(AreaEvent_Type aType, Direction dir, string name, int xVal, int yVal)
        {
            AType = aType;
            Dir = dir;
            Name = name;
            XVal = xVal;
            YVal = yVal;
        }

        public AreaEvent(string sType, string dir, string name, int xVal, int yVal)
        {
            AType = StringToType(sType);
            Dir = StringToDir(dir);
            Name = name;
            XVal = xVal;
            YVal = yVal;
        }

        public string ToData()
        {
            return AType + ";" + Name + ";" + Dir + ";" + XVal + ";" + YVal;
        }
    }
}
