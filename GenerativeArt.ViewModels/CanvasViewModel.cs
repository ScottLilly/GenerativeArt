using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GenerativeArt.Models;
using GenerativeArt.Services;

namespace GenerativeArt.ViewModels;

public class CanvasViewModel : INotifyPropertyChanged
{
    private readonly IRectangleGenerator _randomRectangleGenerator;
    private readonly IRectangleGenerator _steppedRectangleGenerator;
    private readonly IEllipseGenerator _ellipseGenerator;
    private readonly ITileGenerator _tileGenerator;
    private readonly IConnectedLineGenerator _connectedLineGenerator;

    public int Height { get; private set; }
    public int Width { get; private set; }
    public string BackgroundColor { get; private set; }
    public ObservableCollection<IShape> Shapes { get; } =
        new ObservableCollection<IShape>();
    public int MaximumNumberOfShapesOnCanvas { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public CanvasViewModel()
    {
        BackgroundColor = "LightGray";
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
        _connectedLineGenerator =
            ShapeGeneratorFactory.GetConnectedLineGenerator(Height, Width);
    }

    public void SetCanvasSize(int width, int height)
    {
        if (width == Width || height == Height)
        {
            return;
        }

        Width = width;
        Height = height;

        Shapes.Clear();

        _randomRectangleGenerator.SetCanvasSize(width, height);
        _steppedRectangleGenerator.SetCanvasSize(width, height);
        _ellipseGenerator.SetCanvasSize(width, height);
        _tileGenerator.SetCanvasSize(width, height);
        _connectedLineGenerator.SetCanvasSize(width, height);
    }

    public void SetCanvasBackgroundColor(string color)
    {
        BackgroundColor = color;
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

    public void AddConnectedLine()
    {
        Shapes.Add(_connectedLineGenerator.GetNextLine());
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