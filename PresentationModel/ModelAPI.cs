using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;


namespace PresentationModel
{
    internal class ModelAPI : iModelAPI
    {
        private LogicAbstractAPI logic;
        ObservableCollection<iModelBall> observedBalls;

        public ModelAPI()
        {
            logic = LogicAbstractAPI.initialize();
        }


        public override ObservableCollection<iModelBall> ModelBalls()
        {
            observedBalls.Clear();
            foreach (IBall ball in logic.GetBalls()) { 
                ModelBall observedBall = new ModelBall(ball.posX, ball.posY, ball.radius);
                observedBalls.Add(observedBall);
            }
            return observedBalls;
        }

        public override void removeBalls()
        {
            logic.removeBalls();
        }

        public override void start(int amount, int radius)
        {
            logic.addBalls(amount, radius);
            logic.begin();
        }
    }
}
