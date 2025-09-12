using System.ComponentModel;

namespace _301273104_rosario_lab1.Models
{
    public class CreateBucketModel : INotifyPropertyChanged
    {
        private string? _bucketName;
        public string? BucketName
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

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
