using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;

namespace Logic
{

    internal class Ball : IBall
    {
        private Stopwatch _stopwatch;
        private Task _task;
        private Vector2 _position;
        private bool _move = true;
        private int _radius;
        private int _weight;

        public event EventHandler? _changed;

        public int number { get; }

        public float posX => _position.X;

        public float posY => _position.Y;

        public int radius { get => _radius; }

        public int weight { get; }

        public Vector2 position {  
            get => _position;
            private set
            {
                _position = value;
            }
        }

        public Vector2 _speed { get => _speed;
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
            _task = Task.Run(move);
        }

        
        public async void move()
        {
            int delay = 10;

            while(_move)
            {
                _stopwatch.Restart();
                _stopwatch.Start();
                updatePosition(delay);
                _stopwatch.Stop();
                await Task.Delay(delay - (int)_stopwatch.ElapsedMilliseconds < 0 ? 0 : delay - (int)_stopwatch.ElapsedMilliseconds);
            }
        }

        public void updatePosition(long time)
        {
            position += _speed * time;
            _changed?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            _move = false;
            _task.Dispose();
        }
    }
}