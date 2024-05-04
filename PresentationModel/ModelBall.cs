using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationModel
{
    internal class ModelBall : iModelBall, INotifyPropertyChanged
    {
        public override event PropertyChangedEventHandler? PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public ModelBall(int X, int Y, int R) { 
            _posix = X;
            _posix = Y;
            _posix = R;
        }

        public override int posix { get => _posix;
            set { _posix = value; RaisePropertyChanged("PositionXModelBall"); } }
        public override int posiy { get => _posiy;
            set { _posiy = value; RaisePropertyChanged("PositionYModelBall"); } }
        public override int radius { get => _radius;
            set { _radius = value; RaisePropertyChanged("RadiusModelBall"); }
        }

        private int _posix { get; set;  }
        private int _posiy { get; set; }
        private int _radius { get; set; }
        

        public override void updateModelBall(object o, PropertyChangedEventArgs args)
        {
            IBall ballToUpdate = (IBall)o;
            if (args.PropertyName == "PositionXModelBall")
            {
                posix = ballToUpdate.posX;
            }
            else if (args.PropertyName == "PositionYModelBall") {
                posiy = ballToUpdate.posY;
            }
        }
    }
}
