using GenerativeArt.Core;

namespace GenerativeArt.Models;

public class SteppedRectangleGenerator : IRectangleGenerator
{
    private enum LeftRightDirection
    {
        Left,
        Right
    }

    private enum TopBottomDirection
    {
        Top,
        Bottom
    }

    private readonly int _canvasMaxHeight;
    private readonly int _canvasMaxWidth;

    private LeftRightDirection _leftRightDirection = LeftRightDirection.Right;
    private TopBottomDirection _topBottomDirection = TopBottomDirection.Bottom;

    private RectangleShape _latestRectangle;

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
        if (_topBottomDirection == TopBottomDirection.Bottom &&
            _latestRectangle.Top + (_latestRectangle.Height / 2) + rectangleHeight > _canvasMaxHeight)
        {
            _topBottomDirection = TopBottomDirection.Top;
        }

        // Current rectangle goes above canvas top
        if (_topBottomDirection == TopBottomDirection.Top &&
            _latestRectangle.Top + (_latestRectangle.Height / 2) - rectangleHeight < 1)
        {
            _topBottomDirection = TopBottomDirection.Bottom;
        }

        return _topBottomDirection == TopBottomDirection.Bottom
            ? _latestRectangle.Top + (_latestRectangle.Height / 2)
            : _latestRectangle.Top + (_latestRectangle.Height / 2) - rectangleHeight;
    }

    private int CalculateRectangleLeft(int rectangleWidth)
    {
        if (_latestRectangle == null)
        {
            return 1;
        }

        // Current rectangle goes past canvas right
        if (_leftRightDirection == LeftRightDirection.Right &&
            _latestRectangle.Left + (_latestRectangle.Width / 2) + rectangleWidth > _canvasMaxWidth)
        {
            _leftRightDirection = LeftRightDirection.Left;
        }

        // Current rectangle goes past canvas left
        if (_leftRightDirection == LeftRightDirection.Left &&
            _latestRectangle.Left + (_latestRectangle.Width / 2) - rectangleWidth < 1)
        {
            _leftRightDirection = LeftRightDirection.Right;
        }

        return _leftRightDirection == LeftRightDirection.Right
            ? _latestRectangle.Left + (_latestRectangle.Width / 2)
            : _latestRectangle.Left + (_latestRectangle.Width / 2) - rectangleWidth;
    }
}