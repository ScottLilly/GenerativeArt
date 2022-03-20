using System.Collections.ObjectModel;
using GenerativeArt.Models;
using GenerativeArt.Services;

namespace GenerativeArt.ViewModels;

public class CanvasViewModel
{
    private readonly IRectangleGenerator _rectangleGenerator;
    private readonly IEllipseGenerator _ellipseGenerator;

    public int Height { get; set; }
    public int Width { get; set; }
    public ObservableCollection<IShape> Shapes { get; } =
        new ObservableCollection<IShape>();
    public int MaximumNumberOfShapesOnCanvas { get; set; }

    public CanvasViewModel()
    {
        Height = 800;
        Width = 800;
        MaximumNumberOfShapesOnCanvas = 255;

        //_rectangleGenerator =
        //    ShapeGeneratorFactory.GetRectangleGenerator(ShapeGeneratorFactory.GeneratorType.Random, Height, Width);
        _rectangleGenerator =
            ShapeGeneratorFactory.GetRectangleGenerator(ShapeGeneratorFactory.GeneratorType.Stepped, Height, Width);
        _ellipseGenerator =
            ShapeGeneratorFactory.GetEllipseGenerator(ShapeGeneratorFactory.GeneratorType.Random, Height, Width);
    }

    public void ClearShapes()
    {
        Shapes.Clear();
    }

    public void AddRectangle()
    {
        Shapes.Add(_rectangleGenerator.GetRectangle());

        RemoveOldShapes();
    }

    public void AddEllipse()
    {
        Shapes.Add(_ellipseGenerator.GetEllipse());

        RemoveOldShapes();
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