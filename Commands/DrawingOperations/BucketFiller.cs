using CanvasDrawer.Domain;

namespace CanvasDrawer.Commands.DrawingOperations;


public sealed class BucketFiller : IDrawingOperation
{
    private readonly Point _seed;
    private readonly char _fillCharacter;

    public BucketFiller(Point seed, char fillCharacter)
    {
        _seed = seed;
        _fillCharacter = fillCharacter;
    }

    public void Execute(Canvas canvas)
    {
        if (!_seed.IsWithinBounds(canvas.Width, canvas.Height))
        {
            throw new ArgumentOutOfRangeException(nameof(_seed), "Seed point is outside canvas bounds.");
        }

        char targetColor = canvas.GetPixel(_seed.X, _seed.Y);
        if (targetColor == _fillCharacter)
        {
            return;
        }

        var queue = new Queue<Point>();
        var visited = new HashSet<Point>();
        queue.Enqueue(_seed);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (visited.Contains(current))
            {
                continue;
            }
            if (!current.IsWithinBounds(canvas.Width, canvas.Height))
            {
                continue;
            }
            if (canvas.GetPixel(current.X, current.Y) != targetColor)
            {
                continue;
            }

            visited.Add(current);
            canvas.SetPixel(current.X, current.Y, _fillCharacter);

            foreach (var neighbor in current.GetNeighbors())
            {
                if (!visited.Contains(neighbor))
                {
                    queue.Enqueue(neighbor);
                }
            }
        }
    }
}
