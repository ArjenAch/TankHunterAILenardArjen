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
        SpriteFont font;
        World world;
        Player player;
        Tank tank;
        List<Airplane> planes;
       

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            GlobalVars.TimeElapsed = 0;



        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            player = new Player(1, new Vector(0, 0), 1.5f, 4, 2, new Vector(25, 25));
            world = new World(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, player);
            tank = new Tank(world, 1, new Vector(0, 0), 1f, 4, 2, new Vector(400, 400),10);
            planes = new List<Airplane>();

            for (int i = 0; i < 10; i++)
            {
                planes.Add(new Airplane(world, 1, new Vector(0, 0), 1f, 2, 2, new Vector(200 + i * 5, 200 + i * 5), i));
            }

            for (int i = 0; i < 10; i++)
            {
                planes.Add(new Airplane(world, 1, new Vector(0, 0), 1f, 2, 2, new Vector(30 + i * 5, 200 + i * 5), i));
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
            font = Content.Load<SpriteFont>("Arial");

            //Load player texture
            FileStream fileStream = new FileStream("Content/Sprites/Player.png", FileMode.Open);
            player.PlayerTexture = Texture2D.FromStream(GraphicsDevice, fileStream);

            //Load sand tile texture
            fileStream = new FileStream("Content/Sprites/SandTile.png", FileMode.Open);
            world.TileTexture = Texture2D.FromStream(GraphicsDevice, fileStream);

            //Load tank
            fileStream = new FileStream("Content/Sprites/TankBottom.png", FileMode.Open);
            tank.Texture = Texture2D.FromStream(GraphicsDevice, fileStream);
            fileStream = new FileStream("Content/Sprites/TankTop.png", FileMode.Open);
            tank.TankTopTexture = Texture2D.FromStream(GraphicsDevice, fileStream);
            fileStream = new FileStream("Content/Sprites/DebugTarget.png", FileMode.Open);
            tank.TargetTexture = Texture2D.FromStream(GraphicsDevice, fileStream);

            //Load Planes
            fileStream = new FileStream("Content/Sprites/Airplane.png", FileMode.Open);
           
            foreach (Airplane plane in planes)
            {
                plane.PlaneTexture = Texture2D.FromStream(GraphicsDevice, fileStream);
            }

            //If debugging is enabled load the textures (maybe this should always be done in case debugging can be enabled in runtime)
            if (GlobalVars.debug ==true)
            {
                fileStream = new FileStream("Content/Sprites/DebugNeighbor.png", FileMode.Open);
                tank.TileDebugNeighborTexture = Texture2D.FromStream(GraphicsDevice, fileStream);
                fileStream = new FileStream("Content/Sprites/DebugCenter.png", FileMode.Open);
                tank.TileDebugCenterTexture = Texture2D.FromStream(GraphicsDevice, fileStream);
            }
            fileStream.Dispose();

            // TODO: use this.Content to load your game content here
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
            GlobalVars.TimeElapsed += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            tank.Update(gameTime.ElapsedGameTime.Milliseconds);
            world.Update(gameTime.ElapsedGameTime.Milliseconds);
            foreach(Airplane plane in planes)
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

            //world should only be drawed once with its elements
            // The entities should update themselfs and draw/render
            world.Render(spriteBatch, graphics.GraphicsDevice);
            tank.Render(spriteBatch);
            foreach (Airplane plane in planes)
            {
                plane.Render(spriteBatch);
            }

            spriteBatch.Begin();

            spriteBatch.DrawString(font, "Tankpos x:" + tank.Position.X + " \n Tankpos y:" + tank.Position.Y, new Vector2(0, 30), Color.Black);

            spriteBatch.End();

            // TODO: Add your drawing code here
          //  world.Draw(spriteBatch, graphics.GraphicsDevice); safdgzhk

            base.Draw(gameTime);
        }
    }
}
