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

        public abstract int Width { get; set; }
        public abstract int Height { get; set; }
        
        public abstract void AddBalls(int numberOfBallsToDeploy);
        public abstract void RemoveBalls();
        public abstract int GetAmountOfBalls();
        public abstract int GetBallsRadius(int indexOfABall);
        public abstract Vector2 GetPositionOfABall(int indexOfABall);
        public abstract event EventHandler<(int number, float x, float y, int radius)>? LogicLayerEvent;

        internal class Table : LogicAbstractAPI
        {
            private DataAbstract _dataLayer;
            
            public override int Width { get; set; }
            public override int Height { get; set; }
            public override event EventHandler<(int number, float x, float y, int radius)>? LogicLayerEvent;
            private ConcurrentDictionary<(int, int), bool> _collisionFlags = new ConcurrentDictionary<(int, int), bool>();
            private readonly object _collisionLock = new();

            public Table(int height, int width, DataAbstract? data)
            {
                Height = height;
                Width = width;
                _dataLayer = data != null ? data : DataAbstract.init(height, width);

            }


            public override void AddBalls(int numberOfBallsToDeploy)
            {
                _dataLayer.createBalls(numberOfBallsToDeploy);
                for (int i = 0; i < numberOfBallsToDeploy; i++)
                {
                    _dataLayer.getBall(i)._changed += PositionChanged;
                }
            }

            public override void RemoveBalls()
            {
                for (int i = 0; i < _dataLayer.getAmountOfBalls(); i++)
                {
                    _dataLayer.getBall(i)._changed -= PositionChanged;
                }
                _dataLayer.removeBalls();
            }

            public override int GetAmountOfBalls()
            {
                return _dataLayer.getAmountOfBalls();
            }

            public override int GetBallsRadius(int indexOfABall)
            {
                return _dataLayer.getBall(indexOfABall).radius;
            }

            public override Vector2 GetPositionOfABall(int indexOfABall)
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
                wallCollision(ball);
                LogicLayerEvent?.Invoke(this, (ball.number, ball.position.X, ball.position.Y, ball.radius));
            }

            private void detectCollision(IBall observedBall)
            {
                for (int i = 0; i < _dataLayer.getAmountOfBalls(); i++)
                {
                    IBall ball = _dataLayer.getBall(i);

                    if (observedBall == ball)
                    {
                        continue;
                    }

                    if(!checkedCollision(observedBall, ball) && collides(observedBall, ball))
                    {
                        MarkAsChecked(observedBall, ball);

                        Vector2 newObservedBallSpeed = newSpeed(observedBall, ball);
                        Vector2 newBallSpeed = newSpeed(observedBall, ball);
                        if (Vector2.Distance(observedBall.position, ball.position) >
                            Vector2.Distance(observedBall.position + newObservedBallSpeed,
                                ball.position + newBallSpeed))
                        {
                            return;
                        }

                        observedBall.speed = newObservedBallSpeed;
                        ball.speed = newBallSpeed;
                    }
                    else
                    {
                        RemoveCheckedMark(observedBall, ball);
                    }
                }
            }

            private Vector2 newSpeed(IBall observedBall, IBall ball)
            {
                var ObservedBallCurrentSpeed = observedBall.speed;
                var BallCurrentSpeed = ball.speed;
                //var distance = observedBall.position - ball.position
                var distance = observedBall.position - ball.position;
                return observedBall.speed - 2.0f * ball.weight / (observedBall.weight + ball.weight) *
                    (Vector2.Dot(ObservedBallCurrentSpeed - BallCurrentSpeed, distance) * distance) /
                    (float)Math.Pow(distance.Length(), 2);
            }

            private void MarkAsChecked(IBall observedBall, IBall ball)
            {
                int NumberOfFirstBall = observedBall.number;
                int NumberOfSecondBall = ball.number;
                _collisionFlags.TryAdd((NumberOfFirstBall, NumberOfSecondBall), true);
            }

            private void RemoveCheckedMark(IBall observedBall, IBall ball)
            {
                int NumberOfFirstBall = observedBall.number;
                int NumberOfSecondBall = ball.number;
                _collisionFlags.Remove((NumberOfFirstBall, NumberOfSecondBall), out _);
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
                else if (observedBall.position.X + observedBall.radius >= Width)
                {
                    newSpeed.X = -Math.Abs(observedBall.speed.X);
                }

                if (observedBall.position.Y - observedBall.radius <= 0)
                {
                    newSpeed.Y = Math.Abs(observedBall.speed.Y);
                }
                else if (observedBall.position.Y + observedBall.radius >= Height)
                {
                    newSpeed.Y = -Math.Abs(observedBall.speed.Y);
                }
            }
        }
    }
}
