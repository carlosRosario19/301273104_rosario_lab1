using _301273104_rosario_lab1.Commands;
using _301273104_rosario_lab1.Models;
using _301273104_rosario_lab1.Stores;
using System.ComponentModel;
using System.Windows.Data;

namespace _301273104_rosario_lab1.ViewModels
{
    public class ObjectLevelOperationsViewModel : ViewModelBase
    {
        public readonly InMemoryBucketStore _bucketStore;
        public readonly ObjectList _objectList;
        public readonly SelectedBucketModel _selectedBucket;
        public readonly SelectedObjectModel _selectedObject;

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
                }
            }
        }

        private bool _canDeleteObject;
        public bool CanDeleteObject
        {
            get => _canDeleteObject;
            set => SetProperty(ref _canDeleteObject, value);
        }

        public ICollectionView BucketsView { get; }
        public ICollectionView ObjectsView { get; }


        public CommandBase ListObjectsCommand { get; }
        public CommandBase DeleteObjectCommand { get; }
        public CommandBase BackToMainWindowCommand { get; }

        public ObjectLevelOperationsViewModel(
            InMemoryBucketStore bucketStore,
            ObjectList objectList,
            SelectedBucketModel selectedBucket,
            SelectedObjectModel selectedObject,
            ListObjectsCommand listObjectsCommand,
            DeleteObjectCommand deleteObjectCommand,
            BackToMainWindowCommand backToMainWindowCommand)
        {
            _bucketStore = bucketStore;
            _objectList = objectList;
            _selectedBucket = selectedBucket;
            _selectedObject = selectedObject;

            ListObjectsCommand = listObjectsCommand;
            DeleteObjectCommand = deleteObjectCommand;
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
                }
            };
        }
    }
}
