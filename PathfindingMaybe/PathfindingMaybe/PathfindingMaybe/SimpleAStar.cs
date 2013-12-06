using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PathfindingMaybe
{
    class SimpleAStar
    {
        List<Tile> _tilesVisited = new List<Tile>();
        List<Tile> _availableTiles = new List<Tile>();
        int _height;
        int _width;
        Vector2 _target = Vector2.Zero;
        public Tile[,] _level;
        List<Vector2> _path;
        int num = 0;

        public struct coord
        {
            public int X;
            public int Y;
        };


        public SimpleAStar(Tile[,] _level)
        {
            this._level = _level;
            _width = (int)Math.Sqrt(_level.Length);
            _height = _width;
        }

        public List<Vector2> FindPath(Tile _start,Vector2 _end)
        {
            _target = _end ;
            foreach (Tile item in _level)
            {
                item._weight = Vector2.Distance(item._pos, _target);
            }
            _path = new List<Vector2>();
            ProcessNode(_start);

            return _path;
        }

        public void ProcessNode(Tile _tile)
        {
            num++;
            _tile._time = num * 100;
            Console.WriteLine("ProcessNode : " + _tile);
            _availableTiles.Remove(_tile);
            _tilesVisited.Add(_tile);
            
            foreach(Tile item in FindNeighbours(_tile))
            {
                _availableTiles.Add(item);
                Console.WriteLine("Added " + item + " to Neighbours");
            }
            Console.WriteLine("Num of available tiles = " + _availableTiles.Count);
            if(_availableTiles.Count>0)
            {
                int _index = 0;
                float _distance = 999999;
                for(int i = 0; i < _availableTiles.Count; i++)
                {
                    if (_availableTiles[i]._weight < _distance)
                    {
                        _distance = _availableTiles[i]._weight;
                        _index = i;
                    }
                }
                Console.WriteLine("Should we gen final path? ::::" + _availableTiles[_index]._pos + " and " + _target);
                
                if (_availableTiles[_index]._pos == _target || Vector2.Distance(_availableTiles[_index]._pos,_target )<10)
                {
                    Console.WriteLine("Yes");
                    GeneratePath(_availableTiles[_index]);
                }
                
                else
                {
                    Console.WriteLine("No");
                    ProcessNode(_availableTiles[_index]);
                }
            }

        }

        public void GeneratePath(Tile _endNode)
        {
            Console.WriteLine("Gen Path:----");
            List<Vector2> _finalPath = new List<Vector2>();
            List<Tile> _FPath = new List<Tile>();
            Stack<Tile> _reversePath = new Stack<Tile>();

            _reversePath.Push(_endNode);
            Tile _currentTile = _endNode;
            while (_currentTile._previous != null)
            {
                Console.WriteLine("Enqueued : " + _currentTile._previous._pos);
                _reversePath.Push(_currentTile._previous);
                _currentTile = _currentTile._previous;
            }
            Console.WriteLine("num units : " + _reversePath.Count);

            while(_reversePath.Count >0)
            {
            

                Tile _temp =_reversePath.Pop();
                Console.WriteLine("Added " + _temp);
                _finalPath.Add(_temp._pos);
                _FPath.Add(_temp);
            }
            Console.WriteLine("FINAL PATH BITCHES:-----------------------------");
            for (int i = 0; i < _finalPath.Count; i++)
            {
                Console.WriteLine(i + " : " + _finalPath[i]);
                _FPath[i]._finalPathTime = num * 100f;
                num++;
                _FPath[i]._finalPath = true;
            }

            _path = _finalPath;
        }

        public List<Tile> FindNeighbours(Tile _tile)
        {
            Console.WriteLine("Find Neighbours for : " + _tile._pos);
            List<Tile> _neighbours = new List<Tile>();
            Vector2 _pos = _tile._pos / 32;
            float temp = _pos.Y;
            _pos.Y = _pos.X;
            _pos.X = temp;
            coord _position;
            _position.X = (int)_pos.X;
            _position.Y = (int)_pos.Y;


            if (_position.X - 1 > 0)
            {
                if (_level[_position.X - 1, _position.Y]._walkable == true && _tilesVisited.Contains(_level[_position.X - 1, _position.Y]) == false)
                {
                    Console.WriteLine("Left Neighbour found at : " + (_position.X-1) + " , " + _position.Y);
                    _neighbours.Add(_level[_position.X - 1, _position.Y]);
                }
            }
            if (_position.X + 1 < _width)
            {
                if (_level[_position.X + 1, _position.Y]._walkable == true && _tilesVisited.Contains(_level[_position.X + 1, _position.Y]) == false)
                {
                    Console.WriteLine("Right Neighbour found at : " + (_position.X + 1) +" , " +  _position.Y);
                    _neighbours.Add(_level[_position.X + 1, _position.Y]);
                }
            }
            if (_position.Y - 1 > 0)
            {
                if (_level[_position.X, _position.Y - 1]._walkable == true && _tilesVisited.Contains(_level[_position.X, _position.Y - 1]) == false)
                {
                    Console.WriteLine("Up neighbour found at : " + _position.X + " , " + (_position.Y - 1));
                    _neighbours.Add(_level[_position.X, _position.Y - 1]);
                }
            }
            if (_position.Y + 1 < _width)
            {
                if (_level[_position.X, _position.Y + 1]._walkable == true && _tilesVisited.Contains(_level[_position.X, _position.Y + 1]) == false)
                {
                    Console.WriteLine("Down Neighbour found at : " + _position.X + " , " + (_position.Y + 1));
                    _neighbours.Add(_level[_position.X, _position.Y + 1]);
                }
            }

            foreach (Tile item in _neighbours)
            {
                item._previous = _tile;
            }
            return _neighbours;
        }
    }
}
