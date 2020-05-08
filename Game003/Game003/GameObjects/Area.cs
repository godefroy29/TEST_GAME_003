using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Game003
{
    class Area
    {

        private ContentManager content;
        public readonly string name;
        public readonly int blockSize;
        public readonly Texture2D texture;
        public readonly int Width;
        public readonly int Height;
        public Dictionary<Rectangle, string> eventBlocks;
        public List<Rectangle> z0;
        public List<Rectangle> z1;
        public List<Rectangle> z2;
        public Point sp;
        public readonly List<String> listLevel = new List<String> { "-1", "0", "1", "2" };


        


        public Area(ContentManager content, string name, int blockSize)
        {
            try
            {
                this.content = content;
                this.name = name;
                this.blockSize = blockSize;
                texture = content.Load<Texture2D>(name);
                Width = texture.Width;
                Height = texture.Height;

                FillLevels();

                FillEvents();

            }
            catch (Exception e)
            {

                throw e;
            }


        }

        private void FillEvents()
        {
            try
            {


                List<string> eventList = new List<string>();
                List<string> eventListSplit = new List<string>();
                int v1, v2;
                string eventCsvName = "Content/" + name + "_Events.csv";

                using (StreamReader sr = new StreamReader(eventCsvName))
                {
                    while (sr.Peek() >= 0)
                    {
                        eventList.Add(sr.ReadLine());
                    }
                }

                eventBlocks = new Dictionary<Rectangle, string>();
                foreach (string ev in eventList)
                {
                    eventListSplit = ev.Split(';').ToList();
                    if (eventListSplit.First() != "Name")
                    {

                        v1 = Int32.Parse(eventListSplit.ElementAt(1));
                        v2 = Int32.Parse(eventListSplit.ElementAt(2));

                        eventBlocks.Add(new Rectangle(v1 * blockSize, v2 * blockSize, blockSize, blockSize), eventListSplit.First());
                    }

                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

        private void FillLevels()
        {
            try
            {
                List<string> blocks = new List<string>();
                string levelsCsvName = "Content/" + name + "_levels.csv";

                using (StreamReader sr = new StreamReader(levelsCsvName))
                {
                    while (sr.Peek() >= 0)
                    {
                        blocks.Add(sr.ReadLine());
                    }
                }
                z0 = new List<Rectangle>();
                z1 = new List<Rectangle>();
                z2 = new List<Rectangle>();
                foreach (string block in blocks)
                {
                    if (listLevel.Contains(block.Split(';').First()))
                    {
                        List<int> val = block.Split(';').Select(Int32.Parse).ToList();
                        switch (val.First())
                        {
                            case 0:
                                z0.Add(new Rectangle(val.ElementAt(1) * blockSize, val.ElementAt(2) * blockSize, blockSize, blockSize));
                                break;
                            case 1:
                                z1.Add(new Rectangle(val.ElementAt(1) * blockSize, val.ElementAt(2) * blockSize, blockSize, blockSize));
                                break;
                            case 2:
                                z2.Add(new Rectangle(val.ElementAt(1) * blockSize, val.ElementAt(2) * blockSize, blockSize, blockSize));
                                break;
                            case -1:
                                sp = new Point(val.ElementAt(1) * blockSize, val.ElementAt(2) * blockSize);
                                break;

                            default:
                                break;
                        }
                    }

                }
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
