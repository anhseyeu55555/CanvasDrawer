namespace CanvasDrawer.Domain;

public readonly record struct Point
{
	public int X { get; init; }
    public int Y { get; init; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool IsWithinBounds(int width, int height) {
        return X >= 0 && X <= width && Y >= 0 && Y <= height;
    }
        

    public IEnumerable<Point> GetNeighbors()
    {
        yield return new Point(X - 1, Y); //left
        yield return new Point(X + 1, Y); //right
        yield return new Point(X, Y - 1);  // up
        yield return new Point(X, Y + 1);  // down
    }
}
