using _301273104_rosario_lab1.Models;
using _301273104_rosario_lab1.Services;
using System.Windows;

namespace _301273104_rosario_lab1.Commands
{
    public class LoadBucketsCommand : CommandBase
    {
        private readonly IStorageService _storageService;
        private readonly BucketListModel _bucketListModel;

        public LoadBucketsCommand(
            IStorageService storageService, 
            BucketListModel bucketListModel)
        {
            _storageService = storageService;
            _bucketListModel = bucketListModel;
        }
        public override void Execute(object? parameter)
        {
            _ = ExecuteAsync();
        }

        private async Task ExecuteAsync()
        {
            try
            {
                var response = await _storageService.GetBuckets();

                _bucketListModel.Buckets.Clear();

                foreach (var bucket in response.Buckets)
                {
                    _bucketListModel.Buckets.Add(new DisplayBucketModel
                    {
                        Name = bucket.BucketName,
                        CreationDate = bucket.CreationDate
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load buckets: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
