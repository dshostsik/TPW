using System.Diagnostics;
using System.Numerics;

namespace Data
{

    internal class Ball : IBall
    {
        private Stopwatch _stopwatch;
        private Task _task;
        private Vector2 _position;
        private Vector2 _speed;
        private bool _move = true;
        private int _radius;
        private int _weight;

        public event EventHandler? _changed;

        public int number { get; }
        
        public int radius { get => _radius; }

        public int weight
        {
            get => _weight;
            private set
            {
                _weight = value;
            }
        }

        public Vector2 position {  
            get => _position;
            private set
            {
                _position = value;
            }
        }

        public Vector2 speed { get => _speed;
            set
            {
                _speed = value;
            }
        }


        public Ball(int newNumber, int radius, int weight, float x, float y, Vector2 speed) {
            _stopwatch = new Stopwatch();
            _radius = radius;
            _position = new Vector2(x, y);
            _speed = speed;
            _weight = weight;
            number = newNumber;
            _task = Task.Run(Move);
        }

        
        public async void Move()
        {
            float time;

            while(_move)
            {
                _stopwatch.Restart();
                _stopwatch.Start();
                time = (2 / _speed.Length());
                UpdatePosition(time);
                _stopwatch.Stop();
                await Task.Delay(time - (int)_stopwatch.ElapsedMilliseconds < 0 ? 0 : (int)time - (int)_stopwatch.ElapsedMilliseconds);
            }
        }

        public void UpdatePosition(float time)
        {
            position += _speed * time;
            _changed?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            _move = false;
            _task.Wait();
            _task.Dispose();
        }

    }
}