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
        public int directionInDegrees = 120;

        public float BallSpeed { get; set; }

        //-------------------------------------------------------------------------------------
        // Class constructors

       

        public Ball(GameHost game)
            : base(game)
        {
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
            moveBall(BallSpeed);

            base.Update(gameTime);
        }

        private void moveBall(float ballSpeed)
        {
            PositionX = PositionX + (float) Math.Sin(directionInDegrees) * ballSpeed;
            PositionY = PositionY + (float) Math.Cos(directionInDegrees) * ballSpeed;
        }

        public void flip()
        {
            if (directionInDegrees > 180) directionInDegrees -= 180;
            else directionInDegrees += 180;

        }



        private void restart()
        {
            directionInDegrees = 90;
            BallSpeed = 2;
        }

    }

    
}
