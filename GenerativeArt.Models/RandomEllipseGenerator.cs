﻿using GenerativeArt.Core;

namespace GenerativeArt.Models;

public class RandomEllipseGenerator : IEllipseGenerator
{
    private int _maxCanvasHeight;
    private int _maxCanvasWidth;

    public RandomEllipseGenerator(int maxCanvasHeight, int maxCanvasWidth)
    {
        _maxCanvasHeight = maxCanvasHeight;
        _maxCanvasWidth = maxCanvasWidth;
    }

    public void SetCanvasSize(int width, int height)
    {
        _maxCanvasWidth = width;
        _maxCanvasHeight = height;
    }

    public EllipseShape GetEllipse()
    {
        int radius = Randomizer.GetRandomNumberBetween(25, 100);

        var maxRectangleDimension = Math.Max(radius, radius);

        int max =
            (int)Math.Max(
                (_maxCanvasHeight - maxRectangleDimension),
                (_maxCanvasWidth - maxRectangleDimension));

        var edgeOffset = (int)(maxRectangleDimension / 2);

        var top = Randomizer.GetRandomNumberBetween(1 + edgeOffset, max);
        var left = Randomizer.GetRandomNumberBetween(1, max);

        return new EllipseShape
        {
            FillColor = Randomizer.GetRandomColor(),
            Radius = radius,
            RotationAngle = 0,
            Top = top,
            Left = left
        };
    }
}