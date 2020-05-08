using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game003.GameObjects
{
   

    class AreaEvents
    {
        public List<AreaEvent> eventsSource;
        public int blockSize;

        public AreaEvents(string AreaName, int blockSize)
        {
            eventsSource = new List<AreaEvent>();
            this.blockSize = blockSize;

            List<String> splitLine;
            foreach (string line in GlobalFuncs.ReadAllLines("Content/Maps/" + AreaName + "_events.csv"))
            {
                splitLine = line.Split(';').ToList();
                if (int.TryParse(splitLine.ElementAt(2), out int x) & int.TryParse(splitLine.ElementAt(3), out int y))
                    eventsSource.Add(new AreaEvent(splitLine.ElementAt(0), splitLine.ElementAt(1), x, y));
            }
        }

        public List<AreaEvent> GetListAreaEvent(int x, int y)
        {
            List<AreaEvent> list = new List<AreaEvent>();
            foreach (AreaEvent a in eventsSource)
            {
                if (a.XVal == x & a.YVal == y)
                    list.Add(a);
            }
            return list;
        }

        public List<AreaEvent> GetListAreaEvent(AreaEvent_Type aet,int x, int y)
        {
            List<AreaEvent> list = new List<AreaEvent>();
            foreach (AreaEvent a in eventsSource)
            {
                if (a.XVal == x & a.YVal == y & a.AType == aet)
                    list.Add(a);
            }
            return list;
        }

        public AreaEvent GetAreaEvent(string name, int x, int y)
        {
            foreach (AreaEvent a in eventsSource)
            {
                if (a.XVal == x & a.YVal == y & a.Name == name)
                    return a;
            }
            return new AreaEvent(AreaEvent_Type.NONE,"",x,y);
        }
    }




    class AreaEvent
    {
        public AreaEvent_Type AType;
        public string Name;
        public int XVal,YVal;

        public AreaEvent(AreaEvent_Type aType, string name, int xVal, int yVal)
        {
            AType = aType;
            Name = name;
            XVal = xVal;
            YVal = yVal;
        }

        public AreaEvent(string sType, string name, int xVal, int yVal)
        {
            AType = StringToType(sType);
            Name = name;
            XVal = xVal;
            YVal = yVal;
        }

        private AreaEvent_Type StringToType(string sType)
        {
            switch (sType)
            {
                case "ChangeStageNow":
                    return AreaEvent_Type.ChangeStageNow;
                case "ChangeStageUp":
                    return AreaEvent_Type.ChangeStageUp;
                case "ChangeStageDown":
                    return AreaEvent_Type.ChangeStageDown;
                case "ChangeStageLeft":
                    return AreaEvent_Type.ChangeStageLeft;
                case "ChangeStageRight":
                    return AreaEvent_Type.ChangeStageRight;
                case "StartPoint":
                    return AreaEvent_Type.StartPoint;
                case "Interact":
                    return AreaEvent_Type.Interact;
                default:
                    return AreaEvent_Type.NONE;
            }
        }
    }

}
