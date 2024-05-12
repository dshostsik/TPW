using Logic;
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
        public abstract void stop();
        public abstract void removeBalls();
        public abstract ObservableCollection<iModelBall> ModelBalls();
    }

    internal class ModelAPI : iModelAPI, IDisposable
    {
        private LogicAbstractAPI logic;
        ObservableCollection<iModelBall> observedBalls;
        private Timer timer;


        public ModelAPI()
        {
            logic = LogicAbstractAPI.initialize();
        }

        public void Dispose()
        {
            timer.Dispose();
        }

        public override ObservableCollection<iModelBall> ModelBalls()
        {
            observedBalls.Clear();
            foreach (IBall ball in logic.GetBalls())
            {
                ModelBall observedBall = new ModelBall(ball.posX, ball.posY, ball.radius);
                observedBalls.Add(observedBall);
            }
            return observedBalls;
        }

        private void moveBalls(object? state)
        {
            logic.moveBall();
            for (int i = 0; i < observedBalls.Count; i++)
            {
                observedBalls[i].posix = logic.GetBalls()[i].posX;
                observedBalls[i].posiy = logic.GetBalls()[i].posY;
            }
        }

        public override void removeBalls()
        {
            logic.removeBalls();
        }

        public override void start(int amount, int radius)
        {
            logic.addBalls(amount, radius);
            timer = new Timer(moveBalls, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(10));
        }

        public override void stop()
        {
            this.Dispose();
            logic.removeBalls();
        }
    }
}
