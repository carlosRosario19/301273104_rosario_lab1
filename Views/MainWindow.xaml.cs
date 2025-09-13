using _301273104_rosario_lab1.ViewModels;
using System.Windows;

namespace _301273104_rosario_lab1.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            // Run LoadBucketsCommand when the window finishes loading
            Loaded += (_, __) =>
            {
                if (viewModel.LoadBucketsCommands.CanExecute(null))
                    viewModel.LoadBucketsCommands.Execute(null);
            };
        }
    }
}