using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game003.Area
{

    public enum AreaEvent_Type
    {
        NONE = -1,
        ChangeStageNow = 1,
        ChangeStageUp = 2,
        ChangeStageDown = 3,
        ChangeStageLeft = 4,
        ChangeStageRight = 5,
        StartPoint = 6,
        Interact = 7
    }

    public enum AreaLevel_ZPos
    {
        NONE = -1,
        GROUND = 0,
        PLAYER = 1,
        SKY = 2
    }


    //public static class Vars
    //{
    //}
}
