
using Amazon.S3;
using Amazon.S3.Model;
using System.Windows.Media.Animation;

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

        public async Task<ListBucketsResponse> GetBucketsAync()
        {
            return await Client.ListBucketsAsync();
        }

        public async Task<bool> DeleteBucketAsync(string bucketName)
        {
            try
            {
                var request = new DeleteBucketRequest { BucketName = bucketName, };

                await Client.DeleteBucketAsync(request);
                return true;
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine($"Error deleting bucket: {ex.Message}");
                return false;
            }
        }

        public async Task<ListObjectsV2Response> ListObjectsAsync(string bucketName)
        {
            try
            {
                var request = new ListObjectsV2Request
                {
                    BucketName = bucketName,
                    MaxKeys = 1000 // This can be adjusted or configurable
                };

                var response = await Client.ListObjectsV2Async(request);
                return response;
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine($"Error listing objects in bucket '{bucketName}': {ex.Message}");
                throw; // rethrow so caller can handle if needed
            }
        }

        public async Task<bool> DeleteObjectsAsync(string bucketName)
        {
            // Iterate over the contents of the bucket and delete all objects.
            var request = new ListObjectsV2Request
            {
                BucketName = bucketName,
            };

            try
            {
                ListObjectsV2Response response;

                do
                {
                    response = await Client.ListObjectsV2Async(request);
                    response.S3Objects
                        .ForEach(async obj => await Client.DeleteObjectAsync(bucketName, obj.Key));

                    // If the response is truncated, set the request ContinuationToken
                    // from the NextContinuationToken property of the response.
                    request.ContinuationToken = response.NextContinuationToken;
                }
                while (response.IsTruncated ?? false);

                return true;
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine($"Error deleting objects: {ex.Message}");
                return false;
            }
        }

        public async Task DeleteObjectAsync(string bucketName, string objectName)
        {
            try
            {
                var deleteObjectRequest = new DeleteObjectRequest
                {
                    BucketName = bucketName,
                    Key = objectName,
                };

                await Client.DeleteObjectAsync(deleteObjectRequest);
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine($"Error encountered on server. Message:'{ex.Message}' when deleting an object.");
            }
        }
    }
}
