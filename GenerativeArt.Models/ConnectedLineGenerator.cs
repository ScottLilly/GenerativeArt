using GenerativeArt.Core;

namespace GenerativeArt.Models;

public class ConnectedLineGenerator : IConnectedLineGenerator
{
    private enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right
    }

    private readonly int _maxCanvasWidth;
    private readonly int _maxCanvasHeight;
    private int _margin = 10;

    private int _lastEndingX = -1;
    private int _lastEndingY = -1;
    private Direction _lastDirection = Direction.None;

    public ConnectedLineGenerator(int maxCanvasWidth, int maxCanvasHeight)
    {
        _maxCanvasWidth = maxCanvasWidth;
        _maxCanvasHeight = maxCanvasHeight;
    }

    public LineShape GetNextLine()
    {
        // Get starting random X and Y coordinates
        int startingX =
            _lastEndingX == -1
                ? Randomizer.GetRandomNumberBetween(_margin, _maxCanvasWidth - _margin)
                : _lastEndingX;
        int startingY =
            _lastEndingY == -1
                ? Randomizer.GetRandomNumberBetween(_margin, _maxCanvasHeight - _margin)
                : _lastEndingY;

        // Pick direction
        var direction = GetRandomDirection();
        _lastDirection = direction;

        // Get random length (not going past margin)
        int length = Randomizer.GetRandomNumberBetween(25, 250);

        int endingX;
        int endingY;

        switch (direction)
        {
            case Direction.Up:
                endingX = startingX;
                endingY = startingY - length;
                break;
            case Direction.Right:
                endingX = startingX + length;
                endingY = startingY;
                break;
            case Direction.Down:
                endingX = startingX;
                endingY = startingY + length;
                break;
            case Direction.Left:
                endingX = startingX - length;
                endingY = startingY;
                break;
            default:
                throw new ArgumentException("direction");
        }

        endingX = Math.Min(Math.Max(_margin, endingX), _maxCanvasWidth - _margin);
        endingY = Math.Min(Math.Max(_margin, endingY), _maxCanvasHeight - _margin);

        _lastEndingX = endingX;
        _lastEndingY = endingY;

        // Return LineShape
        return new LineShape
        {
            X1 = startingX, Y1 = startingY,
            X2 = endingX, Y2 = endingY
        };
    }

    private Direction GetRandomDirection()
    {
        var rand = Randomizer.GetRandomNumberBetween(1, 4);

        switch (rand)
        {
            case 1:
                return Direction.Up;
            case 2:
                return Direction.Right;
            case 3:
                return Direction.Down;
            case 4:
                return Direction.Left;
            default:
                throw new ArgumentOutOfRangeException("rand");
        }
    }

}