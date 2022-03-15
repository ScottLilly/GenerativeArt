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

        for (int i = 0; i < 100; i++)
        {
            Shapes.Add(RandomShapeGenerator.GetRectangle(Height, Width));
        }
    }
}