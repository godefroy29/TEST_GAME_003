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

namespace Game003.Area
{
    class Area
    {

        private ContentManager content;
        public readonly string name;
        public readonly int blockSize;
        public readonly Texture2D texture;
        public readonly int Width;
        public readonly int Height;
        public readonly AreaEvents listAreaEvents;
        public readonly AreaLevels listAreaLevels;
        
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

                listAreaEvents = new AreaEvents(name, GlobalVars.BLOCK);
                listAreaLevels = new AreaLevels(name, GlobalVars.BLOCK);

            }
            catch (Exception)
            {
                throw;
            }

        }
        

    }
}
