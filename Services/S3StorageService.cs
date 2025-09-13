
using Amazon.S3;
using Amazon.S3.Model;

namespace _301273104_rosario_lab1.Services
{
    public class S3StorageService : IStorageService
    {
        public IAmazonS3 Client;

        public S3StorageService(IAmazonS3 client)
        {
            Client = client;
        }

        public async Task<bool> CreateBucketAsync(string bucketName)
        {
            try
            {
                // Ensure bucket name is globally unique by appending a short GUID
                string uniqueBucketName = $"{bucketName}-{Guid.NewGuid().ToString().Substring(0, 8)}".ToLower();

                var request = new PutBucketRequest
                {
                    BucketName = uniqueBucketName,
                    UseClientRegion = true,
                };

                var response = await Client.PutBucketAsync(request);
                return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine($"Error creating bucket: '{ex.Message}'");
                return false;
            }
        }
            
        public async Task<ListBucketsResponse> GetBuckets()
        {
            return await Client.ListBucketsAsync();
        }
    }
}
