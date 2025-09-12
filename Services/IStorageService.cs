using Microsoft.Extensions.FileProviders;

namespace _301273104_rosario_lab1.Services
{
    public interface IStorageService
    {
        Task<bool> CreateBucketAsync(string bucketName);
    }
}
