using GenerativeArt.Core;

namespace GenerativeArt.Models;

public class ConnectedLineGenerator : IConnectedLineGenerator
{
    private const int MARGIN = 25;

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
    private Direction _lastDirection = Direction.None;
    private int _lastEndingX = -1;
    private int _lastEndingY = -1;

    public ConnectedLineGenerator(int maxCanvasWidth, int maxCanvasHeight)
    {
        _maxCanvasWidth = maxCanvasWidth;
        _maxCanvasHeight = maxCanvasHeight;
    }

    public LineShape GetNextLine()
    {
        int startingX =
            _lastEndingX == -1
                ? Randomizer.GetRandomNumberBetween(MARGIN, _maxCanvasWidth - MARGIN)
                : _lastEndingX;
        int startingY =
            _lastEndingY == -1
                ? Randomizer.GetRandomNumberBetween(MARGIN, _maxCanvasHeight - MARGIN)
                : _lastEndingY;

        var direction = GetRandomDirection();
        _lastDirection = direction;

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

        endingX = Math.Min(Math.Max(MARGIN, endingX), _maxCanvasWidth - MARGIN);
        endingY = Math.Min(Math.Max(MARGIN, endingY), _maxCanvasHeight - MARGIN);

        _lastEndingX = endingX;
        _lastEndingY = endingY;

        return new LineShape
        {
            X1 = startingX, Y1 = startingY,
            X2 = endingX, Y2 = endingY
        };
    }

    private Direction GetRandomDirection()
    {
        List<Direction> validDirections =
            new List<Direction>();

        if (_lastDirection is Direction.Up or Direction.Down or Direction.None)
        {
            validDirections.Add(Direction.Left);
            validDirections.Add(Direction.Right);
        }

        if (_lastDirection is Direction.Left or Direction.Right or Direction.None)
        {
            validDirections.Add(Direction.Up);
            validDirections.Add(Direction.Down);
        }

        return validDirections[Randomizer.GetRandomNumberBetween(0, validDirections.Count - 1)];
    }
}