using System.ComponentModel;

namespace _301273104_rosario_lab1.Models
{
    public class SelectedObjectModel : INotifyPropertyChanged
    {
        private ObjectModel? _object;

        public ObjectModel? Object
        {
            get => _object;
            set
            {
                if (_object != value)
                {
                    _object = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Object)));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
