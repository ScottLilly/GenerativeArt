﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GenerativeArt.ViewModels;
using Microsoft.Win32;

namespace GenerativeArt.WPF;

public partial class CanvasView : Window
{
    private CanvasViewModel VM => DataContext as CanvasViewModel;

    private readonly Dictionary<Key, Action> _userInputActions =
        new Dictionary<Key, Action>();

    public CanvasView()
    {
        InitializeComponent();

        DataContext = new CanvasViewModel();

        _userInputActions.Add(Key.C, () => AddEllipse_OnClick(this, new RoutedEventArgs()));
        _userInputActions.Add(Key.R, () => AddRectangle_OnClick(this, new RoutedEventArgs()));
    }

    private void ClearShapes_OnClick(object sender, RoutedEventArgs e)
    {
        VM.ClearShapes();
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

    private void AddRectangle_OnClick(object sender, RoutedEventArgs e)
    {
        VM.AddRectangle();
    }

    private void AddEllipse_OnClick(object sender, RoutedEventArgs e)
    {
        VM.AddEllipse();
    }

    private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (_userInputActions.ContainsKey(e.Key))
        {
            _userInputActions[e.Key].Invoke();

            e.Handled = true;
        }
    }
}