using GenerativeArt.Core;
using GenerativeArt.Core.Enums;

namespace GenerativeArt.Models;

public class SteppedRectangleGenerator : IRectangleGenerator
{
    private int _maxCanvasHeight;
    private int _maxCanvasWidth;

    private LeftRightDirection _leftRightDirection = LeftRightDirection.MoveToRight;
    private TopBottomDirection _topBottomDirection = TopBottomDirection.MoveToBottom;

    private RectangleShape _latestRectangle;

    private int LatestRectangleMidpointX() =>
        _latestRectangle.Left + (_latestRectangle.Width / 2);
    private int LatestRectangleMidpointY() =>
        _latestRectangle.Top + (_latestRectangle.Height / 2);

    public SteppedRectangleGenerator(int maxCanvasHeight, int maxCanvasWidth)
    {
        _maxCanvasHeight = maxCanvasHeight;
        _maxCanvasWidth = maxCanvasWidth;
    }

    public void SetCanvasSize(int width, int height)
    {
        _maxCanvasWidth = width;
        _maxCanvasHeight = height;
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
            return Randomizer.GetRandomNumberBetween(1, _maxCanvasHeight / 20);
        }

        // Current rectangle goes below canvas bottom
        if (_topBottomDirection == TopBottomDirection.MoveToBottom &&
            LatestRectangleMidpointY() + rectangleHeight > _maxCanvasHeight)
        {
            _topBottomDirection = TopBottomDirection.MoveToTop;
        }

        // Current rectangle goes above canvas top
        if (_topBottomDirection == TopBottomDirection.MoveToTop &&
            LatestRectangleMidpointY() - rectangleHeight < 1)
        {
            _topBottomDirection = TopBottomDirection.MoveToBottom;
        }

        return _topBottomDirection == TopBottomDirection.MoveToBottom
            ? LatestRectangleMidpointY()
            : LatestRectangleMidpointY() - rectangleHeight;
    }

    private int CalculateRectangleLeft(int rectangleWidth)
    {
        if (_latestRectangle == null)
        {
            return Randomizer.GetRandomNumberBetween(1, _maxCanvasWidth / 20);
        }

        // Current rectangle goes past canvas right
        if (_leftRightDirection == LeftRightDirection.MoveToRight &&
            LatestRectangleMidpointX() + rectangleWidth > _maxCanvasWidth)
        {
            _leftRightDirection = LeftRightDirection.MoveToLeft;
        }

        // Current rectangle goes past canvas left
        if (_leftRightDirection == LeftRightDirection.MoveToLeft &&
            LatestRectangleMidpointX() - rectangleWidth < 1)
        {
            _leftRightDirection = LeftRightDirection.MoveToRight;
        }

        return _leftRightDirection == LeftRightDirection.MoveToRight
            ? LatestRectangleMidpointX()
            : LatestRectangleMidpointX() - rectangleWidth;
    }
}