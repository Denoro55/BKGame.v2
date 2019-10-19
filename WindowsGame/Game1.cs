using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame
{
    enum Stat
    {
        SplashScreen,
        Game
    }
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Stat Stat = Stat.SplashScreen;

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
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.ApplyChanges();
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
            SplashScreen.Background = Content.Load<Texture2D>("background");
            SplashScreen.Font = Content.Load<SpriteFont>("SplashFont");

            Asteroids.Init(spriteBatch, 800, 480);

            Star.Texture2D = Content.Load<Texture2D>("Star");

            Player.Texture2D = Content.Load<Texture2D>("Player");
            Target.Texture2D = Content.Load<Texture2D>("Target");

            Enemy.Texture2D = Content.Load<Texture2D>("Enemy");
            Bullet.Texture2D = Content.Load<Texture2D>("Bullet");
            Bullet.EnemyTexture2D = Content.Load<Texture2D>("EnemyBullet");

            Asteroids.healthTexture = Content.Load<Texture2D>("Health");
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
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            switch (Stat)
            {
                case Stat.SplashScreen:
                    SplashScreen.Update();
                    if (keyboardState.IsKeyDown(Keys.Space))
                    {
                        Stat = Stat.Game;
                    }
                    break;
                case Stat.Game:
                    Asteroids.Update();
                    float time = (float)gameTime.TotalGameTime.TotalMilliseconds / 10;
                    if (mouseState.LeftButton == ButtonState.Pressed && time > Asteroids.playerReload)
                    {
                        Asteroids.CreateBullet();
                        Asteroids.playerReload = time + 15;
                    }
                    if (keyboardState.IsKeyDown(Keys.M))
                    {
                        Stat = Stat.SplashScreen;
                    }
                    if (keyboardState.IsKeyDown(Keys.W)) Asteroids.Player.Up();
                    if (keyboardState.IsKeyDown(Keys.S)) Asteroids.Player.Down();
                    if (keyboardState.IsKeyDown(Keys.D)) Asteroids.Player.Right();
                    if (keyboardState.IsKeyDown(Keys.A)) Asteroids.Player.Left();
                    break;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            switch (Stat)
            {
                case Stat.SplashScreen:
                    SplashScreen.Draw(spriteBatch);
                    break;
                case Stat.Game:
                    Asteroids.Draw();
                    break;
            }

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
