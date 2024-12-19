using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;

public enum Direction
{
    Right,
    Left, 
    Up, 
    Down
}

public enum Screen
{
    Intro,
    Main,
    Death
}

namespace Snake
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // max is 32
        Pixel pixel = new Pixel(32);

        List<Snake> snakes = new List<Snake>();
        Texture2D snakeTexture;

        Fruit fruit;
        Texture2D fruitTexture;

        float timer;
        KeyboardState keyboardState;
        MouseState mouseState;
        Screen screen = Screen.Main;
        Texture2D startScreenTexture, deathScreenTexture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = pixel.Width * (800 / pixel.Width);
            _graphics.PreferredBackBufferHeight = pixel.Width * (512 / pixel.Width);
            _graphics.ApplyChanges();

            base.Initialize();

            for (int i = 0; i < 3; i++)
            {
                snakes.Add(new Snake(snakeTexture, new Rectangle(pixel.Width * (160 / pixel.Width) - i * pixel.Width, pixel.Width * (160 / pixel.Width), pixel.Width, pixel.Width), Direction.Right, (float)0.25, pixel));
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            snakeTexture = Content.Load<Texture2D>("snakebody");
            for (int i = 0;i < snakes.Count; i++)
                snakes[i].Texture = snakeTexture;

            fruitTexture = Content.Load<Texture2D>("cherry");
            fruit = new Fruit(new Rectangle(pixel.Width * (640 / pixel.Width), pixel.Width * (320 / pixel.Width), pixel.Width, pixel.Width), fruitTexture, pixel);

            startScreenTexture = Content.Load<Texture2D>("start");
            deathScreenTexture = Content.Load<Texture2D>("death");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            keyboardState = Keyboard.GetState();

            if (screen == Screen.Main)
            {
                if (keyboardState.IsKeyDown(Keys.W) && snakes[0].Direction != Direction.Down)
                    snakes[0].Direction = Direction.Up;
                else if (keyboardState.IsKeyDown(Keys.S) && snakes[0].Direction != Direction.Up)
                    snakes[0].Direction = Direction.Down;
                else if (keyboardState.IsKeyDown(Keys.A) && snakes[0].Direction != Direction.Right)
                    snakes[0].Direction = Direction.Left;
                else if (keyboardState.IsKeyDown(Keys.D) && snakes[0].Direction != Direction.Left)
                    snakes[0].Direction = Direction.Right;

                if (snakes[0].Rectangle.Top < 0 || snakes[0].Rectangle.Bottom > _graphics.PreferredBackBufferHeight)
                    screen = Screen.Death;
                for (int i = 0; i < snakes.Count; i++)
                {
                    for (int j = 0; j < snakes.Count; j++)
                    {
                        if (i != j && snakes[i].Rectangle.Intersects(snakes[j].Rectangle))
                            screen = Screen.Death;
                    }
                }

                for (int i = 0; i < snakes.Count; i++)
                {
                    snakes[i].Update(timer, fruit, snakes, _graphics, screen);
                    if (i != snakes.Count - 1)
                        snakes[i + 1].Direction = snakes[i].PreviousDirection;
                }

                if (timer >= snakes[0].Speed)
                {
                    timer -= snakes[0].Speed;
                    for (int i = 0; i < snakes.Count; i++)
                    {
                        snakes[i].PreviousDirection = snakes[i].Direction;
                    }
                }
            }

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            if (screen == Screen.Main)
            {
                for (int i = 0; i < snakes.Count; i++)
                    snakes[i].Draw(_spriteBatch);

                fruit.Draw(_spriteBatch);
            }

            else if (screen == Screen.Intro)
                _spriteBatch.Draw(startScreenTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);

            else if (screen == Screen.Death)
                _spriteBatch.Draw(deathScreenTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
