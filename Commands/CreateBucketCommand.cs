using _301273104_rosario_lab1.Models;
using _301273104_rosario_lab1.Services;
using System.Windows;

namespace _301273104_rosario_lab1.Commands
{
    public class CreateBucketCommand : CommandBase
    {
        private readonly CreateBucketModel _bucketModel;
        private readonly IStorageService _storageService;

        public CreateBucketCommand(
            CreateBucketModel bucketModel,
            IStorageService storageService
            )
        {
            _bucketModel = bucketModel;
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
                if (string.IsNullOrWhiteSpace(_bucketModel.BucketName))
                {
                    MessageBox.Show("Bucket name cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                bool success = await _storageService.CreateBucketAsync(_bucketModel.BucketName);

                if (success)
                {
                    // Clear the field after success
                    _bucketModel.BucketName = string.Empty;
                    MessageBox.Show("Bucket was successfully created.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Bucket creation failed. Please try again.", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
