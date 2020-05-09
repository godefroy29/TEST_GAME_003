using System;

namespace GameDll
{
    public static class GameEnums
    {

        public enum AreaEvent_Type
        {
            NONE = -1,
            CHANGESTAGENOW = 1,
            CHANGESTAGEUP = 2,
            CHANGESTAGEDOWN = 3,
            CHANGESTAGELEFT = 4,
            CHANGESTAGERIGHT = 5,
            STARTPOINT = 6,
            INTERACT = 7
        }
        public static String TypeToString(AreaEvent_Type a)
        {
            switch (a)
            {
                case AreaEvent_Type.CHANGESTAGENOW:
                    return "CHANGESTAGENOW";
                case AreaEvent_Type.CHANGESTAGEUP:
                    return "CHANGESTAGEUP";
                case AreaEvent_Type.CHANGESTAGEDOWN:
                    return "CHANGESTAGEDOWN";
                case AreaEvent_Type.CHANGESTAGELEFT:
                    return "CHANGESTAGELEFT";
                case AreaEvent_Type.CHANGESTAGERIGHT:
                    return "CHANGESTAGERIGHT";
                case AreaEvent_Type.STARTPOINT:
                    return "STARTPOINT";
                case AreaEvent_Type.INTERACT:
                    return "INTERACT";
                case AreaEvent_Type.NONE:
                default:
                    return "NONE";
            }
        }
        public static AreaEvent_Type StringToType(string sType)
        {
            switch (sType)
            {
                case "CHANGESTAGENOW":
                    return AreaEvent_Type.CHANGESTAGENOW;
                case "CHANGESTAGEUP":
                    return AreaEvent_Type.CHANGESTAGEUP;
                case "CHANGESTAGEDOWN":
                    return AreaEvent_Type.CHANGESTAGEDOWN;
                case "CHANGESTAGELEFT":
                    return AreaEvent_Type.CHANGESTAGELEFT;
                case "CHANGESTAGERIGHT":
                    return AreaEvent_Type.CHANGESTAGERIGHT;
                case "STARTPOINT":
                    return AreaEvent_Type.STARTPOINT;
                default:
                    return AreaEvent_Type.NONE;
            }
        }


        public enum AreaLevel_ZPos
        {
            NONE = -1,
            GROUND = 0,
            PLAYER = 1,
            SKY = 2
        }
        public static String PosToString(AreaLevel_ZPos p)
        {
            switch (p)
            {
                case AreaLevel_ZPos.GROUND:
                    return "GROUND";
                case AreaLevel_ZPos.PLAYER:
                    return "PLAYER";
                case AreaLevel_ZPos.SKY:
                    return "SKY";
                case AreaLevel_ZPos.NONE:
                default:
                    return "NONE";
            }
        }
            public static AreaLevel_ZPos StringToPos(string p)
        {
            switch (p)
            {
                case "GROUND":
                    return AreaLevel_ZPos.GROUND;
                case "PLAYER":
                    return AreaLevel_ZPos.PLAYER;
                case "SKY":
                    return AreaLevel_ZPos.PLAYER;
                case "NONE":
                default:
                    return AreaLevel_ZPos.NONE;
            }
        }


        enum GameState
        {
            SARTMENU = 0,
            LOADINGNEXTAREA = 1,
            PLAYING = 2,
            PAUSEMENU = 3
        }
        



        public enum Direction
        {
            NONE = -1,
            DOWN = 0,
            UP = 1,
            LEFT = 2,
            RIGHT = 3
        }
        public static String DirToString(Direction d)
        {
            switch (d)
            {
                case Direction.DOWN:
                    return "DOWN";
                case Direction.UP:
                    return "UP";
                case Direction.LEFT:
                    return "LEFT";
                case Direction.RIGHT:
                    return "RIGHT";
                case Direction.NONE:
                default:
                    return "NONE";
            }
        }
        public static Direction StringToDir(string sDir)
        {
            switch (sDir)
            {
                case "UP":
                    return Direction.UP;
                case "DOWN":
                    return Direction.DOWN;
                case "LEFT":
                    return Direction.LEFT;
                case "RIGHT":
                    return Direction.RIGHT;
                case "NONE":
                default:
                    return Direction.NONE;
            }
        }
    }
    
}