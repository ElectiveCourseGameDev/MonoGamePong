using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGamePong.GameFrameWork;

namespace MonoGamePong
{
    public class Player : SpriteObject
    {

        public int _playerSpeed = 4;
        private KeyboardState currentKeyState; 
        public int Score  { get; set; }

         public Player(GameHost game)
            : base(game)
        {
            Score = 0;
        }

        public Player(GameHost game, Vector2 position)
            : this(game)
        {
            // Store the provided position
            Position = position;
        }

        public Player(GameHost game, Vector2 position, Texture2D texture)
            : this(game, position)
        {
            // Store the provided texture
            SpriteTexture = texture;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
