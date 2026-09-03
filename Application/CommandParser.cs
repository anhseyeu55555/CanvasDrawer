using CanvasDrawer.Domain;

namespace CanvasDrawer.Commands;

public sealed class CommandParser
{
    public CanvasCommand? Parse(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return null;
        }

        var parts = input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0)
        {
            return null;
        }

        var commandLetter = parts[0].ToUpperInvariant();

        return commandLetter switch
        {
            CommandConstants.CreateCanvas => ParseCreateCanvas(parts),
            CommandConstants.DrawLine => ParseLine(parts),
            CommandConstants.DrawRectangle => ParseRectangle(parts),
            CommandConstants.BucketFill => ParseBucketFill(parts),
            CommandConstants.Quit => new QuitCommand(),
            _ => null
        };
    }

    private static CanvasCommand ParseCreateCanvas(string[] parts)
    {
        if (parts.Length != 3)
        {
            throw new CommandParseException("Create canvas requires 3 arguments: C w h");
        }

        if (!int.TryParse(parts[1], out var width) || !int.TryParse(parts[2], out var height))
        {
            throw new CommandParseException("Canvas width and height must be integers.");
        }

        if (width <= 0 || height <= 0)
        {
            throw new CommandParseException("Canvas width and height must be positive integers.");
        }

        return new CreateCanvasCommand(width, height);
    }

    private static CanvasCommand ParseLine(string[] parts)
    {
        if (parts.Length != 5)
        {
            throw new CommandParseException("Line command requires 5 arguments: L x1 y1 x2 y2");
        }

        var (x1, y1) = ParseCoordinates(parts[1], parts[2]);
        var (x2, y2) = ParseCoordinates(parts[3], parts[4]);

        return new LineCommand(new Point(x1, y1), new Point(x2, y2));
    }

    private static CanvasCommand ParseRectangle(string[] parts)
    {
        if (parts.Length != 5)
        {
            throw new CommandParseException("Rectangle command requires 5 arguments: R x1 y1 x2 y2");
        }

        var (x1, y1) = ParseCoordinates(parts[1], parts[2]);
        var (x2, y2) = ParseCoordinates(parts[3], parts[4]);

        return new RectCommand(new Point(x1, y1), new Point(x2, y2));
    }

    private static CanvasCommand ParseBucketFill(string[] parts)
    {
        if (parts.Length != 4)
        {
            throw new CommandParseException("Bucket fill command requires 4 arguments: B x y c");
        }

        var (x, y) = ParseCoordinates(parts[1], parts[2]);

        if (parts[3].Length != 1)
        {
            throw new CommandParseException("Fill character must be a single character.");
        }

        var fillChar = parts[3][0];

        return new BucketFillCommand(new Point(x, y), fillChar);
    }

    private static (int x, int y) ParseCoordinates(string xStr, string yStr)
    {
        if (!int.TryParse(xStr, out var x) || !int.TryParse(yStr, out var y))
        {
            throw new CommandParseException("Coordinates must be integers.");
        }

        if (x < 1 || y < 1)
        {
            throw new CommandParseException("Coordinates must be at least 1.");
        }

        // Convert from 1-based (user input) to 0-based (internal)
        return (x - 1, y - 1);
    }
}

public sealed class CommandParseException : Exception
{
    public CommandParseException(string message) : base(message) { }
}