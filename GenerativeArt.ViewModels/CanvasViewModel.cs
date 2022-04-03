using System.Collections.ObjectModel;
using GenerativeArt.Models;
using GenerativeArt.Services;

namespace GenerativeArt.ViewModels;

public class CanvasViewModel
{
    private readonly IRectangleGenerator _randomRectangleGenerator;
    private readonly IRectangleGenerator _steppedRectangleGenerator;
    private readonly IEllipseGenerator _ellipseGenerator;
    private readonly ITileGenerator _tileGenerator;

    public int Height { get; }
    public int Width { get; }
    public ObservableCollection<IShape> Shapes { get; } =
        new ObservableCollection<IShape>();
    public int MaximumNumberOfShapesOnCanvas { get; }

    public CanvasViewModel()
    {
        Height = 2500;
        Width = 2500;
        MaximumNumberOfShapesOnCanvas = 0;

        _randomRectangleGenerator =
            ShapeGeneratorFactory.GetRectangleGenerator(ShapeGeneratorFactory.GeneratorType.Random, Height, Width);
        _steppedRectangleGenerator =
            ShapeGeneratorFactory.GetRectangleGenerator(ShapeGeneratorFactory.GeneratorType.Stepped, Height, Width);
        _ellipseGenerator =
            ShapeGeneratorFactory.GetEllipseGenerator(ShapeGeneratorFactory.GeneratorType.Random, Height, Width);
        _tileGenerator =
            ShapeGeneratorFactory.GetTileGenerator(Height, Width, 50);
    }

    public void ClearShapes()
    {
        Shapes.Clear();
    }

    public void AddRectangle()
    {
        Shapes.Add(_randomRectangleGenerator.GetRectangle());

        RemoveOldShapes();
    }

    public void AddEllipse()
    {
        Shapes.Add(_ellipseGenerator.GetEllipse());

        RemoveOldShapes();
    }

    public void AddTiles()
    {
        var tiles = _tileGenerator.GetTiles();

        foreach (TileShape tile in tiles)
        {
            Shapes.Add(tile);
        }
    }

    private void RemoveOldShapes()
    {
        if (MaximumNumberOfShapesOnCanvas < 1)
        {
            return;
        }

        if (Shapes.Count > MaximumNumberOfShapesOnCanvas)
        {
            Shapes.RemoveAt(0);
        }
    }
}