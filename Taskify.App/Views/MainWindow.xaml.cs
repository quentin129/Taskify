using System.Windows;
using System.Windows.Shell;

namespace Taskify.App.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeWindowChrome();
            ResizeToPrimaryScreenWidth();
        }

        private void InitializeWindowChrome()
        {
            WindowChrome windowChrome = new WindowChrome
            {
                CornerRadius = new CornerRadius(10), // Adjust the corner radius as needed
                GlassFrameThickness = new Thickness(0),
                NonClientFrameEdges = NonClientFrameEdges.None
            };
            WindowChrome.SetWindowChrome(this, windowChrome);
        }

        private void ResizeToPrimaryScreenWidth()
        {
            this.Width = SystemParameters.PrimaryScreenWidth * 0.75;
            this.Height = SystemParameters.PrimaryScreenHeight * 0.75;
        }

        private void Border_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            this.DragMove();
        }
    }
}
