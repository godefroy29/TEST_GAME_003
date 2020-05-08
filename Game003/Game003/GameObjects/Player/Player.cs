using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Game003.Area;

namespace Game003.Player
{
    class Player
    {



        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public bool IsOnEventBlock { get; internal set; }

        public PlayerState ps;
        public AreaEvent eventToTrigger;

        private int totalFrames;
        private Area.Area myArea;
        public float speed = 1;

        Dictionary<Direction, Animation> listAnimation;
        Animation currentAnimation;



        Keys[] movingKeys;
        Keys[] interactKeys;

        bool interactionInProgress = false;

        KeyboardState newState;
        GameTime gameTime;



        #region "MAIN FUNCTION"



        /// <summary>
        /// Create Player and place it on the "StartGame" spot on area
        /// </summary>
        /// <param name="texture">Texture for player</param>
        /// <param name="rows">Row count for texture</param>
        /// <param name="columns">Column count for texture</param>
        /// <param name="parea">Area/Stage</param>
        public Player(Texture2D texture, int rows, int columns, Area.Area parea, string oldArenaName)
        {
            movingKeys = new[] { Keys.Left, Keys.Right, Keys.Up, Keys.Down };
            interactKeys = new[] { Keys.Space };
            myArea = parea;
            Texture = texture;
            Rows = rows;
            Columns = columns;
            totalFrames = Rows * Columns;
            IsOnEventBlock = false;
            eventToTrigger = new AreaEvent();
            ps = GetStartPointOfArea(oldArenaName);

            listAnimation = new Dictionary<Direction, Animation>
            {
                { Direction.UP, CreateWalkAnimation(Direction.UP) },
                { Direction.DOWN, CreateWalkAnimation(Direction.DOWN) },
                { Direction.LEFT, CreateWalkAnimation(Direction.LEFT) },
                { Direction.RIGHT, CreateWalkAnimation(Direction.RIGHT) }
            };
            currentAnimation = listAnimation[ps.dir];
        }

        private Animation CreateWalkAnimation(Direction dir)
        {
            Animation animation = new Animation();
            animation.AddFrame(new Rectangle((int)dir * GlobalVars.BLOCK, 0, GlobalVars.BLOCK, GlobalVars.BLOCK), TimeSpan.FromSeconds(.25));
            animation.AddFrame(new Rectangle(((int)dir + 4) * GlobalVars.BLOCK, 0, GlobalVars.BLOCK, GlobalVars.BLOCK), TimeSpan.FromSeconds(.50));
            animation.AddFrame(new Rectangle((int)dir * GlobalVars.BLOCK, 0, GlobalVars.BLOCK, GlobalVars.BLOCK), TimeSpan.FromSeconds(.25));
            return animation;
        }

        private PlayerState GetStartPointOfArea(string name)
        {
            AreaEvent areaSp = myArea.listAreaEvents.GetAreaEvent(AreaEvent_Type.StartPoint, name);
            PlayerState myPs;
            myPs = new PlayerState
            {
                pos = new Vector2(areaSp.XVal, areaSp.YVal),
                dir = Direction.DOWN
            };
            return areaSp.AType == AreaEvent_Type.NONE ? ps : myPs;
        }




        public void Update(KeyboardState kState, GameTime gameTime)
        {
            this.gameTime = gameTime;
            newState = kState;

            if (interactKeys.Any(x => kState.IsKeyDown(x)))
                InteractWithObject();

            if (movingKeys.Any(x => kState.IsKeyDown(x)) & !interactionInProgress)
            {
                MoveAndPlayEvent(ps, GetNewPlayerState());
            }

        }

        private void InteractWithObject()
        {
            //TODO
        }

        private void MoveAndPlayEvent(PlayerState currentPs, PlayerState nextPs)
        {
            AreaEvent_Type eT = AreaEvent_Type.NONE;

            if (currentPs.pos != nextPs.pos) //si on arrive sur une nouvelle case, on jour l'event "ChangeStateNow" de la nouvelle case (exemple : porte de batiment, echelle, ...)
                eT = AreaEvent_Type.ChangeStageNow;

            if (currentPs.pos == nextPs.pos & currentPs.dir == nextPs.dir) //si on garde la même direction (exemple : sortie de batiment, trigger hautes herbes,...)
                switch (currentPs.dir)
                {
                    case Direction.DOWN:
                        eT = AreaEvent_Type.ChangeStageDown;
                        break;
                    case Direction.UP:
                        eT = AreaEvent_Type.ChangeStageUp;
                        break;
                    case Direction.LEFT:
                        eT = AreaEvent_Type.ChangeStageLeft;
                        break;
                    case Direction.RIGHT:
                        eT = AreaEvent_Type.ChangeStageRight;
                        break;
                    default:
                        eT = AreaEvent_Type.NONE;
                        break;
                }
            List<AreaEvent> lae = myArea.listAreaEvents.GetAreaEvent(eT, (int)nextPs.pos.X, (int)nextPs.pos.Y);
            eventToTrigger = lae.Count > 0 ? lae.First() : new AreaEvent();
            ps = nextPs;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)(int)ps.dir / (float)Columns);
            int column = (int)ps.dir % Columns;

            Rectangle sourceFrame = currentAnimation.CurrentRectangle;
            Rectangle destinationRectangle = new Rectangle((int)ps.pos.X * GlobalVars.BLOCK, (int)ps.pos.Y * GlobalVars.BLOCK, width, height);

            spriteBatch.Draw(Texture, destinationRectangle, sourceFrame, Color.White); //, 0, Vector2.Zero, flipWalk ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
        }


        #endregion




        /// <summary>
        /// Get New Player State (Direction and location)
        /// </summary>
        /// <returns>New Player State to apply</returns>
        private PlayerState GetNewPlayerState()
        {
            PlayerState newPs = new PlayerState
            {
                dir = GetDirFromKeyboard(),
                pos = ps.pos
            };
            currentAnimation = newPs.dir >= 0 ? listAnimation[newPs.dir] : listAnimation[Direction.DOWN];

            if (ps.dir == newPs.dir)//direction précédente identique à la nouvelle direction
            {
                newPs.pos = GetNewLocation(newPs.dir);
                currentAnimation.Update(gameTime);
            }
            return newPs;
        }


        /// <summary>
        /// Get the new direction given by keyboard key down
        /// </summary>
        /// <returns>New Direction</returns>
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


        /// <summary>
        /// Get new player location on the area
        /// </summary>
        /// <param name="newFrame">Direction to go</param>
        /// <returns>New location</returns>
        private Vector2 GetNewLocation(Direction newFrame)
        {
            Vector2 newLocation = ps.pos;
            switch (newFrame)
            {
                case Direction.DOWN:
                    newLocation.Y += ps.pos.Y < (myArea.texture.Height / GlobalVars.BLOCK) -1 ? 1 : 0;
                    break;
                case Direction.UP:
                    newLocation.Y -= ps.pos.Y <= 0 ? 0 : 1;
                    break;
                case Direction.LEFT:
                    newLocation.X -= ps.pos.X <= 0 ? 0 : 1;
                    break;
                case Direction.RIGHT:
                    newLocation.X += ps.pos.X < (myArea.texture.Width / GlobalVars.BLOCK) -1 ? 1 : 0;
                    break;
                default:
                    break;
            }

            return myArea.listAreaLevels.GetAreaLevel_ZPos(AreaLevel_ZPos.PLAYER, (int)newLocation.X, (int)newLocation.Y) == AreaLevel_ZPos.PLAYER ? ps.pos : newLocation;

        }



        //Not used for the moment...
        Vector2 GetDesiredVelocityFromLocations(Vector2 oldLoc, Vector2 newLoc)
        {
            Vector2 desiredVelocity = newLoc - oldLoc;
            desiredVelocity.Normalize();
            desiredVelocity *= speed;
            return desiredVelocity;
        }






    }
}
