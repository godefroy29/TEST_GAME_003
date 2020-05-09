using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game003
{


    enum GameState
    {
        StartMenu,
        LoadingNextArea,
        Playing,
        PauseMenu
    }

    public enum Direction
    {
        NONE = -1,
        DOWN = 0,
        UP = 1,
        LEFT = 2,
        RIGHT = 3
    }

    public static class GlobalVars
    {
        public const int BLOCK = 16;
    }

    public static class GlobalFuncs
    {
        public static List<String> ReadAllLines(string fileName)
        {
            List<String> fileLines = new List<string>();
            using (StreamReader sr = new StreamReader(fileName))
            {
                while (sr.Peek() >= 0)
                {
                    fileLines.Add(sr.ReadLine());
                }
            }
            return fileLines;
        }


    }
}
