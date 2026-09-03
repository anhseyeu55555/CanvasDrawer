namespace CanvasDrawer.Commands.DrawingOperations;

public interface IDrawingOperation
{
    void Execute(Domain.Canvas canvas);
}
