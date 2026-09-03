using CanvasDrawer.Commands;
using CanvasDrawer.Domain;

namespace CanvasDrawer.Application;


public sealed class DrawingApp
{
    private readonly CommandParser _parser;
    private Canvas? _canvas;
    private bool _isRunning;

    public DrawingApp()
    {
        _parser = new CommandParser();
        _isRunning = false;
    }

    public void Run()
    {
        _isRunning = true;

        Console.WriteLine("Canvas Drawer - Type 'Q' to quit");
        Console.WriteLine("Commands: C w h (create), L x1 y1 x2 y2 (line), R x1 y1 x2 y2 (rect), B x y c (fill)");
        Console.WriteLine();

        while (_isRunning)
        {
            Console.Write("enter command: ");
            var input = Console.ReadLine();

            ProcessInput(input);
        }

        Console.WriteLine("Goodbye!");
    }

    private void ProcessInput(string? input)
    {
        try
        {
            var command = _parser.Parse(input);

            if (command == null)
            {
                Console.WriteLine("Invalid command. Please try again.");
                return;
            }

            ExecuteCommand(command);
        }
        catch (CommandParseException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private void ExecuteCommand(CanvasCommand command)
    {
        switch (command)
        {
            case CreateCanvasCommand create:
            {
                _canvas = new Canvas(create.Width, create.Height);
                Console.WriteLine(_canvas);
                break;
            }

            case LineCommand line:
            {
                EnsureCanvasExists();
                line.Execute(_canvas!);
                Console.WriteLine(_canvas);
                break;
            }

            case RectCommand rect:
            {
                EnsureCanvasExists();
                rect.Execute(_canvas!);
                Console.WriteLine(_canvas);
                break;
            }

            case BucketFillCommand fill:
            {
                EnsureCanvasExists();
                fill.Execute(_canvas!);
                Console.WriteLine(_canvas);
                break;
            }

            case QuitCommand:
            {
                _isRunning = false;
                break;
            }

        }
    }

    private void EnsureCanvasExists()
    {
        if (_canvas == null)
        {
            throw new InvalidOperationException("No canvas exists. Use 'C w h' to create one first.");
        }
    }
}