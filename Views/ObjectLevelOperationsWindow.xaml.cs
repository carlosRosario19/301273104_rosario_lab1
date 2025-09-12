using _301273104_rosario_lab1.ViewModels;
using System.Windows;

namespace _301273104_rosario_lab1.Views
{
    /// <summary>
    /// Interaction logic for ObjectLevelOperationsWindow.xaml
    /// </summary>
    public partial class ObjectLevelOperationsWindow : Window
    {
        public ObjectLevelOperationsWindow(ObjectLevelOperationsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
