using _301273104_rosario_lab1.Models;
using _301273104_rosario_lab1.Services;

namespace _301273104_rosario_lab1.Commands
{
    public class DeleteBucketCommand : CommandBase
    {
        private readonly SelectedBucketModel _bucketModel;
        private readonly IStorageService _storageService;

        public DeleteBucketCommand(
            SelectedBucketModel bucketModel,
            IStorageService storageService
            )
        {
            _bucketModel = bucketModel;
            _storageService = storageService;
        }
        public override void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }
    }
}
