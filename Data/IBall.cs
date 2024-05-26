using System.Numerics;


namespace Data
{
    public interface IBall : IDisposable
    {
        int number { get; }
        float posX { get; }
        float posY { get; }
        int radius { get; }
        int weight { get; }
        Vector2 position { get; }
        Vector2 speed { get; set; }

        event EventHandler? _changed;

    }
}
