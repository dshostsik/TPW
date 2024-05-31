using System.Diagnostics;
using System.Numerics;
using Data;
using Logic;
using System.Runtime.CompilerServices;

namespace TestLogic
{
    [TestClass]
    public class UnitTest1
    {
        internal class ballForTests : IBall
        { private Stopwatch _stopwatch;
            public int number { get; }
            public int radius { get; }
            public int weight { get; }

            public Vector2 _position;
            

            public Vector2 position
            {
                get { return _position;}
                private set
                {
                    _position = value;
                }
            }

            public Vector2 speed { get; set; }
            public Vector2 _speed { get; set; }
            private bool _move = true;
            
            public ballForTests(int newNumber, int radius, int weight, float x, float y, Vector2 speed)
            {
                _stopwatch = new Stopwatch();
                position = new Vector2(x, y);
                _speed = speed;
                number = newNumber;
                this.radius = radius;
                this.weight = weight;
            }
            public async void Move()
            {
                int delay = 10;

                while(_move)
                {
                    _stopwatch.Restart();
                    _stopwatch.Start();
                    UpdatePosition(delay);
                    _stopwatch.Stop();
                    await Task.Delay(delay - (int)_stopwatch.ElapsedMilliseconds < 0 ? 0 : delay - (int)_stopwatch.ElapsedMilliseconds);
                }
            }
            
            public void UpdatePosition(long time)
            {
                position += _speed * time;
                _changed?.Invoke(this, EventArgs.Empty);
            }
            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public event EventHandler? _changed;
        }
        private DataAbstract data;
        private LogicAbstractAPI table;

        [TestMethod]
        public void TestTableNBall()
        {
           LogicAbstractAPI logic = LogicAbstractAPI.initialize(500, 500);
           Vector2 initialSpeed = new Vector2(1.5f, 2.0f);
           ballForTests ball = new ballForTests(1, 10, 20, 1.0f, 1.0f, initialSpeed);
           Vector2 firstPosition = new Vector2(ball.position.X, ball.position.Y); 
           ball.Move();

           double TOLERANCE = 0.00000001f;
           Assert.IsFalse(Math.Abs(firstPosition.X - ball.position.X) < TOLERANCE);
           Assert.IsFalse(Math.Abs(firstPosition.Y - ball.position.Y) < TOLERANCE);
        }
    }
}