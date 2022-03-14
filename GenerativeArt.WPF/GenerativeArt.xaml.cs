using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using GenerativeArt.Core;

namespace GenerativeArt.WPF;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void CreateRectangles_OnClick(object sender, RoutedEventArgs e)
    {
        for (int i = 0; i < 50; i++)
        {
            Rectangle rect = new Rectangle
            {
                Height = RngCreator.GetNumberBetween(10, 50),
                Width = RngCreator.GetNumberBetween(50, 150)
            };

            SolidColorBrush rectangleFillBrush = RandomBrush();

            SolidColorBrush borderFillBrush = new SolidColorBrush();
            borderFillBrush.Color = Colors.Black;

            rect.StrokeThickness = 2;
            rect.Stroke = borderFillBrush;
            rect.Fill = rectangleFillBrush;

            rect.RenderTransformOrigin = new Point(0.5, 0.5);
            rect.RenderTransform = new RotateTransform(RngCreator.GetNumberBetween(0, 359));

            ShapeCanvas.Children.Add(rect);

            var maxRectangleDimension = Math.Max(rect.Height, rect.Width);

            int max =
                (int)Math.Max(
                    (ShapeCanvas.Height - maxRectangleDimension),
                    (ShapeCanvas.Width - maxRectangleDimension));

            Canvas.SetTop(rect, RngCreator.GetNumberBetween(1 + (int)(maxRectangleDimension / 2), max));
            Canvas.SetLeft(rect, RngCreator.GetNumberBetween(1 + (int)(maxRectangleDimension / 2), max));
        }
    }

    private void Exit_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private SolidColorBrush RandomBrush()
    {
        return new SolidColorBrush(Color.FromArgb(255,
            RandomByteValue(), RandomByteValue(), RandomByteValue()));
    }

    private static byte RandomByteValue()
    {
        return (byte)RngCreator.GetNumberBetween(0, byte.MaxValue + 1);
    }
}