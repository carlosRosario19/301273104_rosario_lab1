using Amazon.S3.Model;

namespace _301273104_rosario_lab1.Services
{
    public interface IStorageService
    {
        Task<bool> CreateBucketAsync(string bucketName);
        Task<ListBucketsResponse> GetBuckets();
    }
}
