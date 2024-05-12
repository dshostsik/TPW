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
        public abstract void moveBall();
        public abstract List<IBall> GetBalls();
    }

    internal class Table : LogicAbstractAPI
    {
        private DataAbstract _dataLayer;
        private bool workStop;
        public int _height { 
            get; 
            set; 
        }
        public int _width { 
            get; 
            set; 
        }
        public List<IBall> yourBalls { get; set; }

        public Table(int height, int width, DataAbstract data)
        {
            this._height = height;
            this._width = width;
            yourBalls = new List<IBall>();
            _dataLayer = data;

        }


        public override void addBalls(int numberOfBallsToDeploy, int radiusOfBalls)
        {
            Random rand = new Random();
            for (int i = 0; i < numberOfBallsToDeploy; i++)
            {
                int x = rand.Next(0 + radiusOfBalls, _width - radiusOfBalls);
                int y = rand.Next(0 + radiusOfBalls, _height - radiusOfBalls);
                IBall ball = IBall.createInstance(radiusOfBalls, x, y);
                yourBalls.Add(ball);
            }
        }
        public override void moveBall()
        {
            foreach (IBall ball in yourBalls)
            {
                ball.posX += ball.getXSpeed();
                ball.posY += ball.getYSpeed();
                int newspeedX = ball.getXSpeed();
                int newspeedY = ball.getYSpeed();
                if (ball.posX - ball.radius < 0 || ball.posX + ball.radius > _width)
                {
                    newspeedX *= -1;
                }
                if (ball.posY - ball.radius < 0 || ball.posY + ball.radius > _height)
                {
                    newspeedY *= -1;
                }
                ball.setSpeed(newspeedX, newspeedY);
            }
        }

        public override List<IBall> GetBalls()
        {
            return yourBalls;
        }

        public override void removeBalls()
        {
            yourBalls.Clear();
        }
    }
}
