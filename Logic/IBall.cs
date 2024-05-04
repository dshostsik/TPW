using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public abstract class IBall :  INotifyPropertyChanged
    {
        public static IBall createInstance(int radius, int x, int y) {
            return new Ball(radius, x, y); 
        }

        public abstract int posX { get; set; }
        public abstract int posY { get; set; }
        public abstract int radius { get; set; }

        public abstract void move(int deltaX, int deltaY);
        public abstract void setSpeed(int newXSpeed, int newYSpeed);
        public abstract bool isInside(int xBorder, int yBorder);

        public abstract int getXSpeed();
        public abstract int getYSpeed();

        public abstract event PropertyChangedEventHandler? PropertyChanged; 
    }
}
