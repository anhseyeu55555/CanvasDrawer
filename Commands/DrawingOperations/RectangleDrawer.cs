using CanvasDrawer.Domain;

namespace CanvasDrawer.Commands.DrawingOperations;

public sealed class RectangleDrawer : IDrawingOperation
{
    private readonly Point _topLeft;
    private readonly Point _bottomRight;
    private readonly char _character;

    public RectangleDrawer(Point topLeft, Point bottomRight, char character = CanvasConstants.DefaultDrawingCharacter)
    {
        _topLeft = topLeft;
        _bottomRight = bottomRight;
        _character = character;
    }

    public void Execute(Canvas canvas)
    {
        // Top edge
        var topLine = new LineDrawer(_topLeft, new Point(_bottomRight.X, _topLeft.Y), _character);
        topLine.Execute(canvas);

        // Bottom edge
        var bottomLine = new LineDrawer(new Point(_topLeft.X, _bottomRight.Y), _bottomRight, _character);
        bottomLine.Execute(canvas);

        // Left edge
        var leftLine = new LineDrawer(_topLeft, new Point(_topLeft.X, _bottomRight.Y), _character);
        leftLine.Execute(canvas);

        // Right edge
        var rightLine = new LineDrawer(new Point(_bottomRight.X, _topLeft.Y), _bottomRight, _character);
        rightLine.Execute(canvas);
    }
}
