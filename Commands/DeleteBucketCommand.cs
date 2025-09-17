using _301273104_rosario_lab1.Models;
using _301273104_rosario_lab1.Services;
using System.Windows;

namespace _301273104_rosario_lab1.Commands
{
    public class DeleteBucketCommand : CommandBase
    {
        private readonly SelectedBucketInGridModel  _bucketModel;
        private readonly RefreshBucketsCommand _refreshBucketsCommand;
        private readonly IStorageService _storageService;

        public DeleteBucketCommand(
            SelectedBucketInGridModel  bucketModel,
            RefreshBucketsCommand refreshBucketsCommand,
            IStorageService storageService
            )
        {
            _bucketModel = bucketModel;
            _refreshBucketsCommand = refreshBucketsCommand;
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
                if (_bucketModel.Bucket == null)
                {
                    MessageBox.Show(
                        "No bucket is selected for deletion.",
                        "Validation Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning
                    );
                    return;
                }

                string bucketName = _bucketModel.Bucket.BucketName;

                // Step 1: Check if bucket contains objects
                var objectsResponse = await _storageService.ListObjectsAsync(_bucketModel.Bucket.BucketName);

                var objects = objectsResponse?.S3Objects ?? new List<Amazon.S3.Model.S3Object>();

                if (objects.Any())
                {
                    var result = MessageBox.Show(
                        $"Bucket '{bucketName}' contains {objects.Count} object(s).\n" +
                        "Do you still want to delete it? All contents will be lost.",
                        "Confirm Deletion",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning
                    );

                    if (result != MessageBoxResult.Yes)
                        return;

                    // Step 2: Delete all objects first
                    bool objectsDeleted = await _storageService.DeleteObjectsAsync(bucketName);

                    if (!objectsDeleted)
                    {
                        MessageBox.Show(
                            $"Failed to delete objects from bucket '{bucketName}'.",
                            "Failure",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                        );
                        return;
                    }
                }

                // Step 3: Delete the bucket
                bool deleted = await _storageService.DeleteBucketAsync(_bucketModel.Bucket.BucketName);

                if (deleted)
                {
                    _refreshBucketsCommand.Execute(null);
                }
                else
                {
                    MessageBox.Show(
                        $"Failed to delete bucket '{_bucketModel.Bucket.BucketName}'. Please try again.",
                        "Failure",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"An error occurred while deleting the bucket: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }
    }
}
