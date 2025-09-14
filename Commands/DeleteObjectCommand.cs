using _301273104_rosario_lab1.Models;
using _301273104_rosario_lab1.Services;
using System.Windows;

namespace _301273104_rosario_lab1.Commands
{
    public class DeleteObjectCommand : CommandBase
    {
        private readonly SelectedObjectModel _selectedObjectModel;
        private readonly ListObjectsCommand _listObjectsCommand;
        private readonly IStorageService _storageService;

        public DeleteObjectCommand(
            SelectedObjectModel selectedObjectModel, 
            ListObjectsCommand listObjectsCommand, 
            IStorageService storageService)
        {
            _selectedObjectModel = selectedObjectModel;
            _listObjectsCommand = listObjectsCommand;
            _storageService = storageService;
        }

        public override void Execute(object? parameter)
        {
            _ = ExecuteAsync();
        }

        private async Task ExecuteAsync()
        {
            if (_selectedObjectModel.Object == null)
            {
                MessageBox.Show(
                    "No object selected. Please choose an object to delete.",
                    "Validation Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return;
            }

            var result = MessageBox.Show(
                $"Are you sure you want to delete '{_selectedObjectModel.Object.ObjectName}'?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result != MessageBoxResult.Yes)
                return;

            try
            {
                await _storageService.DeleteObjectAsync(
                    _selectedObjectModel.Object.BucketName,
                    _selectedObjectModel.Object.ObjectName
                );

                MessageBox.Show(
                    $"Object '{_selectedObjectModel.Object.ObjectName}' deleted successfully.",
                    "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );

                // Refresh the object list
                _listObjectsCommand.Execute(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"An error occurred while deleting the object: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }
    }
}
