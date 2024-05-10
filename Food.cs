using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topic_9___making_a_player_class
{
    internal class Food
{

        private Texture2D _texture;
        private Rectangle _rectangle;
        private Vector2 _speed;


        Random _generator = new Random();

        public Food(Texture2D texture, Rectangle rect, Vector2 speed)
        {
            _texture = texture;
            _rectangle = rect;
            _speed = speed;
        }

        public Food(Texture2D texture, Rectangle rect)
        {
            _speed = new Vector2(_generator.Next(1, 10), _generator.Next(1, 10));
            _texture = texture;
            _rectangle = rect;
        }

        public Food(List<Texture2D> textures, Rectangle rect)
        {
            _speed = new Vector2(_generator.Next(1, 5), _generator.Next(1, 5));
            _rectangle = rect;
            _texture = textures[_generator.Next(textures.Count)];
            ;
        }

        public Texture2D Texture
        {
            get { return _texture; }
        }

        public Rectangle Bounds
        {
            get { return _rectangle; }
            set { _rectangle = value; }
        }

        public void Move(GraphicsDeviceManager graphics)
        {


            _rectangle.Offset(_speed);
            if (_rectangle.Right > graphics.PreferredBackBufferWidth || _rectangle.Left < 0)
            {
                _speed.X *= -1;
            }
            if (_rectangle.Bottom > graphics.PreferredBackBufferHeight || _rectangle.Top < 0)
            {
                _speed.Y *= -1;
            }

        }
    }
}
