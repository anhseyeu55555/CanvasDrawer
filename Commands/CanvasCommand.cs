using CanvasDrawer.Commands.DrawingOperations;
using CanvasDrawer.Domain;

namespace CanvasDrawer.Commands;

public abstract class CanvasCommand
{
    public abstract void Execute(Canvas canvas);
}

public sealed class CreateCanvasCommand : CanvasCommand
{
    public int Width { get; }
    public int Height { get; }

    public CreateCanvasCommand(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public override void Execute(Canvas canvas)
    {
       
    }
}

public sealed class LineCommand : CanvasCommand
{
    private readonly IDrawingOperation _drawer;

    public LineCommand(Point start, Point end)
    {
        _drawer = new LineDrawer(start, end);
    }

    public override void Execute(Canvas canvas)
    {
        _drawer.Execute(canvas);
    }
}

public sealed class RectCommand : CanvasCommand
{
    private readonly IDrawingOperation _drawer;

    public RectCommand(Point topLeft, Point bottomRight)
    {
        _drawer = new RectangleDrawer(topLeft, bottomRight);
    }

    public override void Execute(Canvas canvas)
    {
        _drawer.Execute(canvas);
    }
}

public sealed class BucketFillCommand : CanvasCommand
{
    private readonly IDrawingOperation _filler;

    public BucketFillCommand(Point seed, char fillCharacter)
    {
        _filler = new BucketFiller(seed, fillCharacter);
    }

    public override void Execute(Canvas canvas)
    {
        _filler.Execute(canvas);
    }
}

public sealed class QuitCommand : CanvasCommand
{
    public override void Execute(Canvas canvas)
    {

    }
}
