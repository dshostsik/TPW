using System.Collections.Concurrent;
using System.Numerics;
using Data;

namespace Logic
{
    public abstract class LogicAbstractAPI
    {
        public static LogicAbstractAPI initialize(int width, int height, DataAbstract? dataAbstract = null)
        {
            return new Table(width, height, dataAbstract);
        }

        public abstract void addBalls(int numberOfBallsToDeploy);
        public abstract void removeBalls();
        public abstract int getAmountOfBalls();
        public abstract int getBallsRadius(int indexOfABall);
        public abstract Vector2 getPositionOfABall(int indexOfABall);
        public abstract event EventHandler<(int number, float x, float y, int radius)>? LogicLayerEvent;

        internal class Table : LogicAbstractAPI
        {
            private DataAbstract _dataLayer;

            public override event EventHandler<(int number, float x, float y, int radius)>? LogicLayerEvent;
            private ConcurrentDictionary<(int, int), bool> _collisionFlags = new ConcurrentDictionary<(int, int), bool>();
            private readonly object _collisionLock = new();
            public int _height
            {
                get;
                set;
            }
            public int _width
            {
                get;
                set;
            }

            public Table(int height, int width, DataAbstract data)
            {
                this._height = height;
                this._width = width;
                _dataLayer = data;

            }


            public override void addBalls(int numberOfBallsToDeploy)
            {
                _dataLayer.createBalls(numberOfBallsToDeploy);
            }

            public override void removeBalls()
            {
                _dataLayer.removeBalls();
            }

            public override int getAmountOfBalls()
            {
                return _dataLayer.getAmountOfBalls();
            }

            public override int getBallsRadius(int indexOfABall)
            {
                return _dataLayer.getBall(indexOfABall).radius;
            }

            public override Vector2 getPositionOfABall(int indexOfABall)
            {
                return _dataLayer.getBall(indexOfABall).position;
            }


            public void PositionChanged(object? sender, EventArgs args)
            {
                if(sender == null)
                {
                    return;
                }
                IBall ball = (IBall) sender;
                lock (_collisionLock)
                {
                    detectCollision(ball);
                }
            }

            private void detectCollision(IBall observedBall)
            {
                for (int i = 0; i < _dataLayer.getAmountOfBalls(); i++)
                {
                    IBall ball = _dataLayer.getBall(i);

                    if (observedBall == null)
                    {
                        continue;
                    }

                    if(!checkedCollision(observedBall, ball) && collides(observedBall, ball))
                    {

                    }
                }
            }

            private bool collides(IBall observedBall, IBall ball)
            {
                if (observedBall != null && ball != null)
                {
                    float distanceBetweenBalls = Vector2.Distance(observedBall.position, ball.position);
                    return distanceBetweenBalls <= (observedBall.radius + ball.radius);
                }
                return false;
            }

            private bool checkedCollision(IBall observedBall, IBall ball)
            {   
                return _collisionFlags.ContainsKey((observedBall.number, ball.number));
            }

            private void wallCollision(IBall observedBall)
            {
                Vector2 newSpeed = new Vector2(observedBall.position.X, observedBall.position.Y);
                if (observedBall.position.X - observedBall.radius <= 0)
                {
                    newSpeed.X = Math.Abs(observedBall.speed.X);
                }
                else if (observedBall.position.X + observedBall.radius >= _width)
                {
                    newSpeed.X = -Math.Abs(observedBall.speed.X);
                }

                if (observedBall.position.Y - observedBall.radius <= 0)
                {
                    newSpeed.Y = Math.Abs(observedBall.speed.Y);
                }
                else if (observedBall.position.Y + observedBall.radius >= _height)
                {
                    newSpeed.Y = -Math.Abs(observedBall.speed.Y);
                }
            }
        }
    }

    
}
