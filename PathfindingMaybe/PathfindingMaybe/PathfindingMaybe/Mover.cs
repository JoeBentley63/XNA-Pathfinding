using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PathfindingMaybe
{
    class Mover
    {
        List<Vector2> _path = new List<Vector2>();
        Texture2D _texture;
        Vector2 _position;

        public Mover(ContentManager _content,Vector2 _position)
        {
            this._texture = _content.Load<Texture2D>("tile");
            this._position = _position;
        }

        public void SetPath(List<Vector2> _path)
        {
            this._path = _path;
        }

        public void Update()
        {
            if (_path.Count > 0)
            {
                if (this._position.X < _path[0].X)
                {
                    _position.X+=1f;
                }

                if (this._position.X > _path[0].X)
                {
                    _position.X-=1f;
                }
                
                if (this._position.Y < _path[0].Y)
                {
                    _position.Y+=1f;
                }

                if (this._position.Y > _path[0].Y)
                {
                    _position.Y-=1f;
                }
                if (Vector2.Distance(_path[0], this._position) < 0.5f)
                {
                    _path.RemoveAt(0);
                }
            }

        }
        
        public void Draw(SpriteBatch _spriteBatch)
        {
            //_spriteBatch.Draw(_texture, _position, Color.Black);

        }
    }
}
