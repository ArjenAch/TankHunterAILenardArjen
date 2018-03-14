using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
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
            player = new Player(1, new Vector(0,0), 1.5f, 4, 2, new Vector(0,0));
            world = new World(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
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

            FileStream fileStream = new FileStream("Content/Sprites/Player.png", FileMode.Open);
            player.PlayerTexture = Texture2D.FromStream(GraphicsDevice, fileStream);
            fileStream = new FileStream("Content/Sprites/SandTile.png", FileMode.Open);
            world.TileTexture = Texture2D.FromStream(GraphicsDevice, fileStream);

            //If debugging is enabled load the textures (maybe this should always be done in case debugging can be enabled in runtime)
            if(GlobalVars.debug ==true)
            {
                fileStream = new FileStream("Content/Sprites/DebugNeighbor.png", FileMode.Open);
                world.TileDebugNeighborTexture = Texture2D.FromStream(GraphicsDevice, fileStream);
                fileStream = new FileStream("Content/Sprites/DebugCenter.png", FileMode.Open);
                world.TileDebugCenterTexture = Texture2D.FromStream(GraphicsDevice, fileStream);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime.ElapsedGameTime.Milliseconds);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LawnGreen);
            
            // Render the player
            player.Render(spriteBatch);

            //world should only be drawed once with its elements
            // The entities should update themselfs and draw/render
            world.Draw(spriteBatch, graphics.GraphicsDevice);
            base.Draw(gameTime);
        }
    }
}
