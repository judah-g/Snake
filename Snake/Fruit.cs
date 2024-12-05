using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake
{
    public class Fruit
    {
        private Rectangle _rect;
        private Texture2D _texture;
        private Random _random;
        private Vector2 _helper;
        private bool _empty;

        public Fruit(Rectangle rect, Texture2D texture)
        {
            _rect = rect; 
            _texture = texture;
        }

        public Rectangle Rectangle
        { get { return _rect; } }

        public Texture2D Texture
        { 
            get { return _texture; } 
            set { _texture = value; }
        }

        public void Eat(List<Snake> snakes, GraphicsDeviceManager graphics)
        {
            _empty = false;
            while (_empty == false)
            {
                _helper = new Vector2(_random.Next(graphics.PreferredBackBufferWidth / 16), _random.Next(graphics.PreferredBackBufferHeight));
                for (int i = 0; i < snakes.Count; i++)
                {
                    if (!snakes[i].Rectangle.Contains(_helper))
                        _empty = true;
                }
            }
            _rect = new Rectangle((int)_helper.X, (int)_helper.Y, 16, 16);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rect, Color.White);
        }
    }
}
