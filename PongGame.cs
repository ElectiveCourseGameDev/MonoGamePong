using System.Linq.Expressions;
using Windows.UI.Xaml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGamePong.GameFrameWork;

namespace MonoGamePong
{
    /// <summary>
    /// This is the main type for your game
    /// 
    /// </summary>
    public class PongGame : GameHost
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        private Texture2D _redPlayer;
        private Texture2D _playerTexture;
        private Texture2D _ballTexture;
        private SpriteFont _logFont;

        private MonoLog _Log;
        private Player _p1;
        private Player _p2;

        private Ball _ball;
        private KeyboardState currentKeyState;
        private float _playerSpeed = 2;

        public PongGame()
        {
            _graphics = new GraphicsDeviceManager(this);
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
           
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _playerTexture = generateTexture2D(30, 200, Color.White);
            _ballTexture = generateTexture2D(20, 20, Color.White);
            _logFont = Content.Load<SpriteFont>(@"MonoLog");
            
            //load test png
            _redPlayer = Content.Load<Texture2D>(@"player");

            ResetGame();
        }

        private void ResetGame()
        {
            _p1 = new Player(this, new Vector2(50, 50), _playerTexture);
            GameObjects.Add(_p1);
            
            _p2 = new Player(this, new Vector2(2300, 50), _playerTexture);
            GameObjects.Add(_p2);

            _ball = new Ball(this, new Vector2(1200f, 800f), _ballTexture);
            _ball.BallSpeed = 2;
            GameObjects.Add(_ball);

            _Log = new MonoLog(this, _logFont, Color.DarkOrange);
            GameObjects.Add(_Log);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // TODO: Add your update logic here

            //Read keyboard---------------------------------------------------------
            currentKeyState = Keyboard.GetState();
            if (currentKeyState.IsKeyDown(Keys.W))
            {
                _p1.PositionY -= _playerSpeed;
            }

            if (currentKeyState.IsKeyDown(Keys.S))
            {
                _p1.PositionY += _playerSpeed;
            }

            if (currentKeyState.IsKeyDown(Keys.Up))
            {
                _p2.PositionY -= _playerSpeed;
            }

            if (currentKeyState.IsKeyDown(Keys.Down))
            {
                _p2.PositionY += _playerSpeed;
            }

            if (currentKeyState.IsKeyDown(Keys.Space))
            {
                _Log.Write("Ball: " + _ball.BoundingBox.ToString());
                _Log.Write("P2: " + _p2.BoundingBox.ToString());
                _Log.Write("P1: " + _p1.BoundingBox.ToString());
            }

            if (currentKeyState.IsKeyDown(Keys.Pause))
            {

            }

            //collisions detection---------------------------------------------------

            Point ballCornerA = new Point((int) _ball.PositionX, (int) _ball.PositionY);
            Point ballCornerB = new Point((int) _ball.PositionX + (int) _ball.BoundingBox.Width, (int) _ball.PositionY);
            Point ballCornerC = new Point((int) _ball.PositionX, (int) _ball.PositionY + _ball.BoundingBox.Height);
            Point ballCornerD = new Point((int) _ball.PositionX + (int) _ball.BoundingBox.Width,
                (int) _ball.PositionY + (int) _ball.BoundingBox.Height);

            if (_p1.BoundingBox.Contains(ballCornerA) || _p1.BoundingBox.Contains(ballCornerB) ||
                _p1.BoundingBox.Contains(ballCornerC) || _p1.BoundingBox.Contains(ballCornerD))
            {
                _ball.flip();
                _Log.Write(_ball.DirectionInDegrees.ToString());
                //_ball.BallSpeed = 2;
                _Log.Write("P1 Colision!");
            }

            if (_p2.BoundingBox.Contains(ballCornerA) || _p2.BoundingBox.Contains(ballCornerB) ||
                _p2.BoundingBox.Contains(ballCornerC) || _p2.BoundingBox.Contains(ballCornerD))
            {
                _ball.flip();
                _Log.Write(_ball.DirectionInDegrees.ToString());
                //_ball.BallSpeed = -2;
                _Log.Write("p2 Colision!");
            }

            if (ballCornerA.Y == 0)
            {
                _ball.PositionY = 1440;
            }

            if (ballCornerC.Y == 1440)
            {
                _ball.PositionY = 0;
            }




            //_Log.Write(_ball.BoundingBox.ToString());

            UpdateAll(gameTime);
            base.Update(gameTime);


        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            foreach (SpriteObject so in GameObjects)
            {
                so.Draw(gameTime, _spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private Texture2D generateTexture2D(int width, int height, Color textureColor)
        {
            Texture2D rectangleTexture = new Texture2D(this.GraphicsDevice, width, height, false, SurfaceFormat.Color);
            Color[] color = new Color[width * height];
            for (int i = 0; i < color.Length; i++)
            {
                color[i] = textureColor;
            }
            rectangleTexture.SetData(color);//set the color data on the texture
            return rectangleTexture;//return the texture
        }
    }
}
