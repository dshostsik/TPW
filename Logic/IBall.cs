using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal interface IBall
    {
        public abstract int posX { get; set; }
        public abstract int posY { get; set; }
        public abstract int radius { get; set; }

        public abstract void move(int deltaX, int deltaY);
        public abstract void setSpeed();
        public abstract bool collides(int xBorder, int yBorder);
    }
}
