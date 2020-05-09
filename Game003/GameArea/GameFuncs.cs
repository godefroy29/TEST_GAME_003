using System;
using System.Collections.Generic;
using System.IO;

namespace GameDll
{
    public static class GameFuncs
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

        public static AreaEventList ReadEventList(String path, int block)
        {
            return new AreaEventList(path,block);
        }

        public static AreaLevelList ReadLevelList(String path, int block)
        {
            return new AreaLevelList(path, block);
        }

        public static void WriteLevels(AreaLevelList al, String path)
        {
            List<string> data = new List<string> { "Level;X;Y" };
            foreach (AreaLevel t in al.levelsSource)
            {
                data.Add(t.ToData());
            }
            File.WriteAllLines(path, data);
        }

        public static void WriteEvents(AreaEventList ae, String path)
        {
            List<string> data = new List<string> { "Type;Name;Dir;X;Y" };
            foreach (AreaEvent t in ae.eventsSource)
            {
                data.Add(t.ToData());
            }
            File.WriteAllLines(path, data);
        }


    }
}
