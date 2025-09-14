using System.ComponentModel;

namespace _301273104_rosario_lab1.Models
{
    public class UploadObjectModel : INotifyPropertyChanged
    {
        private string _bucketName = string.Empty;
        public string BucketName
        {
            get => _bucketName;
            set
            {
                if (_bucketName != value)
                {
                    _bucketName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BucketName)));
                }
            }
        }

        private string _objectName = string.Empty;
        public string ObjectName
        {
            get => _objectName;
            set
            {
                if (_objectName != value)
                {
                    _objectName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ObjectName)));
                }
            }
        }

        private string _filePath = string.Empty;
        public string FilePath
        {
            get => _filePath;
            set
            {
                if (_filePath != value)
                {
                    _filePath = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilePath)));
                }
            }
        }

        public void Clear()
        {
            BucketName = string.Empty;
            ObjectName = string.Empty;
            FilePath = string.Empty;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

    }
}
