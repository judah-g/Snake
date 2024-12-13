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
        private Direction _direction, _prevDirection;
        private float _speed, _rotation;
        private Pixel _pixel;

        private bool _needsToGrow;

        public Snake(Texture2D texture, Rectangle rect, Direction direction, float speed, Pixel pixel) 
        { 
            _texture = texture;
            _rect = rect;
            _direction = direction;
            _speed = speed;
            _pixel = pixel;
        }

        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        public Direction Direction 
        { 
            get { return _direction; } 
            set { _direction = value; }
        }

        public Direction PreviousDirection
        {
            get { return _prevDirection; }
            set { _prevDirection = value; }
        }

        public float Speed
        { get { return _speed; } }

        public Rectangle Rectangle
        { get { return _rect; } }

        public bool NeedsToGrow
        {
            get { return _needsToGrow; }
            set { _needsToGrow = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Rectangle(_rect.X + (_pixel.Width / 2), _rect.Y + (_pixel.Width / 2), _pixel.Width, _pixel.Width), 
                null,Color.White, _rotation, new Vector2(_texture.Width / 2, _texture.Height / 2), SpriteEffects.None, 0f);
        }

        public void Update(float timer, Fruit fruit, List<Snake> snakes, GraphicsDeviceManager graphics, Screen screen)
        {
            //movement
            if (timer >= _speed)
            {
               if (_direction == Direction.Right)
                    _rect.X += _rect.Width;
               else if (_direction == Direction.Left)
                    _rect.X -= _rect.Width;
               else if (_direction == Direction.Up)
                    _rect.Y -= _rect.Height;
               else if (_direction == Direction.Down)
                    _rect.Y += _rect.Height;
            }

            if (_prevDirection == Direction.Up)
                _rotation = -1.5708f;
            else if (_prevDirection == Direction.Down)
                _rotation = 1.5708f;
            else if (_prevDirection == Direction.Right)
                _rotation = 0;
            else if (_prevDirection == Direction.Left)
                _rotation = (float)Math.PI;

            //growing
            if (_rect.Intersects(fruit.Rectangle))
            { fruit.Eat(snakes, graphics); }

            if (snakes[0].NeedsToGrow && timer >= _speed)
            {
                if (snakes[^1].PreviousDirection == Direction.Right)
                {
                    snakes.Add(new Snake(snakes[0].Texture, new Rectangle(snakes[^1].Rectangle.X - _pixel.Width, snakes[^1].Rectangle.Y, _pixel.Width, _pixel.Width),
                        Direction.Right, snakes[0].Speed, _pixel));
                }
                else if (snakes[^1].PreviousDirection == Direction.Left)
                {
                    snakes.Add(new Snake(snakes[0].Texture, new Rectangle(snakes[^1].Rectangle.X + _pixel.Width, snakes[^1].Rectangle.Y, _pixel.Width, _pixel.Width),
                        Direction.Right, snakes[0].Speed, _pixel));
                }
                else if (snakes[^1].PreviousDirection == Direction.Up)
                {
                    snakes.Add(new Snake(snakes[0].Texture, new Rectangle(snakes[^1].Rectangle.X, snakes[^1].Rectangle.Y + _pixel.Width, _pixel.Width, _pixel.Width),
                        Direction.Right, snakes[0].Speed, _pixel));
                }
                else if (snakes[^1].PreviousDirection == Direction.Down)
                {
                    snakes.Add(new Snake(snakes[0].Texture, new Rectangle(snakes[^1].Rectangle.X, snakes[^1].Rectangle.Y - _pixel.Width, _pixel.Width, _pixel.Width),
                        Direction.Right, snakes[0].Speed, _pixel));
                }
                snakes[0].NeedsToGrow = false;
            }
        }
    }
}
