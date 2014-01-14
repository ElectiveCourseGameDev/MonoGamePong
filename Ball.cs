using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGamePong.GameFrameWork;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGamePong.GameFrameWork;

namespace MonoGamePong
{
    class Ball : SpriteObject
    {
        public int velocityX;
        public int velocityY;
    
        public float BallSpeed { get; set; }

        //-------------------------------------------------------------------------------------
        // Class constructors

        public Ball(GameHost game)
            : base(game)
        {
            BallSpeed = 0.5f;
            velocityX = 1;
            do
            {
               velocityY = GameHelper.RandomNext(-2, 2);
            } while (velocityY == 0);
            SpriteColor = Color.White;
        }

        public Ball(GameHost game, Vector2 position)
            : this(game)
        {
            // Store the provided position
            Position = position;
        }

        public Ball(GameHost game, Vector2 position, Texture2D texture)
            : this(game, position)
        {
            // Store the provided texture
            SpriteTexture = texture;
        }

        //-------------------------------------------------------------------------------------
        // Properties

        public override void Update(GameTime gameTime)
        {
            PositionX += velocityX * BallSpeed;
            PositionY += velocityY * BallSpeed;

            base.Update(gameTime);
        }

    }

    
}
