using System.Collections.ObjectModel;

namespace _301273104_rosario_lab1.Models
{
    public class ObjectList
    {
        public ObservableCollection<ObjectModel> Objects { get; } = new();

        public void Clear()
        {
            Objects.Clear();
        }
    }
}
