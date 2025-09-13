using _301273104_rosario_lab1.Models;
using _301273104_rosario_lab1.Services;
using System.Collections.ObjectModel;

namespace _301273104_rosario_lab1.Stores
{
    public class InMemoryBucketStore
    {
        private readonly IStorageService _storageService;
        private bool _isLoaded;

        public ObservableCollection<BucketModel> Buckets { get; } = new();

        public InMemoryBucketStore(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task LoadBucketsAsync()
        {
            if (_isLoaded) return; // only load once

            var response = await _storageService.GetBuckets();

            Buckets.Clear();
            foreach (var bucket in response.Buckets)
            {
                Buckets.Add(new BucketModel
                {
                    BucketArn = bucket.BucketArn,
                    BucketName = bucket.BucketName,
                    BucketRegion = bucket.BucketRegion,
                    CreationDate = bucket.CreationDate
                });
            }

            _isLoaded = true;
        }

        public async Task RefreshBucketsAsync()
        {
            var response = await _storageService.GetBuckets();

            Buckets.Clear();
            foreach (var bucket in response.Buckets)
            {
                Buckets.Add(new BucketModel
                {
                    BucketArn = bucket.BucketArn,
                    BucketName = bucket.BucketName,
                    BucketRegion = bucket.BucketRegion,
                    CreationDate = bucket.CreationDate
                });
            }
            _isLoaded = true;
        }
    }
}
