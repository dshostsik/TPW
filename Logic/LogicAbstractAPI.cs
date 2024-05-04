using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    public abstract class LogicAbstractAPI
    {
        public static LogicAbstractAPI initialize(DataAbstract? dataAbstract = null) {
            if (dataAbstract != null) { 
                return new Table(720, 1080, dataAbstract); 
            }
            else {
                return new Table(720, 1080, DataAbstract.init());
            }
        }

        public abstract void addBalls(int numberOfBallsToDeploy, int radiusOfBalls);
        public abstract void removeBalls();
        public abstract void begin();
        public abstract List<List<int>> getPositions();
        public abstract List<IBall> GetBalls();
    }
}
