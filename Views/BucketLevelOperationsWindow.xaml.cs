using _301273104_rosario_lab1.ViewModels;
using System.Windows;

namespace _301273104_rosario_lab1.Views
{
    /// <summary>
    /// Interaction logic for BucketLevelOperationsWindow.xaml
    /// </summary>
    public partial class BucketLevelOperationsWindow : Window
    {
        public BucketLevelOperationsWindow(BucketLevelOperationsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            // Run LoadBucketsCommand when the window finishes loading
            Loaded += (_, __) =>
            {
                if (viewModel.LoadBucketsCommand.CanExecute(null))
                    viewModel.LoadBucketsCommand.Execute(null);
            };
        }
    }
}
