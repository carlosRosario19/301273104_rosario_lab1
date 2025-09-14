using _301273104_rosario_lab1.Models;
using _301273104_rosario_lab1.Services;
using Amazon.S3.Model;
using System.Windows;

namespace _301273104_rosario_lab1.Commands
{
    public class ListObjectsCommand : CommandBase
    {
        public readonly ObjectList _objectList;
        private readonly SelectedBucketModel _selectedBucket;
        private readonly IStorageService _storageService;

        public ListObjectsCommand(
            ObjectList objectList,
            SelectedBucketModel selectedBucket,
            IStorageService storageService)
        {
            _objectList = objectList;
            _selectedBucket = selectedBucket;
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
                if (_selectedBucket.Bucket == null)
                {
                    MessageBox.Show(
                        "No bucket selected. Please choose a bucket.",
                        "Validation Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning
                    );
                    return;
                }

                // Fetch objects from S3
                ListObjectsV2Response response =
                    await _storageService.ListObjectsAsync(_selectedBucket.Bucket.BucketName);

                // Clear existing items
                _objectList.Objects.Clear();

                if (response.S3Objects != null && response.S3Objects.Any())
                {
                    foreach (var s3Object in response.S3Objects)
                    {
                        _objectList.Objects.Add(new ObjectModel
                        {
                            BucketName = _selectedBucket.Bucket.BucketName,
                            ObjectName = s3Object.Key,
                            ObjectSize = s3Object.Size,
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"An error occurred while listing objects: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }
    }
}
