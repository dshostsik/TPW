using Logic;
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
            public DataAPI(int startWidth, int startHeight) {
                width = startWidth;
                height = startHeight;
                List<IBall> balls = new List<IBall>();
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
                Random random = new Random();
                for (int i = 0; i < amountOfBalls; i++) {
                    float speedX = (float)((random.NextDouble() - 0.5) / 2);
                    float speedY = (float)((random.NextDouble() - 0.5) / 2);

                    while (speedX == 0 & speedY == 0)
                    {
                        speedX = random.Next(-2, 2);
                        speedY = random.Next(-2, 2);
                    }

                    Vector2 speeds = new Vector2(speedX, speedY);
                    int radius = random.Next(10, 20);
                    int weight = 4 * radius;
                    float x = (float)(random.Next(20 + 2 * radius, width - 2 * radius - 20) + random.NextDouble());
                    float y = (float)(random.Next(20 + 2 * radius, width - 2 * radius - 20) + random.NextDouble());

                    Ball ball = new Ball(i, radius, weight, x, y, speeds);
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
