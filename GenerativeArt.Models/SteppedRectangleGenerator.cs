using GenerativeArt.Core;

namespace GenerativeArt.Models;

public class SteppedRectangleGenerator : IRectangleGenerator
{
    private readonly int _canvasMaxHeight;
    private readonly int _canvasMaxWidth;

    private Enums.LeftRightDirection _leftRightDirection = Enums.LeftRightDirection.MoveToRight;
    private Enums.TopBottomDirection _topBottomDirection = Enums.TopBottomDirection.MoveToBottom;

    private RectangleShape _latestRectangle;

    private int LatestRectangleMidpointX() =>
        _latestRectangle.Left + (_latestRectangle.Width / 2);
    private int LatestRectangleMidpointY() =>
        _latestRectangle.Top + (_latestRectangle.Height / 2);

    public SteppedRectangleGenerator(int canvasMaxHeight, int canvasMaxWidth)
    {
        _canvasMaxHeight = canvasMaxHeight;
        _canvasMaxWidth = canvasMaxWidth;
    }

    public RectangleShape GetRectangle()
    {
        int rectHeight = Randomizer.GetRandomNumberBetween(10, 50);
        var rectWidth = Randomizer.GetRandomNumberBetween(25, 200);

        var top = CalculateRectangleTop(rectHeight);
        var left = CalculateRectangleLeft(rectWidth);

        var rectangle = new RectangleShape
        {
            FillColor = Randomizer.GetRandomColor(),
            Height = rectHeight,
            Width = rectWidth,
            RotationAngle = 0,
            Top = top,
            Left = left
        };

        _latestRectangle = rectangle;

        return rectangle;
    }

    private int CalculateRectangleTop(int rectangleHeight)
    {
        if (_latestRectangle == null)
        {
            return 1;
        }

        // Current rectangle goes below canvas bottom
        if (_topBottomDirection == Enums.TopBottomDirection.MoveToBottom &&
            LatestRectangleMidpointY() + rectangleHeight > _canvasMaxHeight)
        {
            _topBottomDirection = Enums.TopBottomDirection.MoveToTop;
        }

        // Current rectangle goes above canvas top
        if (_topBottomDirection == Enums.TopBottomDirection.MoveToTop &&
            LatestRectangleMidpointY() - rectangleHeight < 1)
        {
            _topBottomDirection = Enums.TopBottomDirection.MoveToBottom;
        }

        return _topBottomDirection == Enums.TopBottomDirection.MoveToBottom
            ? LatestRectangleMidpointY()
            : LatestRectangleMidpointY() - rectangleHeight;
    }

    private int CalculateRectangleLeft(int rectangleWidth)
    {
        if (_latestRectangle == null)
        {
            return 1;
        }

        // Current rectangle goes past canvas right
        if (_leftRightDirection == Enums.LeftRightDirection.MoveToRight &&
            LatestRectangleMidpointX() + rectangleWidth > _canvasMaxWidth)
        {
            _leftRightDirection = Enums.LeftRightDirection.MoveToLeft;
        }

        // Current rectangle goes past canvas left
        if (_leftRightDirection == Enums.LeftRightDirection.MoveToLeft &&
            LatestRectangleMidpointX() - rectangleWidth < 1)
        {
            _leftRightDirection = Enums.LeftRightDirection.MoveToRight;
        }

        return _leftRightDirection == Enums.LeftRightDirection.MoveToRight
            ? LatestRectangleMidpointX()
            : LatestRectangleMidpointX() - rectangleWidth;
    }
}