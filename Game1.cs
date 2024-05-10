using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Topic_9___making_a_player_class
{
    public class Game1 : Game
    {
        Player blob;

        Food food1, food2, food3;

        Rectangle window;

        KeyboardState keyboardState;

        Texture2D blobTexture;
        Texture2D whiteCircle;
        Texture2D whiteSqaure;

        List<Rectangle> barriers;
        List<Rectangle> food;

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

            food1 = new Food (whiteCircle, new Rectangle(50, 50, 10, 10), new Vector2(0,2));
            food2 = new Food (whiteCircle, new Rectangle(600, 100, 10, 10));
            food3 = new Food (whiteCircle, new Rectangle(50, 200, 10, 10));

            food = new List<Rectangle>();
            food.Add(food1.Bounds);
            food.Add(food2.Bounds);
            food.Add(food3.Bounds);

            base.Initialize();
            blob = new Player(blobTexture, 10, 10, window);
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

            food1.Move(_graphics);
            food2.Move(_graphics);
            food3.Move(_graphics);

            foreach (Rectangle barrier in barriers)
                if (blob.Collide(barrier))
                    blob.UndoMove();

            for (int i = 0; i < food.Count; i++)
                if (blob.Collide(food[i]))
                {
                    food.RemoveAt(i);
                    blob.Grow();
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

            foreach (Rectangle barrier in barriers)
                _spriteBatch.Draw(whiteSqaure, barrier, Color.White);

            //foreach (Rectangle bit in food)
            //    _spriteBatch.Draw(whiteCircle, bit, Color.Green);

            _spriteBatch.Draw(whiteCircle, food1.Bounds, Color.White);
            _spriteBatch.Draw(whiteCircle, food2.Bounds, Color.White);
            _spriteBatch.Draw(whiteCircle, food3.Bounds, Color.White);

            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}