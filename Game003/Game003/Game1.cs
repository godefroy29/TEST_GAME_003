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

        enum GameState
        {
            StartMenu,
            Loading,
            Playing,
            PauseMenu
        }

        public const int BLOCK = 16;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D background;
        private Rectangle rectWindows;

        private Player player;

        Area arena1;

        Matrix scale;
        int sizeExtend = 1;

        KeyboardState oldState;

        private Texture2D startButton, resumeButton, exitButton;
        private Vector2 startButtonPosition, resumeButtonPosition, exitButtonPosition;
        private Rectangle startButtonRect, resumeButtonRect, exitButtonRect;
        private Thread backgroundThread;
        private bool isLoading = false;
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


            menuBackground = Content.Load<Texture2D>("PeterCity");

            startButton = Content.Load<Texture2D>("Start");
            startButtonPosition = new Vector2((menuBackground.Width / 2) - (startButton.Width / 2), (menuBackground.Height / 2) - startButton.Height - 25);
            startButtonRect = new Rectangle((int)startButtonPosition.X, (int)startButtonPosition.Y, startButton.Width, startButton.Height);

            resumeButton = Content.Load<Texture2D>("Start");
            resumeButtonPosition = new Vector2((menuBackground.Width / 2) - (resumeButton.Width / 2), (menuBackground.Height / 2) - resumeButton.Height - 25);
            resumeButtonRect = new Rectangle((int)resumeButtonPosition.X, (int)resumeButtonPosition.Y, resumeButton.Width, resumeButton.Height);

            exitButton = Content.Load<Texture2D>("Exit");
            exitButtonPosition = new Vector2((menuBackground.Width / 2) - (exitButton.Width / 2), (menuBackground.Height / 2) - exitButton.Height + 25);
            exitButtonRect = new Rectangle((int)exitButtonPosition.X, (int)exitButtonPosition.Y, exitButton.Width, exitButton.Height);


            arena1 = new Area(Content, "PeterCity", 16);
            graphics.PreferredBackBufferWidth = arena1.width * sizeExtend;
            graphics.PreferredBackBufferHeight = arena1.height * sizeExtend;
            graphics.ApplyChanges();
            rectWindows = new Rectangle(0, 0, arena1.width, arena1.height);
            player = new Player(Content.Load<Texture2D>("RedPlayer"), 1, 8, arena1);

            currentGameState = GameState.StartMenu;
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
                    IsMouseVisible = true;
                    if (Keyboard.GetState().IsKeyUp(Keys.Escape) & oldState.IsKeyDown(Keys.Escape))
                        Exit();
                    CheckMouseState();
                    break;

                case GameState.PauseMenu:
                    IsMouseVisible = true;
                    if (Keyboard.GetState().IsKeyUp(Keys.Escape) & oldState.IsKeyDown(Keys.Escape))
                        Exit();
                    CheckMouseState();
                    break;

                case GameState.Loading:
                    break;

                case GameState.Playing:
                    IsMouseVisible = false;
                    if (Keyboard.GetState().IsKeyUp(Keys.Escape) & oldState.IsKeyDown(Keys.Escape))
                        currentGameState = GameState.PauseMenu;
                    player.Update(Keyboard.GetState(), gameTime);
                    break;

                default:
                    break;
            }

            oldState = Keyboard.GetState();

            base.Update(gameTime);
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
                                background = arena1.texture;
                            }
                            if (mouseClickRect.Intersects(exitButtonRect))
                                Exit();
                            break;

                        case GameState.PauseMenu:
                            if (mouseClickRect.Intersects(resumeButtonRect))
                            {
                                currentGameState = GameState.Playing;
                                background = arena1.texture;
                            }

                            if (mouseClickRect.Intersects(exitButtonRect))
                                Exit();
                            break;

                        case GameState.Loading:
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

                case GameState.Loading:
                    break;

                case GameState.Playing:
                    spriteBatch.Draw(background, rectWindows, Color.White);
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
