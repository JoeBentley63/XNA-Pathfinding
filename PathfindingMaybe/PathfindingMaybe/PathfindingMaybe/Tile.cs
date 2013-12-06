using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace PathfindingMaybe
{
    class Tile : IComparable
    {
        Texture2D _texture;
        public Vector2 _pos;
        public bool _walkable;
        public Color _colour = Color.Black;
        public float _weight = 0;
        public Tile _previous = null;
        public float _time = 999*1000f;
        public float _startTime = 0f;
        public bool _finalPath = false;
        public float _finalPathTime = 99999f;
        public Tile(bool _walkable,Texture2D _texture,Vector2 _pos)
        {
            _startTime = System.Environment.TickCount;
            this._walkable = _walkable;
            this._pos = _pos;
            this._texture = _texture;
            if (_walkable == true)
            {
                _colour = Color.White;
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_texture, _pos, _colour);
        }

        public void Update()
        {
            if (System.Environment.TickCount >(_startTime + _time) && _walkable)
            {
                _colour = Color.Green;
            }
            if (System.Environment.TickCount > (_startTime + _finalPathTime) && _walkable && _finalPath == true)
            {
                _colour = Color.Red;
            }
        }

        public int CompareTo(Object _object)
        {
            return (int)(this._weight - ((Tile)_object)._weight);
        }

        public override string ToString()
        {
            return ("Tile at :" + _pos + " with a weight of : " + _weight); 
        }
    }

    
}
