using System;
using System.Collections.Generic;
using System.Linq;

namespace Game003.Area
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
            foreach (string line in GlobalFuncs.ReadAllLines("Content/" + AreaName + "_events.csv"))
            {
                splitLine = line.Split(';').ToList();
                if (int.TryParse(splitLine.ElementAt(2), out int x) & int.TryParse(splitLine.ElementAt(3), out int y))
                    eventsSource.Add(new AreaEvent(splitLine.ElementAt(0), splitLine.ElementAt(1), x, y));
            }
        }



        public List<AreaEvent> GetAreaEvent(int x, int y)
        {
            IEnumerable<AreaEvent> ie = from item in eventsSource where item.XVal == x & item.YVal == y select item;
            if (ie.Count() > 0)
                return ie.ToList();
            return new List<AreaEvent>();
        }

        public List<AreaEvent> GetAreaEvent(AreaEvent_Type aet,int x, int y)
        {
            IEnumerable<AreaEvent> ie = from item in eventsSource where item.AType == aet & item.XVal == x & item.YVal == y select item;
            if (ie.Count() > 0)
                return ie.ToList();
            return new List<AreaEvent>();
        }

        public AreaEvent GetAreaEvent(string name)
        {
            IEnumerable<AreaEvent> ie = from item in eventsSource where item.Name == name select item;
            if (ie.Count() > 0)
                return ie.First();
            return new AreaEvent();
        }
        
        public AreaEvent GetAreaEvent(AreaEvent_Type aet, string name)
        {
            IEnumerable<AreaEvent> ie = from item in eventsSource where item.AType == aet & item.Name == name select item;
            if (ie.Count() > 0)
                return ie.First();
            return new AreaEvent();
        }

        public AreaEvent GetAreaEvent(string name, int x, int y)
        {
            IEnumerable<AreaEvent> ie = from item in eventsSource where item.Name == name & item.XVal == x & item.YVal == y select item;
            if (ie.Count() > 0)
                return ie.First();
            return new AreaEvent();
        }
    }




    class AreaEvent
    {
        public AreaEvent_Type AType;
        public string Name;
        public int XVal,YVal;

        public AreaEvent()
        {
            AType = AreaEvent_Type.NONE;
            Name = "";
            XVal = -1;
            YVal = -1;
        }

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
