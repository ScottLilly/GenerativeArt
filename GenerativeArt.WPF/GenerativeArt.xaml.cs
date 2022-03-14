using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GenerativeArt.Core;
using Microsoft.Win32;

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

    private void SaveCanvasToDisk_OnClick(object sender, RoutedEventArgs e)
    {
        Rect bounds = VisualTreeHelper.GetDescendantBounds(ShapeCanvas);

        RenderTargetBitmap rtb =
            new RenderTargetBitmap((int)bounds.Width, (int)bounds.Height, 96, 96, PixelFormats.Pbgra32);

        DrawingVisual dv = new DrawingVisual();

        using (DrawingContext dc = dv.RenderOpen())
        {
            VisualBrush vb = new VisualBrush(ShapeCanvas);
            dc.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
        }

        rtb.Render(dv);

        PngBitmapEncoder png = new PngBitmapEncoder();

        png.Frames.Add(BitmapFrame.Create(rtb));

        SaveFileDialog dialog = new SaveFileDialog();
        dialog.Filter = "PNG files (*.png)|*.png";

        if (dialog.ShowDialog(this) == true)
        {
            using Stream stm = File.Create(dialog.FileName);
            png.Save(stm);
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
        //return (byte)RngCreator.GetNumberBetween(0, (byte.MaxValue + 1)/2); // Dark colors
        //return (byte)RngCreator.GetNumberBetween(128, byte.MaxValue + 1); // Light colors
    }
}