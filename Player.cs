using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topic_9___making_a_player_class
{
    internal class Player
{
        private Texture2D _texture;
        private Rectangle _location;
        private Vector2 _speed;
        private Rectangle _window;

        public Player(Texture2D texture, int x, int y, Rectangle window)
        {
            _texture = texture;
            _location = new Rectangle(x, y, 30, 30);
            _speed = new Vector2();
            _window = window;
        }
        public float HSpeed
        {
            get { return _speed.X; }
            set { _speed.X = value; }
        }
        public float VSpeed
        {
            get { return _speed.Y; }
            set { _speed.Y = value; }
        }
        private void Move()
        {
            _location.Offset(_speed);
            if (!_window.Contains(_location))
                UndoMove();


        }
        public void Update()
        {
            Move();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _location, Color.White);
        }
        public bool Collide(Rectangle item)
        {
            return _location.Intersects(item);
        }
        public void UndoMove()
        {
            _location.Offset(-_speed);

        }
        public void Grow()
        {
            _location.Width += 1;
            _location.Height += 1;
        }
       

    }
}
