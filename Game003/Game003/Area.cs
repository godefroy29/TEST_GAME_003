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
        public readonly List<Rectangle> z0;
        public readonly List<Rectangle> z1;
        public readonly List<Rectangle> z2;
        public readonly Point sp;
        public readonly List<String> listLevel = new List<String>{"-1","0","1","2"};


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

                List<string> blocks = new List<string>();
                string fullCsvName = "Content/" + name + ".csv";

                using (StreamReader sr = new StreamReader(fullCsvName))
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

                throw e;
            }


        }
    }
}
