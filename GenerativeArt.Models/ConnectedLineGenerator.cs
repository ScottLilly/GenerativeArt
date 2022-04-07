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

    private int _maxCanvasWidth;
    private int _maxCanvasHeight;
    private Direction _lastDirection = Direction.None;
    private int _lastEndingX = -1;
    private int _lastEndingY = -1;

    public ConnectedLineGenerator(int maxCanvasWidth, int maxCanvasHeight)
    {
        _maxCanvasWidth = maxCanvasWidth;
        _maxCanvasHeight = maxCanvasHeight;
    }

    public void SetCanvasSize(int width, int height)
    {
        _maxCanvasWidth = width;
        _maxCanvasHeight = height;
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
            if (_lastEndingX == -1 ||
                _lastEndingX != -1 && _lastEndingX != MARGIN)
            {
                validDirections.Add(Direction.Left);
            }

            if (_lastEndingX == -1 ||
                _lastEndingX != -1 && _lastEndingX != _maxCanvasWidth - MARGIN)
            {
                validDirections.Add(Direction.Right);
            }
        }

        if (_lastDirection is Direction.Left or Direction.Right or Direction.None)
        {
            if (_lastEndingY == -1 ||
                _lastEndingY != -1 && _lastEndingY != MARGIN)
            {
                validDirections.Add(Direction.Up);
            }
            if (_lastEndingY == -1 ||
                _lastEndingY != -1 && _lastEndingY != _maxCanvasHeight - MARGIN)
            {
                validDirections.Add(Direction.Down);
            }
        }

        return validDirections[Randomizer.GetRandomNumberBetween(0, validDirections.Count - 1)];
    }
}