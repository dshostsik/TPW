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
        
        public abstract int _width { get; }
        public abstract int _height { get;  }

        public abstract void AddBalls(int amount);
        
        public static iModelAPI Instance(int width, int height) { 
            return new ModelAPI(width, height);
        }

        public ObservableCollection<ModelBall> Balls;
        public abstract void stop();

        internal class ModelAPI : iModelAPI
        {
            private LogicAbstractAPI logic;
            public override int _width { get; }
            public override int _height { get; }

            public override void AddBalls(int amount)
            {
                logic.addBalls(amount);
                for (int i = 0; i < amount; i++)
                {
                    ModelBall ballModel = new ModelBall(logic.getPositionOfABall(i).X, logic.getPositionOfABall(i).Y,
                        logic.getBallsRadius(i));
                    Balls.Add(ballModel);
                }
            }

            public ModelAPI(int width, int height)
            {
                _width = width;
                _height = height;
                logic = LogicAbstractAPI.initialize(_width, _height);
                Balls = new ObservableCollection<ModelBall>();
                logic.LogicLayerEvent += UpdateBall;
            }

            private void UpdateBall(object? sender, (int id, float x, float y, int radius) args)
            {
                if (args.id >= Balls.Count)
                {
                    return;
                }

                Balls[args.id].Move(args.x - args.radius, args.y - args.radius);
            }

            public override void stop()
            {
                logic.removeBalls();
                Balls.Clear();
            }
        }
    }

    
}
