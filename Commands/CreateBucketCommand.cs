using _301273104_rosario_lab1.Models;
using System.Windows;

namespace _301273104_rosario_lab1.Commands
{
    public class CreateBucketCommand : CommandBase
    {
        private readonly CreateBucketModel _bucketModel;

        public CreateBucketCommand(CreateBucketModel bucketModel)
        {
            _bucketModel = bucketModel;
        }
        public override void Execute(object? parameter)
        {
            MessageBox.Show(_bucketModel.BucketName, "Bucket Name");
        }
    }
}
