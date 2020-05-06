using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

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

        private Player player;


        public int WINDOW_WIDTH = 160;
        public int WINDOW_HEIGHT = 224;

        public const int BLOCK = 16;

        public const int START_X = 64;
        public const int START_Y = 208;

        Matrix scale;
        int sizeExtend = 2;

        List<Rectangle> listBlockedBlock;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = WINDOW_WIDTH * sizeExtend;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT * sizeExtend;
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
            rectWindows = new Rectangle(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT);
            listBlockedBlock = GetBlockedBlock();
            player = new Player(Content.Load<Texture2D>("RedPlayer"),1,8, new Vector2(START_X, START_Y), WINDOW_HEIGHT, WINDOW_WIDTH, listBlockedBlock);
            scale = Matrix.CreateScale(sizeExtend);
            

            base.Initialize();
        }

        private List<Rectangle> GetBlockedBlock()
        {
            List<Rectangle> res = new List<Rectangle>();
            res.Add(CreateBlockedBlock(0, 0));
            res.Add(CreateBlockedBlock(0, 1));
            res.Add(CreateBlockedBlock(0, 2));
            res.Add(CreateBlockedBlock(0, 3));
            res.Add(CreateBlockedBlock(0, 4));
            res.Add(CreateBlockedBlock(0, 5));
            res.Add(CreateBlockedBlock(0, 6));
            res.Add(CreateBlockedBlock(0, 7));
            res.Add(CreateBlockedBlock(0, 8));
            res.Add(CreateBlockedBlock(0, 9));

            res.Add(CreateBlockedBlock(1, 0));
            res.Add(CreateBlockedBlock(1, 3));
            res.Add(CreateBlockedBlock(1, 9));

            res.Add(CreateBlockedBlock(2, 0));
            res.Add(CreateBlockedBlock(2, 3));
            res.Add(CreateBlockedBlock(2, 5));
            res.Add(CreateBlockedBlock(2, 7));
            res.Add(CreateBlockedBlock(2, 9));

            res.Add(CreateBlockedBlock(3, 0));
            res.Add(CreateBlockedBlock(3, 3));
            res.Add(CreateBlockedBlock(3, 9));
            res.Add(CreateBlockedBlock(3, 10));

            res.Add(CreateBlockedBlock(4, 0));

            res.Add(CreateBlockedBlock(5, 0));
            res.Add(CreateBlockedBlock(5, 5));
            res.Add(CreateBlockedBlock(5, 7));
            
            res.Add(CreateBlockedBlock(6, 0));
            res.Add(CreateBlockedBlock(6, 3));
            res.Add(CreateBlockedBlock(6, 5));
            res.Add(CreateBlockedBlock(6, 7));
            res.Add(CreateBlockedBlock(6, 9));
            res.Add(CreateBlockedBlock(6, 10));
            
            res.Add(CreateBlockedBlock(7, 0));
            res.Add(CreateBlockedBlock(7, 3));
            res.Add(CreateBlockedBlock(7, 5));
            res.Add(CreateBlockedBlock(7, 7));
            res.Add(CreateBlockedBlock(7, 9));

            res.Add(CreateBlockedBlock(8, 0));
            res.Add(CreateBlockedBlock(8, 3));
            res.Add(CreateBlockedBlock(8, 9));

            res.Add(CreateBlockedBlock(9, 0));
            res.Add(CreateBlockedBlock(9, 1));
            res.Add(CreateBlockedBlock(9, 2));
            res.Add(CreateBlockedBlock(9, 3));
            res.Add(CreateBlockedBlock(9, 4));
            res.Add(CreateBlockedBlock(9, 5));
            res.Add(CreateBlockedBlock(9, 6));
            res.Add(CreateBlockedBlock(9, 7));
            res.Add(CreateBlockedBlock(9, 8));
            res.Add(CreateBlockedBlock(9, 9));




            return res;
        }

        private Rectangle CreateBlockedBlock(int v1, int v2)
        {
            return new Rectangle(v1 * BLOCK, v2 * BLOCK, BLOCK, BLOCK);
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
            background = Content.Load<Texture2D>("Arene1");
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            
           
            player.Update(Keyboard.GetState());


            //player.Move("Up");

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(transformMatrix:scale);
            spriteBatch.Draw(background, rectWindows, Color.White);
            player.Draw(spriteBatch);



            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
