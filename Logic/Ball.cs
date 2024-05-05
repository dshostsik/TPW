using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Logic
{

    internal class Ball : IBall, INotifyPropertyChanged
    {
        public override event PropertyChangedEventHandler? PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public int _posX { get; set; }
        public int _posY { get; set; }
        public int _radius { get; set; }
        private int _speedX { get; set; }
        private int _speedY { get; set; }
        public override int posX { get => _posX; set { _posX = value; RaisePropertyChanged(String.Empty); } }            
        public override int posY { get => _posY; set { _posY = value; RaisePropertyChanged(String.Empty); } }
        public override int radius { get => _radius; set => _radius = value; }

        public Ball(int radius, int x, int y) {
            _radius = radius;
            _posX = x;
            _posY = y;
        }

        public override bool isInside (int xBorder, int yBorder)
        {
            if (((this._posX+this._speedX+this.radius < xBorder) && (this._posX+this._speedX - this.radius > 0)) 
                    &&
                 ((this._posY + this._speedY - this.radius > 0) && (this._posY + this._speedY + this.radius < yBorder)))
            {
                return true;
            }
            return false;   
        }

        public override void move(int deltaX, int deltaY)
        {
           _posX += deltaX;
           _posY += deltaY;
        }

        public override void setSpeed(int newXSpeed, int newYSpeed)
        {
            _speedX = newXSpeed;
            _speedY = newYSpeed;
        }

        public override int getXSpeed()
        {
            return _speedX;
        }

        public override int getYSpeed()
        {
            return _speedY;
        }
    }
}