using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Topic_9___making_a_player_class
{
    public class Game1 : Game
    {
        Player blob, blob2;

        Rectangle window;

        KeyboardState keyboardState;

        Texture2D blobTexture;
        Texture2D whiteCircle;
        Texture2D whiteSqaure;

        List<Rectangle> barriers;
        List<Food> food;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            barriers = new List<Rectangle>();
            barriers.Add(new Rectangle(100, 100, 10, 200));
            barriers.Add(new Rectangle(400, 400, 100, 10));

            window = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            food = new List<Food>();
            food.Add(new Food(whiteCircle, new Rectangle(50, 50, 10, 10), new Vector2(0, 2)));
            food.Add(new Food(whiteCircle, new Rectangle(600, 100, 10, 10)));
            food.Add(new Food(whiteCircle, new Rectangle(50, 200, 10, 10)));

            base.Initialize();
            blob = new Player(blobTexture, 10, 10, window);
            blob2 = new Player(blobTexture, 760, 10, window);
            blob2.ColorMask = Color.Green;
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            blobTexture = Content.Load<Texture2D>("blob");
            whiteCircle = Content.Load<Texture2D>("whiteCircle");
            whiteSqaure = Content.Load<Texture2D>("whiteSquare");


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            blob.HSpeed = 0;
            blob.VSpeed = 0;

            if (keyboardState.IsKeyDown(Keys.D))
                blob.HSpeed += 3;
            if (keyboardState.IsKeyDown(Keys.A))
                blob.HSpeed += -3;
            if (keyboardState.IsKeyDown(Keys.W))
                blob.VSpeed += -3;
            if (keyboardState.IsKeyDown(Keys.S))
                blob.VSpeed += 3;

            blob.Update();


            blob2.HSpeed = 0;
            blob2.VSpeed = 0;

            if (keyboardState.IsKeyDown(Keys.Right))
                blob2.HSpeed += 3;
            if (keyboardState.IsKeyDown(Keys.Left))
                blob2.HSpeed += -3;
            if (keyboardState.IsKeyDown(Keys.Up))
                blob2.VSpeed += -3;
            if (keyboardState.IsKeyDown(Keys.Down))
                blob2.VSpeed += 3;

            blob2.Update();

            foreach (Food bit in food)
            {
                bit.Move(_graphics);
            }

            foreach (Rectangle barrier in barriers)
                if (blob.Collide(barrier))
                    blob.UndoMove();

            foreach (Rectangle barrier in barriers)
                if (blob2.Collide(barrier))
                    blob2.UndoMove();

            for (int i = 0; i < food.Count; i++)
                if (blob.Collide(food[i].Bounds))
                {
                    food.RemoveAt(i);
                    blob.Grow();
                    i--;
                }
            for (int i = 0; i < food.Count; i++)
                if (blob2.Collide(food[i].Bounds))
                {
                    food.RemoveAt(i);
                    blob2.Grow();
                    i--;
                }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkMagenta);

            _spriteBatch.Begin();

            blob.Draw(_spriteBatch);
            blob2.Draw(_spriteBatch);

            foreach (Rectangle barrier in barriers)
                _spriteBatch.Draw(whiteSqaure, barrier, Color.White);

            foreach (Food bit in food)
                _spriteBatch.Draw(whiteCircle, bit.Bounds, Color.Green);

            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}