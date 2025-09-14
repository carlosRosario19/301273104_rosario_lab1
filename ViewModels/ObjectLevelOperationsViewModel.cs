using _301273104_rosario_lab1.Commands;
using _301273104_rosario_lab1.Models;
using _301273104_rosario_lab1.Stores;
using System.ComponentModel;
using System.Windows.Data;

namespace _301273104_rosario_lab1.ViewModels
{
    public class ObjectLevelOperationsViewModel : ViewModelBase
    {
        private readonly InMemoryBucketStore _bucketStore;
        private readonly ObjectList _objectList;
        private readonly SelectedBucketInComboModel _selectedBucket;
        private readonly SelectedObjectModel _selectedObject;
        private readonly UploadObjectModel _uploadObjectModel;

        public BucketModel? SelectedBucket
        {
            get => _selectedBucket.Bucket;
            set
            {
                if (_selectedBucket.Bucket != value)
                {
                    _selectedBucket.Bucket = value;
                    OnPropertyChanged(); // Notify UI

                    // Trigger the command whenever a new bucket is selected
                    if (value != null && ListObjectsCommand.CanExecute(null))
                    {
                        ListObjectsCommand.Execute(null);
                    }

                    // Update UploadObjectModel bucket name
                    _uploadObjectModel.BucketName = value?.BucketName ?? string.Empty;
                    CanBrowseObject = value != null;
                }
            }
        }

        public ObjectModel? SelectedObject
        {
            get => _selectedObject.Object;
            set
            {
                if (_selectedObject.Object != value)
                {
                    _selectedObject.Object = value;
                    OnPropertyChanged(); // Notifies the view
                    CanDeleteObject = value != null;
                    CanDownloadObject = value != null;
                }
            }
        }

        public UploadObjectModel UploadObject => _uploadObjectModel;

        private bool _canDeleteObject;
        public bool CanDeleteObject
        {
            get => _canDeleteObject;
            set => SetProperty(ref _canDeleteObject, value);
        }

        private bool _canDownloadObject;
        public bool CanDownloadObject
        {
            get => _canDownloadObject;
            set => SetProperty(ref _canDownloadObject, value);
        }

        private bool _canUploadObject;
        public bool CanUploadObject
        {
            get => _canUploadObject;
            set => SetProperty(ref _canUploadObject, value);
        }

        private bool _canBrowseObject;
        public bool CanBrowseObject
        {
            get => _canBrowseObject;
            set => SetProperty(ref _canBrowseObject, value);
        }

        public ICollectionView BucketsView { get; }
        public ICollectionView ObjectsView { get; }


        public CommandBase ListObjectsCommand { get; }
        public CommandBase DeleteObjectCommand { get; }
        public CommandBase DownloadObjectCommand { get; }
        public CommandBase UploadObjectCommand { get; }
        public CommandBase BrowseObjectCommand { get; }
        public CommandBase BackToMainWindowCommand { get; }
        
        public ObjectLevelOperationsViewModel(
            InMemoryBucketStore bucketStore,
            ObjectList objectList,
            SelectedBucketInComboModel selectedBucket,
            SelectedObjectModel selectedObject,
            UploadObjectModel uploadObjectModel,
            ListObjectsCommand listObjectsCommand,
            DeleteObjectCommand deleteObjectCommand,
            DownloadObjectCommand downloadObjectCommand,
            UploadObjectCommand uploadObjectCommand,
            BrowseObjectCommand browseObjectCommand,
            BackToMainWindowCommand backToMainWindowCommand)
        {
            _bucketStore = bucketStore;
            _objectList = objectList;
            _selectedBucket = selectedBucket;
            _selectedObject = selectedObject;
            _uploadObjectModel = uploadObjectModel;

            ListObjectsCommand = listObjectsCommand;
            DeleteObjectCommand = deleteObjectCommand;
            DownloadObjectCommand = downloadObjectCommand;
            UploadObjectCommand = uploadObjectCommand;
            BrowseObjectCommand = browseObjectCommand;
            BackToMainWindowCommand = backToMainWindowCommand;

            // Build a view of bucket store
            BucketsView = CollectionViewSource.GetDefaultView(_bucketStore.Buckets);

            //Build a view of the object list
            ObjectsView = CollectionViewSource.GetDefaultView(_objectList.Objects);

            // Subscribe to model property changed
            _selectedObject.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(SelectedObjectModel.Object))
                {
                    OnPropertyChanged(nameof(SelectedObject));
                    // Enable Delete only if an object is selected
                    CanDeleteObject = _selectedObject.Object != null;
                    CanDownloadObject = _selectedObject.Object != null;
                }
            };

            // Subscribe to upload object model changes
            _uploadObjectModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(UploadObjectModel.FilePath) ||
                    e.PropertyName == nameof(UploadObjectModel.ObjectName) ||
                    e.PropertyName == nameof(UploadObjectModel.BucketName))
                {
                    CanUploadObject =
                        !string.IsNullOrWhiteSpace(_uploadObjectModel.FilePath) &&
                        !string.IsNullOrWhiteSpace(_uploadObjectModel.ObjectName) &&
                        !string.IsNullOrWhiteSpace(_uploadObjectModel.BucketName);
                }
            };

            // Subscribe to model property changed
            _selectedBucket.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(SelectedBucketInGridModel.Bucket))
                {
                    OnPropertyChanged(nameof(SelectedBucket));
                    // Enable Delete only if a bucket is selected
                    CanBrowseObject = _selectedBucket.Bucket != null;
                }
            };
        }
    }
}
