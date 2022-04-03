using GenerativeArt.Models;

namespace GenerativeArt.Services;

public static class ShapeGeneratorFactory
{
    public enum GeneratorType
    {
        Random,
        Stepped,
        Grid
    }

    public static IRectangleGenerator GetRectangleGenerator(GeneratorType type,
        int maxCanvasHeight, int maxCanvasWidth)
    {
        switch (type)
        {
            case GeneratorType.Random:
                return new RandomRectangleGenerator(maxCanvasHeight, maxCanvasWidth);
            case GeneratorType.Stepped:
                return new SteppedRectangleGenerator(maxCanvasHeight, maxCanvasWidth);
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

    public static ITileGenerator GetTileGenerator(int maxCanvasHeight, int maxCanvasWidth, int tileSizeInPixels)
    {
        return new ConnectedTilesGenerator(maxCanvasHeight, maxCanvasWidth, tileSizeInPixels);
    }

    public static IConnectedLineGenerator GetConnectedLineGenerator(int maxCanvasHeight, int maxCanvasWidth)
    {
        return new ConnectedLineGenerator(maxCanvasWidth, maxCanvasHeight);
    }
}