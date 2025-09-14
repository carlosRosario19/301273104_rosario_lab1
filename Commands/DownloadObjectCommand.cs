using _301273104_rosario_lab1.Models;
using _301273104_rosario_lab1.Services;
using System.Windows;

namespace _301273104_rosario_lab1.Commands
{
    public class DownloadObjectCommand : CommandBase
    {
        private readonly SelectedObjectModel _selectedObject;
        private readonly IStorageService _storageService;

        public DownloadObjectCommand(SelectedObjectModel selectedObject, IStorageService storageService)
        {
            _selectedObject = selectedObject;
            _storageService = storageService;
        }
        public override void Execute(object? parameter)
        {
            _ = ExecuteAsync();
        }

        private async Task ExecuteAsync()
        {
            try
            {
                if (_selectedObject.Object == null)
                {
                    MessageBox.Show(
                        "No object is selected for download.",
                        "Validation Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning
                    );
                    return;
                }

                // Validate bucket and object name
                if (string.IsNullOrWhiteSpace(_selectedObject.Object.BucketName) ||
                    string.IsNullOrWhiteSpace(_selectedObject.Object.ObjectName))
                {
                    MessageBox.Show(
                        "Invalid object details. Bucket name or object name is missing.",
                        "Validation Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning
                    );
                    return;
                }

                // Attempt download
                bool success = await _storageService.DownloadObjectAsync(
                    _selectedObject.Object.BucketName,
                    _selectedObject.Object.ObjectName
                );

                if (!success)
                {
                    MessageBox.Show(
                        $"Failed to download object '{_selectedObject.Object.ObjectName}'.",
                        "Failure",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"An error occurred while downloading the object: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }
    }
}
