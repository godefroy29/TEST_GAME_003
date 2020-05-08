using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Game003
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D background;
        private Rectangle rectWindows;
        private string oldArenaName;
        private Player.Player player;

        Area.Area area;
         const string firstAreaName = "Maps/PeterCity";

        Matrix scale;
        int sizeExtend = 1;

        KeyboardState oldState;

        private Texture2D startButton, resumeButton, exitButton;
        private Vector2 startButtonPosition, resumeButtonPosition, exitButtonPosition;
        private Rectangle startButtonRect, resumeButtonRect, exitButtonRect;
        //private Thread backgroundThread;
        //private bool isLoading = false;
        MouseState mouseState, previousMouseState;
        GameState currentGameState;

        Texture2D menuBackground;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            scale = Matrix.CreateScale(sizeExtend);
            IsMouseVisible = true;
            currentGameState = GameState.StartMenu;
            oldState = Keyboard.GetState();
            oldArenaName = "StartGame";
            base.Initialize();
        }


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here


            menuBackground = Content.Load<Texture2D>("Maps/PeterCity");
            startButton = Content.Load<Texture2D>("Dialog/Start");
            startButtonPosition = new Vector2((menuBackground.Width / 2) - (startButton.Width / 2), (menuBackground.Height / 2) - startButton.Height - 25);
            startButtonRect = new Rectangle((int)startButtonPosition.X, (int)startButtonPosition.Y, startButton.Width, startButton.Height);

            resumeButton = Content.Load<Texture2D>("Dialog/Start");
            resumeButtonPosition = new Vector2((menuBackground.Width / 2) - (resumeButton.Width / 2), (menuBackground.Height / 2) - resumeButton.Height - 25);
            resumeButtonRect = new Rectangle((int)resumeButtonPosition.X, (int)resumeButtonPosition.Y, resumeButton.Width, resumeButton.Height);

            exitButton = Content.Load<Texture2D>("Dialog/Exit");
            exitButtonPosition = new Vector2((menuBackground.Width / 2) - (exitButton.Width / 2), (menuBackground.Height / 2) - exitButton.Height + 25);
            exitButtonRect = new Rectangle((int)exitButtonPosition.X, (int)exitButtonPosition.Y, exitButton.Width, exitButton.Height);

            ActualiseGraphicSize(menuBackground);

            currentGameState = GameState.StartMenu;
        }

        private void ActualiseGraphicSize(Texture2D t2d)
        {
            rectWindows = new Rectangle(0, 0, t2d.Width, t2d.Height);
            graphics.PreferredBackBufferWidth = t2d.Width * sizeExtend;
            graphics.PreferredBackBufferHeight = t2d.Height * sizeExtend;
            graphics.ApplyChanges();
        }

        public void LoadArea(String name)
        {
            area = new Area.Area(Content, name, 16);
            rectWindows = new Rectangle(0, 0, area.Width, area.Height);
            player = new Player.Player(Content.Load<Texture2D>("Player/RedPlayer"), 1, 8, area, oldArenaName);
            oldArenaName = area.name.Replace("Maps/","");
            ActualiseGraphicSize(area.texture);
        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            switch (currentGameState)
            {
                case GameState.StartMenu:
                case GameState.PauseMenu:
                    IsMouseVisible = true;
                    if (Keyboard.GetState().IsKeyUp(Keys.Escape) & oldState.IsKeyDown(Keys.Escape))
                        Exit();
                    CheckMouseState();
                    break;

                case GameState.LoadingNextArea:
                    IsMouseVisible = false;
                    break;

                case GameState.Playing:
                    IsMouseVisible = false;
                    if (Keyboard.GetState().IsKeyUp(Keys.Escape) & oldState.IsKeyDown(Keys.Escape))
                        currentGameState = GameState.PauseMenu;
                    player.Update(Keyboard.GetState(), gameTime);

                    switch (player.eventToTrigger.AType)
                    {
                        case Area.AreaEvent_Type.ChangeStageNow:
                        case Area.AreaEvent_Type.ChangeStageUp:
                        case Area.AreaEvent_Type.ChangeStageDown:
                        case Area.AreaEvent_Type.ChangeStageLeft:
                        case Area.AreaEvent_Type.ChangeStageRight:
                            GoToNextArea(player.eventToTrigger.Name);
                            break;

                        case Area.AreaEvent_Type.Interact:
                            break;

                        case Area.AreaEvent_Type.StartPoint:
                        case Area.AreaEvent_Type.NONE:
                            break;
                    }
                    break;
            }

            oldState = Keyboard.GetState();

            base.Update(gameTime);
        }

        public void GoToNextArea(String name)
        {
            currentGameState = GameState.LoadingNextArea;
            LoadArea("Maps/" + name);
            currentGameState = GameState.Playing;
        }


        private void CheckMouseState()
        {
            if (IsMouseVisible & (currentGameState == GameState.StartMenu | currentGameState == GameState.PauseMenu))
            {
                mouseState = Mouse.GetState();
                Rectangle mouseClickRect = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

                if (previousMouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
                {

                    switch (currentGameState)
                    {
                        case GameState.StartMenu:
                            if (mouseClickRect.Intersects(startButtonRect))
                            {
                                currentGameState = GameState.Playing;
                                LoadArea(firstAreaName);
                            }
                            if (mouseClickRect.Intersects(exitButtonRect))
                                Exit();
                            break;

                        case GameState.PauseMenu:
                            if (mouseClickRect.Intersects(resumeButtonRect))
                            {
                                currentGameState = GameState.Playing;
                            }
                            if (mouseClickRect.Intersects(exitButtonRect))
                                Exit();
                            break;

                        case GameState.LoadingNextArea:
                            break;

                        case GameState.Playing:
                            break;


                        default:
                            break;
                    }
                }
            }
            previousMouseState = mouseState;
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(transformMatrix: scale);

            switch (currentGameState)
            {
                case GameState.StartMenu:
                    spriteBatch.Draw(menuBackground, Vector2.Zero, Color.LightGray);
                    spriteBatch.Draw(startButton, startButtonPosition, Color.White);
                    spriteBatch.Draw(exitButton, exitButtonPosition, Color.White);
                    break;

                case GameState.PauseMenu:
                    spriteBatch.Draw(menuBackground, Vector2.Zero, Color.LightGray);
                    spriteBatch.Draw(resumeButton, resumeButtonPosition, Color.White);
                    spriteBatch.Draw(exitButton, exitButtonPosition, Color.White);
                    break;

                case GameState.LoadingNextArea:
                    spriteBatch.Draw(area.texture, rectWindows, Color.Black);
                    break;

                case GameState.Playing:
                    spriteBatch.Draw(area.texture, rectWindows, Color.White);
                    player.Draw(spriteBatch);
                    break;

                default:
                    break;
            }


            spriteBatch.End();
            base.Draw(gameTime);
        }




    }
}
