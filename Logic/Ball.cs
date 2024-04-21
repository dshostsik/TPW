using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal class Ball : IBall
    {

        public int _posX { get; set; }
        public int _posY { get; set; }
        public int _radius { get; set; }
        public int _speedX { get; set; }
        public int _speedY { get; set; }
        public int posX { get => _posX; set => _posX = value; }
        public int posY { get => _posY; set => _posY = value; }
        public int radius { get => _radius; set => _radius = value; }

        public Ball(int radius, int x, int y) { 
            _radius = radius;
            _posX = x;
            _posY = y;
        }

        public bool collides(int xBorder, int yBorder)
        {
            throw new NotImplementedException();
        }

        public void move(int deltaX, int deltaY)
        {
           _posX += deltaX;
            _posY += deltaY;
        }

        public void setSpeed() //i decided to make speed constant so far
        {
            _speedX = 5;
            _speedY = 5;
        }
    }
}
