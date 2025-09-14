using _301273104_rosario_lab1.Factories;
using _301273104_rosario_lab1.Models;
using _301273104_rosario_lab1.Views;
using System.Windows;

namespace _301273104_rosario_lab1.Commands
{
    public class BackToMainWindowCommand : CommandBase
    {
        private readonly IWindowFactory _windowFactory;
        private readonly CreateBucketModel _createBucketModel;
        private readonly UploadObjectModel _uploadObjectModel;
        private readonly SelectedBucketInComboModel _selectedBucketInComboModel;
        private readonly ObjectList _objects;

        public BackToMainWindowCommand(
            IWindowFactory windowFactory,
            CreateBucketModel createBucketModel,
            UploadObjectModel uploadObjectModel,
            SelectedBucketInComboModel selectedBucketInComboModel,
            ObjectList objects)
        {
            _windowFactory = windowFactory;
            _createBucketModel = createBucketModel;
            _uploadObjectModel = uploadObjectModel;
            _selectedBucketInComboModel = selectedBucketInComboModel;
            _objects = objects;
        }
        public override void Execute(object? parameter)
        {
            // Reset selected items
            _createBucketModel.Clear();
            _uploadObjectModel.Clear();
            _selectedBucketInComboModel.Clear();
            _objects.Clear();

            // Resolve the main window from DI
            var mainWindow = _windowFactory.Create<MainWindow>();
            mainWindow.Show();

            // Close the current window (passed as parameter)
            if (parameter is Window currentWindow)
            {
                currentWindow.Close();
            }
            else
            {
                // fallback: close any open window that's not MainWindow
                Application.Current.Windows
                    .OfType<Window>()
                    .FirstOrDefault(w => !(w is MainWindow))?
                    .Close();
            }
        }
    }
}
