using Amazon.S3.Model;

namespace _301273104_rosario_lab1.Services
{
    public interface IStorageService
    {
        Task<bool> CreateBucketAsync(string bucketName);
        Task<ListBucketsResponse> GetBucketsAync();
        Task<bool> DeleteBucketAsync(string bucketName);
        Task<ListObjectsV2Response> ListObjectsAsync(string bucketName);
        Task<bool> DeleteObjectsAsync(string bucketName);
        Task DeleteObjectAsync(string bucketName, string objectName);
        Task<bool> DownloadObjectAsync(string bucketName, string objectName);
    }
}
