using System.ComponentModel;

namespace _301273104_rosario_lab1.Models
{
    public class SelectedBucketInComboModel : INotifyPropertyChanged
    {
        private BucketModel? _bucket;

        public BucketModel? Bucket
        {
            get => _bucket;
            set
            {
                if (_bucket != value)
                {
                    _bucket = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Bucket)));
                }
            }
        }
        public void Clear()
        {
            Bucket = null; // automatically fires PropertyChanged
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
