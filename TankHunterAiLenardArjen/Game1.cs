using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using TankHunterAiLenardArjen.Enitities;
using TankHunterAiLenardArjen.Support;

namespace TankHunterAiLenardArjen
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        World world;
        Player player;
        Tank tank;
        List<Airplane> planes;
       

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

            GlobalVars.worldWidth = GraphicsDevice.Viewport.Width;
            GlobalVars.worldHeight = GraphicsDevice.Viewport.Height;
            world = new World(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            player = new Player(1, new Vector(0, 0), 1.5f, 4, 2, new Vector(25, 25), world);
            tank = new Tank(world, 1, new Vector(0, 0), 1f, 4, 45, new Vector(250, 250));
            planes = new List<Airplane>();


            for (int i = 0; i < 20; i++)
            {
                planes.Add(new Airplane(world, 1, new Vector(0, 0), 3.2f, 5, 2, new Vector(200 + i * 5, 200 + i * 5)));
            }

            for (int i = 0; i < 10; i++)
            {
                planes.Add(new Airplane(world, 1, new Vector(0, 0), 3f, 4, 12, new Vector(30 + i * 5, 200 + i * 5)));
            }

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

            //Load font
            //font = Content.Load<SpriteFont>("Arial");

            //Load player texture
            FileStream fileStream = new FileStream("Content/Sprites/Player.png", FileMode.Open);
            GlobalVars.PlayerTexture = Texture2D.FromStream(GraphicsDevice, fileStream);
            player.PlayerTexture = Support.GlobalVars.PlayerTexture;

            // World
            fileStream = new FileStream("Content/Sprites/SandTile.png", FileMode.Open);
            GlobalVars.DefaultTileTexture = Texture2D.FromStream(GraphicsDevice, fileStream);
            world.TileTexture = Support.GlobalVars.DefaultTileTexture;
            GlobalVars.GraphTexture = new Texture2D(graphics.GraphicsDevice, 1, 1);
            GlobalVars.GraphTexture.SetData(new Color[] { Color.Blue });


            //Load tank
            fileStream = new FileStream("Content/Sprites/TankBottom.png", FileMode.Open);
            tank.Texture = Texture2D.FromStream(GraphicsDevice, fileStream);
            fileStream = new FileStream("Content/Sprites/TankTop.png", FileMode.Open);
            tank.TankTopTexture = Texture2D.FromStream(GraphicsDevice, fileStream);
            //fileStream = new FileStream("Content/Sprites/DebugTarget.png", FileMode.Open);
            //tank. = Texture2D.FromStream(GraphicsDevice, fileStream);

            //Load Planes
            fileStream = new FileStream("Content/Sprites/Airplane.png", FileMode.Open);

            foreach (Airplane plane in planes)
            {
                plane.PlaneTexture = Texture2D.FromStream(GraphicsDevice, fileStream);
            }

            //If debugging is enabled load the textures (maybe this should always be done in case debugging can be enabled in runtime)
            if (GlobalVars.debug == true)
            {
                fileStream = new FileStream("Content/Sprites/DebugNeighbor.png", FileMode.Open);
                tank.TileDebugNeighborTexture = Texture2D.FromStream(GraphicsDevice, fileStream);
                fileStream = new FileStream("Content/Sprites/DebugCenter.png", FileMode.Open);
                tank.TileDebugCenterTexture = Texture2D.FromStream(GraphicsDevice, fileStream);
            }
            fileStream.Dispose();
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
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                GlobalVars.debug = !GlobalVars.debug;
            // Entity's updated here
            tank.Update(gameTime.ElapsedGameTime.Milliseconds);
            world.Update(gameTime.ElapsedGameTime.Milliseconds);
            player.Update(gameTime.ElapsedGameTime.Milliseconds);

            foreach (Airplane plane in planes)
            {
                plane.Update(gameTime.ElapsedGameTime.Milliseconds);
            }

            base.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LawnGreen);

            world.Render(spriteBatch);
            player.Render(spriteBatch);
            tank.Render(spriteBatch);

            foreach (Airplane plane in planes)
            {
                plane.Render(spriteBatch);
            }

            // TODO pipeline doest work for me
            //spriteBatch.Begin();

            //spriteBatch.DrawString(font, "Tankpos x:" + tank.Position.X + " \n Tankpos y:" + tank.Position.Y, new Vector2(0, 30), Color.Black);

            //spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
