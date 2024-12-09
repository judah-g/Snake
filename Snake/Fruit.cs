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
        private Random _random = new Random();
        private Vector2 _helper;
        private bool _empty;
        private Pixel _pixel;

        public Fruit(Rectangle rect, Texture2D texture, Pixel pixel)
        {
            _rect = rect; 
            _texture = texture;
            _pixel = pixel;
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
                _helper = new Vector2(_random.Next(graphics.PreferredBackBufferWidth / _pixel.Width), _random.Next(graphics.PreferredBackBufferHeight / _pixel.Width));
                _empty = true;
                for (int i = 0; i < snakes.Count; i++)
                {
                    if (snakes[i].Rectangle.Contains(_helper))
                        _empty = false;
                }
            }
            _rect = new Rectangle((int)_helper.X * _pixel.Width, (int)_helper.Y * _pixel.Width, _pixel.Width, _pixel.Width);

            snakes[0].NeedsToGrow = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rect, Color.White);
        }
    }
}
