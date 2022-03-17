using GenerativeArt.Models;

namespace GenerativeArt.Services;

public static class ShapeGeneratorFactory
{
    public enum GeneratorType
    {
        Random
    }

    public static IRectangleGenerator GetRectangleGenerator(GeneratorType type,
        int maxCanvasHeight, int maxCanvasWidth)
    {
        switch (type)
        {
            case GeneratorType.Random:
                return new RandomRectangleGenerator(maxCanvasHeight, maxCanvasWidth);
            default:
                throw new ArgumentException("Invalid 'type' value");
        }
    }

    public static IEllipseGenerator GetEllipseGenerator(GeneratorType type,
        int maxCanvasHeight, int maxCanvasWidth)
    {
        switch (type)
        {
            case GeneratorType.Random:
                return new RandomEllipseGenerator(maxCanvasHeight, maxCanvasWidth);
            default:
                throw new ArgumentException("Invalid 'type' value");
        }
    }
}