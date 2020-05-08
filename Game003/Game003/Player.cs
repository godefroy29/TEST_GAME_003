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
        public bool IsOnEventBlock { get; internal set; }

        private Direction currentDir;
        private int totalFrames;
        private Area area;
        public float speed = 1;

        Dictionary<Direction, Animation> listAnimation;
        Animation currentAnimation;



        Keys[] movingKeys;
        Keys[] interactKeys;

        bool interactionInProgress = false;

        public Vector2 currentLocation;
        KeyboardState newState;
        GameTime gameTime;



        #region "MAIN FUNCTION"


        public Player(Texture2D texture, int rows, int columns, Area area)
        {
            movingKeys = new[] { Keys.Left, Keys.Right, Keys.Up, Keys.Down };
            interactKeys = new[] { Keys.Space };

            this.area = area;
            currentLocation = new Vector2(area.sp.X, area.sp.Y);
            Texture = texture;
            Rows = rows;
            Columns = columns;
            totalFrames = Rows * Columns;
            IsOnEventBlock = false;
            currentDir = Direction.UP;
            listAnimation = new Dictionary<Direction, Animation>
            {
                { Direction.UP, CreateWalkAnimation(Direction.UP) },
                { Direction.DOWN, CreateWalkAnimation(Direction.DOWN) },
                { Direction.LEFT, CreateWalkAnimation(Direction.LEFT) },
                { Direction.RIGHT, CreateWalkAnimation(Direction.RIGHT) }
            };
            currentAnimation = listAnimation[Direction.UP];

        }

        public void Update(KeyboardState kState, GameTime gameTime)
        {
            this.gameTime = gameTime;
            newState = kState;

            if (interactKeys.Any(x => newState.IsKeyDown(x)))
                InteractWithObject();

            if (movingKeys.Any(x => newState.IsKeyDown(x)) & !interactionInProgress)
                MoveMe();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)(int)currentDir / (float)Columns);
            int column = (int)currentDir % Columns;

            Rectangle sourceFrame = currentAnimation.CurrentRectangle;
            Rectangle destinationRectangle = new Rectangle((int)currentLocation.X, (int)currentLocation.Y, width, height);

            spriteBatch.Draw(Texture, destinationRectangle, sourceFrame, Color.White); //, 0, Vector2.Zero, flipWalk ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);


        }


        #endregion



        private void InteractWithObject()
        {
            //throw new NotImplementedException();
        }

        private void MoveMe()
        {
            Direction newDir = GetDirFromKeyboard();
            currentAnimation = newDir >= 0 ? listAnimation[newDir] : listAnimation[Direction.DOWN];

            if (currentDir == newDir)//direction précédente identique à la nouvelle direction
            {
                MoveLocation(newDir);
                currentAnimation.Update(gameTime);
            }

            currentDir = newDir;

        }

        private Direction GetDirFromKeyboard()
        {
            Direction newDir = Direction.NONE;
            if (newState.IsKeyDown(Keys.Left))
                newDir = Direction.LEFT;
            if (newState.IsKeyDown(Keys.Right))
                newDir = Direction.RIGHT;
            if (newState.IsKeyDown(Keys.Up))
                newDir = Direction.UP;
            if (newState.IsKeyDown(Keys.Down))
                newDir = Direction.DOWN;
            return newDir;
        }

        private void MoveLocation(Direction newFrame)
        {
            Vector2 newLocation = currentLocation;
            bool roadIsBlocked = false;
            switch (newFrame)
            {
                case Direction.DOWN:
                    newLocation.Y += currentLocation.Y >= area.texture.Height - GlobalVars.BLOCK ? 0 : GlobalVars.BLOCK;
                    break;
                case Direction.UP:
                    newLocation.Y -= currentLocation.Y <= 0 ? 0 : GlobalVars.BLOCK;
                    break;
                case Direction.LEFT:
                    newLocation.X -= currentLocation.X <= 0 ? 0 : GlobalVars.BLOCK;
                    break;
                case Direction.RIGHT:
                    newLocation.X += currentLocation.X >= area.texture.Width - GlobalVars.BLOCK ? 0 : GlobalVars.BLOCK;
                    break;
                default:
                    break;
            }

            //TODO : Improve with area.Z1.Contains ?
            foreach (Rectangle rect in area.z1)
            {
                if (new Rectangle((int)newLocation.X, (int)newLocation.Y, GlobalVars.BLOCK, GlobalVars.BLOCK).Intersects(rect))
                    roadIsBlocked = true;
            }

            currentLocation = roadIsBlocked ? currentLocation : newLocation;

            IsOnEventBlock = area.eventBlocks.Keys.Contains(new Rectangle((int)currentLocation.X, (int)currentLocation.Y, GlobalVars.BLOCK, GlobalVars.BLOCK));


        }



        //Not used for the moment...
        Vector2 GetDesiredVelocityFromLocations(Vector2 oldLoc, Vector2 newLoc)
        {
            Vector2 desiredVelocity = newLoc - oldLoc;
            desiredVelocity.Normalize();
            desiredVelocity *= speed;
            return desiredVelocity;
        }



        private Animation CreateWalkAnimation(Direction dir)
        {
            Animation animation = new Animation();
            animation.AddFrame(new Rectangle((int)dir * GlobalVars.BLOCK, 0, GlobalVars.BLOCK, GlobalVars.BLOCK), TimeSpan.FromSeconds(.25));
            animation.AddFrame(new Rectangle(((int)dir + 4) * GlobalVars.BLOCK, 0, GlobalVars.BLOCK, GlobalVars.BLOCK), TimeSpan.FromSeconds(.50));
            animation.AddFrame(new Rectangle((int)dir * GlobalVars.BLOCK, 0, GlobalVars.BLOCK, GlobalVars.BLOCK), TimeSpan.FromSeconds(.25));
            return animation;
        }


    }
}
