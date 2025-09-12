using _301273104_rosario_lab1.Commands;
using _301273104_rosario_lab1.Models;
using System.Collections.ObjectModel;

namespace _301273104_rosario_lab1.ViewModels
{
    public class BucketLevelOperationsViewModel : ViewModelBase
    {
        private readonly CreateBucketModel _createBucketModel;
        public ObservableCollection<DisplayBucketModel> Buckets { get; }
        private bool _canCreateBucket;

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

        public bool CanCreateBucket
        {
            get => _canCreateBucket;
            set => SetProperty(ref _canCreateBucket, value);
        }

        public CommandBase BackToMainWindowCommand { get; }
        public CommandBase CreateBucketCommand { get; }
        public CommandBase LoadBucketsCommand { get; }

        public BucketLevelOperationsViewModel(
            CreateBucketModel createBucketModel,
            BucketListModel bucketListModel,
            BackToMainWindowCommand backToMainWindowCommand,
            CreateBucketCommand createBucketCommand,
            LoadBucketsCommand loadBucketsCommand
            )
        {
            _createBucketModel = createBucketModel;
            Buckets = bucketListModel.Buckets;
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
        }


    }
}
