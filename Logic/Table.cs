using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal class Table: LogicAbstractAPI
    {
        private DataAbstract _dataLayer;
        private bool workStop;
        public int _height { get; set; }
        public int _width { get; set; }
        public List<IBall> yourBalls { get; set; }
        public List<Task> tasks { get; set; }

        public Table(int height, int width, DataAbstract data)
        {
            this._height = height;
            this._width = width;
            yourBalls = new List<IBall>();
            tasks = new List<Task>();
            _dataLayer = data;

        }


        public override void addBalls(int numberOfBallsToDeploy, int radiusOfBalls)
        {
            Random rand = new Random();
            for (int i = 0; i < numberOfBallsToDeploy; i++) {
                int x = rand.Next(0+radiusOfBalls, _width - radiusOfBalls);
                int y = rand.Next(0 + radiusOfBalls, _height - radiusOfBalls);
                IBall ball = IBall.createInstance(radiusOfBalls, x, y);
                yourBalls.Add(ball);
                tasks.Add(new Task(() =>
                {
                    while (!workStop) {
                        ball.setSpeed(5,5);
                    if (ball.collides(_width, _height)) {
                            ball.move(ball.getXSpeed(), ball.getYSpeed());
                            Thread.Sleep(100);
                        }
                    }
                }));
            }
        }

        public override List<List<int>> getPositions()
        {
            List<List<int>> positionsOfBalls = new List<List<int>>();
            foreach (IBall sampleBall in yourBalls)
            {
                List<int> positionOfAConcreteBall = new List<int>();
                positionOfAConcreteBall.Add(sampleBall.posX);
                positionOfAConcreteBall.Add(sampleBall.posY);
                positionsOfBalls.Add(positionOfAConcreteBall);
            }
            return positionsOfBalls;
        }

        public override List<IBall> GetBalls()
        {
            return yourBalls;
        }

        public override void removeBalls()
        {
            throw new NotImplementedException();
        }

        public override void begin()
        {
            workStop = false;
        }
    }
}
