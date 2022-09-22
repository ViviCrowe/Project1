using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Data.Common;

namespace Project1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D ball;
        private Vector2 ballLocation;
        private float ballRotation;
        private SpriteFont _font;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ballLocation = new Vector2(GraphicsDevice.Viewport.Width/2f, GraphicsDevice.Viewport.Height / 2f);
            ballRotation = 0.0f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ball = Content.Load<Texture2D>("ball");
            _font = Content.Load<SpriteFont>("Arial");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            ballRotation += 0.1f;

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                ballLocation.Y -= 10;
            } else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                ballLocation.Y += 10;
            } else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                ballLocation.X += 10;
            } else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                ballLocation.X -= 10;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            _spriteBatch.Draw(ball, ballLocation, null, Color.White, ballRotation,
                new Vector2(ball.Width / 2, ball.Height / 2), 1, SpriteEffects.None, 0);
            _spriteBatch.DrawString(_font, ballLocation.X + ", " + ballLocation.Y, Vector2.Zero, Color.Black);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}