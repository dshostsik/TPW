using System.Collections.Concurrent;
using System.Text.Json;

namespace Data
{
    internal class FileDAO : IDisposable
    {
        private Task Logger;
        private StreamWriter Writer;
        private BlockingCollection<BallData> Queue;
        private string Path = "./log.txt";
        private int Width;
        private int Height;
        private readonly object _writeLock = new();
        private readonly object _fillQueueLock = new();

        public FileDAO(int width, int height)
        {
            Width = width;
            Height = height;
        }


        public void FillQueue(IBall iball)
        {
            lock (_fillQueueLock)
            {
                if (iball == null)
                {
                    return;
                }

                BallData servedBall = new BallData(iball.position.X, iball.position.Y, iball.radius, iball.weight,iball.speed.X,iball.speed.Y, iball.number);
                if (!Queue.IsAddingCompleted && !Queue.IsCompleted)
                {
                    Queue.Add(servedBall);
                }
            }
        }

        private void write()
        {
            lock (_writeLock)
            {
                using (Writer = new StreamWriter(Path, append: false))
                {
                    JsonSerializerOptions jso = new JsonSerializerOptions();
                    jso.WriteIndented = true;
                    Writer.Write("\n");
                    Writer.Write("{" + string.Format("\n\t\"Width\": {0},\n\t\"Height\": {1}\n", Width, Height) + "}");
                    foreach (BallData ball in Queue.GetConsumingEnumerable())
                    {
                        string log = JsonSerializer.Serialize(ball, jso);

                        Writer.Write("," + "\n" + log);
                    }

                    Writer.Write("\n}");
                    Writer.Flush();
                }
            }
        }
        //comment for commit
        public void Dispose()
        {
            Queue.CompleteAdding();
            Logger.Wait();
            Logger.Dispose();
        }
        
        internal class BallData
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float SpeedX { get; set; }
            public float SpeedY { get; set; }
            public int Weight { get; set; }
            public int Radius { get; set; }
            public string Time { get; set; }
            public int Number { get; set; }

            public BallData(float x, float y, int radius, int weight, float speedX, float speedY,  int number)
            {
                X = x;
                Y = y;
                SpeedX = speedX;
                SpeedY = speedY;
                Weight = weight;
                Radius = radius;
                Number = number;
                Time = DateTime.UtcNow.ToString("G");
            }
        }
    }    
}

