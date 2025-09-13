using _301273104_rosario_lab1.Stores;
using System.Windows;

namespace _301273104_rosario_lab1.Commands
{
    public class RefreshBucketsCommand : CommandBase
    {
        private readonly InMemoryBucketStore _bucketStore;

        public RefreshBucketsCommand(InMemoryBucketStore bucketStore)
        {
            _bucketStore = bucketStore;
        }
        public override void Execute(object? parameter)
        {
            _ = ExecuteAsync();
        }

        private async Task ExecuteAsync()
        {
            try
            {
                await _bucketStore.RefreshBucketsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to refresh buckets: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
