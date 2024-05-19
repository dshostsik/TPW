using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace Logic
{
    public interface IBall : IDisposable
    {
        public static IBall createInstance(int newNumber, int radius, int weight, float x, float y, Vector2 speed) {
            return new Ball(newNumber, radius, weight, x, y, speed); 
        }

        int number { get; }
        float posX { get; }
        float posY { get; }
        int radius { get; }
        int weight { get; }
        Vector2 position { get; }
        Vector2 _speed { get; set; }

        event EventHandler? _changed;

    }
}
