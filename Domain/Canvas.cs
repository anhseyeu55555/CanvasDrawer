namespace CanvasDrawer.Domain;


public class Canvas
{
    private readonly char[,] _pixels;

    public int Width { get; }
    public int Height { get; }

    public Canvas(int width, int height)
    {
        if (width <= 0 || height <= 0)
        {
            throw new ArgumentException("Canvas dimensions must be positive.");
        }

        Width = width;
        Height = height;
        _pixels = new char[height, width];

        Clear();
    }

    public void Clear()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                _pixels[y, x] = CanvasConstants.EmptyCell;
            }
        }
    }

    public char GetPixel(int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            return '\0';
        }
        return _pixels[y, x];
    }

    public void SetPixel(int x, int y, char character)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            _pixels[y, x] = character;
        }
    }

    public override string ToString()
    {
        var sb = new System.Text.StringBuilder();

        sb.AppendLine(new string('-', Width + 2));

        for (int y = 0; y < Height; y++)
        {
            sb.Append('|');
            for (int x = 0; x < Width; x++)
            {
                sb.Append(_pixels[y, x]);
            }
            sb.AppendLine("|");
        }

        sb.Append(new string('-', Width + 2));

        return sb.ToString();
    }
}
