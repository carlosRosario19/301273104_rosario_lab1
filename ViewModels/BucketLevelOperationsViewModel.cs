using _301273104_rosario_lab1.Commands;
using _301273104_rosario_lab1.Models;
using _301273104_rosario_lab1.Stores;
using System.ComponentModel;
using System.Windows.Data;

namespace _301273104_rosario_lab1.ViewModels
{
    public class BucketLevelOperationsViewModel : ViewModelBase
    {
        private readonly InMemoryBucketStore _bucketStore;
        private readonly CreateBucketModel _createBucketModel;
        private readonly SelectedBucketModel _selectedBucket;

        public ICollectionView BucketsView { get; }

        public string BucketName
        {
            get => _createBucketModel.BucketName ?? "";
            set
            {
                if (_createBucketModel.BucketName != value)
                {
                    _createBucketModel.BucketName = value;
                    CanCreateBucket = !string.IsNullOrWhiteSpace(value);
                }
            }
        }

        private bool _canCreateBucket;
        public bool CanCreateBucket
        {
            get => _canCreateBucket;
            set => SetProperty(ref _canCreateBucket, value);
        }

        private bool _canDeleteBucket;
        public bool CanDeleteBucket
        {
            get => _canDeleteBucket;
            set => SetProperty(ref _canDeleteBucket, value);
        }

        public BucketModel? SelectedBucket
        {
            get => _selectedBucket.Bucket;
            set
            {
                if (_selectedBucket.Bucket != value)
                {
                    _selectedBucket.Bucket = value;
                    OnPropertyChanged(); // Notifies the view
                    CanDeleteBucket = value != null;
                }
            }
        }

        public CommandBase BackToMainWindowCommand { get; }
        public CommandBase CreateBucketCommand { get; }
        public CommandBase DeleteBucketCommand { get; }

        public BucketLevelOperationsViewModel(
            InMemoryBucketStore bucketStore,
            CreateBucketModel createBucketModel,
            SelectedBucketModel selectedBucketModel,
            BackToMainWindowCommand backToMainWindowCommand,
            CreateBucketCommand createBucketCommand,
            DeleteBucketCommand deleteBucketCommand
            )
        {
            _bucketStore = bucketStore;
            _createBucketModel = createBucketModel;
            _selectedBucket = selectedBucketModel;

            BackToMainWindowCommand = backToMainWindowCommand;
            CreateBucketCommand = createBucketCommand;
            DeleteBucketCommand = deleteBucketCommand;
            CanCreateBucket = false;

            // Build a view of bucket store
            BucketsView = CollectionViewSource.GetDefaultView(_bucketStore.Buckets);

            // Subscribe to model property changed
            _createBucketModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(CreateBucketModel.BucketName))
                {
                    // Notify view that view-model property changed
                    OnPropertyChanged(nameof(BucketName));
                    CanCreateBucket = !string.IsNullOrWhiteSpace(_createBucketModel.BucketName);
                }
            };

            _selectedBucket.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(SelectedBucketModel.Bucket))
                {
                    OnPropertyChanged(nameof(SelectedBucket));
                    // Enable Delete only if a bucket is selected
                    CanDeleteBucket = _selectedBucket.Bucket != null;
                }
            };
        }
    }
}
