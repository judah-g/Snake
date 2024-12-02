using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Snake
{
    public class Snake
    {
        private Rectangle _rect;
        private Texture2D _texture;
        private Direction _direction;

        public Snake(Texture2D texture, Rectangle rect) 
        { 
            _texture = texture;
            _rect = rect;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rect, Color.White);
        }

        public void Update(float timer)
        {
            if (timer % 0.5 == 0)
            {
                //change next body to the previous direction of the one in front of it
            }
        }
    }
}
