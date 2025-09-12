using _301273104_rosario_lab1.Commands;
using _301273104_rosario_lab1.Models;

namespace _301273104_rosario_lab1.ViewModels
{
    public class BucketLevelOperationsViewModel : ViewModelBase
    {
        private readonly CreateBucketModel _createBucketModel;
        private bool _canCreateBucket;

        public string BucketName
        {
            get => _createBucketModel.BucketName ?? "";
            set
            {
                if (SetProperty(ref _createBucketModel.BucketName, value))
                {
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

        public BucketLevelOperationsViewModel(
            CreateBucketModel createBucketModel,
            BackToMainWindowCommand backToMainWindowCommand,
            CreateBucketCommand createBucketCommand
            )
        {
            _createBucketModel = createBucketModel;
            BackToMainWindowCommand = backToMainWindowCommand;
            CreateBucketCommand = createBucketCommand;
            CanCreateBucket = false;
        }
    }
}
