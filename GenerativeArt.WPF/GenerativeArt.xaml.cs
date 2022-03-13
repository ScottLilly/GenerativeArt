using System.Windows;

namespace GenerativeArt.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateRectangles_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}