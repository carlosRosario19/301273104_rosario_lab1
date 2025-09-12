using System.Collections.ObjectModel;

namespace _301273104_rosario_lab1.Models
{
    public class BucketListModel
    {
        public ObservableCollection<DisplayBucketModel> Buckets { get; } = new ObservableCollection<DisplayBucketModel>();
    }
}
