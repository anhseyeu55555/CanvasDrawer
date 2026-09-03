using CanvasDrawer.Domain;

namespace CanvasDrawer.Commands.DrawingOperations;

public sealed class LineDrawer : IDrawingOperation
{
    private readonly Point _start;
    private readonly Point _end;
    private readonly char _character;

    public LineDrawer(Point start, Point end, char character = CanvasConstants.DefaultDrawingCharacter)
    {
        _start = start;
        _end = end;
        _character = character;
    }

    public void Execute(Canvas canvas)
    {
        ValidateLine();

        if (_start.X == _end.X)
        {
            DrawVerticalLine(canvas);
        }
        else
        {
            DrawHorizontalLine(canvas);
        }
    }

    private void ValidateLine()
    {
        if (_start.X != _end.X && _start.Y != _end.Y)
        {
            throw new ArgumentException("Only horizontal or vertical lines are supported.");
        }
    }

    private void DrawVerticalLine(Canvas canvas)
    {
        int x = _start.X;
        int minY = Math.Min(_start.Y, _end.Y);
        int maxY = Math.Max(_start.Y, _end.Y);

        for (int y = minY; y <= maxY; y++)
        {
            canvas.SetPixel(x, y, _character);
        }
    }

    private void DrawHorizontalLine(Canvas canvas)
    {
        int y = _start.Y;
        int minX = Math.Min(_start.X, _end.X);
        int maxX = Math.Max(_start.X, _end.X);

        for (int x = minX; x <= maxX; x++)
        {
            canvas.SetPixel(x, y, _character);
        }
    }
}
