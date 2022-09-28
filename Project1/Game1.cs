using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
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
        private Texture2D ball2;
        private BoundingSphere _boundingSphere;
        private BoundingSphere _boundingSphere2;
        private Vector3 sphereLocation;
        SoundEffect _soundEffect;
        Song _song;
        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ballLocation = new Vector2(GraphicsDevice.Viewport.Width/2f, GraphicsDevice.Viewport.Height / 2f);
            ballRotation = 0.0f;
            sphereLocation = new Vector3(ballLocation.X, ballLocation.Y, 0);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ball = Content.Load<Texture2D>("ball");
            ball2 = Content.Load<Texture2D>("ball");
            _font = Content.Load<SpriteFont>("Arial");
            _soundEffect = Content.Load<SoundEffect>("pop");
            _song = Content.Load<Song>("Hellfire_Anna");

            // TODO: use this.Content to load your game content here

            _boundingSphere = new BoundingSphere(sphereLocation, ball.Width/2);
            _boundingSphere2 = new BoundingSphere(new Vector3(200, 200, 0), ball2.Width/2);

            MediaPlayer.Volume = 70;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_song);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            ballRotation += 0.1f;

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                ballLocation.Y -= 10;
                sphereLocation.Y -= 10;
            } 
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                ballLocation.Y += 10;
                sphereLocation.Y += 10;
            } 
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                ballLocation.X += 10;
                sphereLocation.X += 10;
            } 
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                ballLocation.X -= 10;
                sphereLocation.X -= 10;
            }

            _boundingSphere.Center = sphereLocation;

            if (ballLocation.X > _graphics.PreferredBackBufferWidth - ball.Width / 2)
            {
                ballLocation.X = _graphics.PreferredBackBufferWidth - ball.Width / 2;
                sphereLocation.X = _graphics.PreferredBackBufferWidth - ball.Width / 2;
            }
            else if (ballLocation.X < ball.Width / 2)
            {
                ballLocation.X = ball.Width / 2;
                sphereLocation.X = ball.Width / 2;
            }

            if (ballLocation.Y > _graphics.PreferredBackBufferHeight - ball.Height / 2)
            {
                ballLocation.Y = _graphics.PreferredBackBufferHeight - ball.Height / 2;
                sphereLocation.Y = _graphics.PreferredBackBufferWidth - ball.Width / 2;
            }
            else if (ballLocation.Y < ball.Height / 2)
            {
                ballLocation.Y = ball.Height / 2;
                sphereLocation.Y = ball.Width / 2;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            if (_boundingSphere.Intersects(_boundingSphere2))
            {
                _spriteBatch.Draw(ball2, new Vector2(200, 200), null, Color.Red, 0,
                  new Vector2(ball.Width / 2, ball.Height / 2), 1, SpriteEffects.None, 1);
                
                _soundEffect.Play();
                
            }
            else
            {
                _spriteBatch.Draw(ball2, new Vector2(200, 200), null, Color.White, 0,
                 new Vector2(ball.Width / 2, ball.Height / 2), 1, SpriteEffects.None, 1);
            }

            

            _spriteBatch.Draw(ball, ballLocation, null, Color.White, ballRotation,
                new Vector2(ball.Width / 2, ball.Height / 2), 1, SpriteEffects.None, 0);
            _spriteBatch.DrawString(_font, ballLocation.X + ", " + ballLocation.Y + "\n" + _boundingSphere.ToString() +"\n" + _boundingSphere2.ToString(), Vector2.Zero, Color.Red);
            

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}