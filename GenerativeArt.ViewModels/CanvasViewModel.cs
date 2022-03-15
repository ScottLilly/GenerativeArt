using System.Collections.ObjectModel;
using GenerativeArt.Models;
using GenerativeArt.Services;

namespace GenerativeArt.ViewModels;

public class CanvasViewModel
{
    public int Height { get; set; }
    public int Width { get; set; }
    public ObservableCollection<IShape> Shapes { get; } =
        new ObservableCollection<IShape>();

    public CanvasViewModel()
    {
        Height = 800;
        Width = 800;
    }

    public void ClearShapes()
    {
        Shapes.Clear();
    }

    public void AddRectangle()
    {
        Shapes.Add(RandomShapeGenerator.GetRectangle(Height, Width));
    }

    public void AddEllipse()
    {
        Shapes.Add(RandomShapeGenerator.GetEllipse(Height, Width));
    }
}