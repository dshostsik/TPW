using System.Collections.ObjectModel;
using System.Numerics;

namespace Data
{

    public abstract class DataAbstract
    {
        public static DataAbstract init(int startWidth, int startHeight) { 
            return new DataAPI(startWidth, startHeight); 
        }

        public abstract int getAmountOfBalls();
        public abstract IBall getBall(int numberOfABall);
        public abstract void createBalls(int amountOfBalls);
        public abstract void removeBalls();

        public abstract int width { get; }
        public abstract int height { get; }


        internal class DataAPI : DataAbstract
        {
            private FileDAO dao;
            
            private readonly Random random = new Random();
            public DataAPI(int startWidth, int startHeight) {
                width = startWidth;
                height = startHeight;
                balls = new List<IBall>();
            }

            private List<IBall> balls;
            public override int width { get; }
            public override int height { get; }

            public override int getAmountOfBalls()
            {
                return balls.Count;
            }

            public override IBall getBall(int numberOfABall)
            {
                return balls[numberOfABall];
            }

            public override void createBalls(int amountOfBalls)
            {
                dao = new FileDAO(width, height);
                for (int i = 0; i < amountOfBalls; i++)
                {
                    float scale = 1;
                    float speedX = (float)((random.NextDouble() - 0.5) / scale);
                    float speedY = (float)((random.NextDouble() - 0.5) / scale);

                    while (speedX == 0 & speedY == 0)
                    {
                        speedX = (float)((random.NextDouble() - 0.5) * scale);
                        speedY = (float)((random.NextDouble() - 0.5) * scale);
                    }

                    Vector2 speeds = new Vector2(speedX, speedY);
                    int radius = random.Next(10, 20);
                    int weight = 4 * radius;
                    float x = (float)(random.Next(20 + 2 * radius, width - 2 * radius - 20) + random.NextDouble());
                    float y = (float)(random.Next(20 + 2 * radius, height - 2 * radius - 20) + random.NextDouble());

                    Ball ball = new Ball(i, radius, weight, x, y, speeds);
                    ball._changed += (object? sender,EventArgs args) => dao.FillQueue((IBall)sender); 
                    balls.Add(ball);
                }
            }

            public override void removeBalls()
            {
                foreach (IBall b in balls) {
                    b.Dispose();
                }
                balls.Clear();
            }
        }
    }

}
