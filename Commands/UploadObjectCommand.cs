using _301273104_rosario_lab1.Models;
using _301273104_rosario_lab1.Services;
using System.Windows;

namespace _301273104_rosario_lab1.Commands
{
    public class UploadObjectCommand : CommandBase
    {
        private readonly IStorageService _storageService;
        private readonly UploadObjectModel _uploadObjectModel;
        private readonly ListObjectsCommand _listObjectsCommand;

        public UploadObjectCommand(
            IStorageService storageService, 
            UploadObjectModel uploadObjectModel,
            ListObjectsCommand listObjectsCommand)
        {
            _storageService = storageService;
            _uploadObjectModel = uploadObjectModel;
            _listObjectsCommand = listObjectsCommand;
        }
        public override void Execute(object? parameter)
        {
            _ = ExecuteAsync();
        }

        private async Task ExecuteAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_uploadObjectModel.BucketName) ||
                    string.IsNullOrWhiteSpace(_uploadObjectModel.ObjectName) ||
                    string.IsNullOrWhiteSpace(_uploadObjectModel.FilePath))
                {
                    MessageBox.Show(
                        "Bucket name, object name, and file path must all be provided.",
                        "Validation Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning
                    );
                    return;
                }

                bool success = await _storageService.UploadObjectAsync(
                    _uploadObjectModel.BucketName,
                    _uploadObjectModel.ObjectName,
                    _uploadObjectModel.FilePath
                );

                if (success)
                {
                    // Clear the name to reset the UI
                    _uploadObjectModel.ObjectName = string.Empty;
                    // Refresh the object list
                    _listObjectsCommand.Execute(null);
                }
                else
                {
                    MessageBox.Show(
                        $"Failed to upload '{_uploadObjectModel.ObjectName}' to bucket '{_uploadObjectModel.BucketName}'.",
                        "Upload Failed",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"An error occurred while uploading the object: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }
    }
}
