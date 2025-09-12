using _301273104_rosario_lab1.Commands;
using _301273104_rosario_lab1.Models;
using System.Collections.ObjectModel;

namespace _301273104_rosario_lab1.ViewModels
{
    public class BucketLevelOperationsViewModel : ViewModelBase
    {
        private readonly CreateBucketModel _createBucketModel;
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
        public ObservableCollection<DisplayBucketModel> Buckets { get; }

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

        private readonly SelectedBucketModel _selectedBucket;
        public DisplayBucketModel? SelectedBucket
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
        public CommandBase LoadBucketsCommand { get; }

        public BucketLevelOperationsViewModel(
            CreateBucketModel createBucketModel,
            BucketListModel bucketListModel,
            SelectedBucketModel selectedBucketModel,
            BackToMainWindowCommand backToMainWindowCommand,
            CreateBucketCommand createBucketCommand,
            LoadBucketsCommand loadBucketsCommand
            )
        {
            _createBucketModel = createBucketModel;
            Buckets = bucketListModel.Buckets;
            _selectedBucket = selectedBucketModel;

            BackToMainWindowCommand = backToMainWindowCommand;
            CreateBucketCommand = createBucketCommand;
            LoadBucketsCommand = loadBucketsCommand;
            CanCreateBucket = false;

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
