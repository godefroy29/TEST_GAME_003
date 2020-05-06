using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game003
{
    class Player
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        //public const int player_spriteNumber = 8;
        //public const int player_width = 24;
        //public const int player_height = 32;
        //public double speed;
        //private int oldKey;

        public const int DOWN = 0;
        public const int UP = 1;
        public const int LEFT = 2;
        public const int RIGHT = 3;
        
        private int speed = 20;
        private int tickCount = 0;

        private const int BLOCK = 16;

        Keys[] movingKeys;
        Keys[] interactKeys;

        bool interactionInProgress = false;

        bool willMove = false;

        bool flipWalk = false;
        int flipWalkCount = 0;

        int maxHeight;
        int maxWidth;

        Vector2 currentLocation;

        KeyboardState oldState;
        KeyboardState newState;

        List<Rectangle> listBlockedBlock;

        public Player(Texture2D texture, int rows, int columns, Vector2 location, int maxH, int maxW, List<Rectangle> listBlock)
        {
            listBlockedBlock = listBlock;
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            currentLocation = location;
            maxHeight = maxH;
            maxWidth = maxW;

            movingKeys = new[] { Keys.Left, Keys.Right, Keys.Up, Keys.Down };
            interactKeys = new[] { Keys.Space };
        }

        public void Update(KeyboardState kState)
        {
            newState = kState;
            tickCount++;
            if (tickCount >= speed)
            {
                Process();
                tickCount = 0;
            }
        }

        private void Process()
        {

            if (interactKeys.Any(x => newState.IsKeyDown(x)))
                InteractWithObject();

            if (movingKeys.Any(x => newState.IsKeyDown(x)))
                MoveMe();
            
        }

        private void InteractWithObject()
        {
            //throw new NotImplementedException();
        }

        private void MoveMe()
        {
            if (interactionInProgress)
                return;

            int newFrame = 0;

            if (newState.IsKeyDown(Keys.Left))
                newFrame = LEFT;
            if (newState.IsKeyDown(Keys.Right))
                newFrame = RIGHT;
            if (newState.IsKeyDown(Keys.Up))
                newFrame = UP;
            if (newState.IsKeyDown(Keys.Down))
                newFrame = DOWN;

            if (currentFrame % 4 == newFrame)//direction précédente identique à la nouvelle direction
            {
                MoveLocation(newFrame);
            }

            currentFrame = newFrame + (currentFrame >= 4 ? 0 : 4);
            
            

            //flipWalk = currentFrame >= 4 & flipWalkCount >= 3;
            //flipWalkCount += flipWalkCount >= 3 ? -flipWalkCount : 1;
            //flipWalkCount++;

            //currentFrame = newFrame + ((currentFrame == newFrame || currentFrame == newFrame + 4) ? currentFrame / 4 : 0);

        }

        private void MoveLocation(int newFrame)
        {
            Vector2 newLocation = currentLocation;
            bool roadIsBlocked = false;
            switch (newFrame)
            {
                case 0:
                    newLocation.Y += currentLocation.Y >= maxHeight - BLOCK ? 0 : BLOCK;
                    break;
                case 1:
                    newLocation.Y -= currentLocation.Y <= 0 ? 0 : BLOCK;
                    break;
                case 2:
                    newLocation.X -= currentLocation.X <= 0 ? 0 : BLOCK;
                    break;
                case 3:
                    newLocation.X += currentLocation.X >= maxWidth - BLOCK ? 0 : BLOCK;
                    break;
                default:
                    break;
            }
            foreach (Rectangle rect in listBlockedBlock)
            {
                if(new Rectangle((int)newLocation.X,(int)newLocation.Y, BLOCK, BLOCK).Intersects(rect))
                    roadIsBlocked = true;
            }
            currentLocation = roadIsBlocked ? currentLocation : newLocation;

        }
        

        public void Draw(SpriteBatch spriteBatch)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)(int)currentFrame / (float)Columns);
            int column = (int)currentFrame % Columns;

            Rectangle sourceFrame = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)currentLocation.X, (int)currentLocation.Y, width, height);

            //spriteBatch.Draw(Texture, destinationRectangle, sourceFrame, Color.White, 0, Vector2.Zero, flipWalk ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            spriteBatch.Draw(Texture, destinationRectangle, sourceFrame, Color.White); //, 0, Vector2.Zero, flipWalk ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
        }
        

    }
}
