using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationModel
{
    public abstract class iModelAPI
    {
        public static iModelAPI Instance() { 
            return new ModelAPI();
        }

        public abstract void start(int amount, int radius);
        public abstract void removeBalls();
        public abstract ObservableCollection<iModelBall> ModelBalls();
    }
}
