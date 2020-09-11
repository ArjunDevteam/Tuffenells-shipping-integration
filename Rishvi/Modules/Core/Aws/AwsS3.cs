using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Spinx.Web.Modules.Core.Aws
{
    public class AwsS3
    {
        public static string AwsBuketName = "shipping-integration-file-storage";
        public static string AwsFolderName = "Authorization";
        public static string AwsAccessKey = "AKIAZCHLP2HSH6LRL7HP";
        public static string AwsSecretKey = "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";

        public static bool DeleteImageToAws(string fileKey)
        {
            try
            {
                IAmazonS3 client = AwsAuthentication();
                DeleteObjectRequest request = new DeleteObjectRequest()
                {
                    BucketName = AwsBuketName,
                    Key = AwsFolderName + "/" + fileKey
                };
                Task<DeleteObjectResponse> response = client.DeleteObjectAsync(request);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static IAmazonS3 AwsAuthentication()
        {
            AWSCredentials awsCredentials = new BasicAWSCredentials(AwsAccessKey, AwsSecretKey);
            IAmazonS3 client = new AmazonS3Client(awsCredentials, Amazon.RegionEndpoint.EUWest2);
            return client;
        }

        public static bool UploadFileToS3(System.IO.Stream fileInputStream, string fileKey)
        {
            try
            {
                IAmazonS3 client = AwsAuthentication();
                TransferUtility utility = new TransferUtility(client);

                TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();
                request.BucketName = AwsBuketName;
                request.Key = AwsFolderName + "/" + fileKey;
                request.InputStream = fileInputStream;
                request.CannedACL = S3CannedACL.PublicReadWrite;
                request.StorageClass = S3StorageClass.Standard;

                utility.Upload(request);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static MemoryStream GetS3ImageForInputStream(string fileKey)
        {
            using (IAmazonS3 client = AwsAuthentication())
            {
                MemoryStream file = new MemoryStream();
                try
                {
                    Task<GetObjectResponse> objectResponse = client.GetObjectAsync(new GetObjectRequest()
                    {
                        BucketName = AwsBuketName,
                        Key = AwsFolderName + "/" + fileKey
                    });

                    if (objectResponse.Result.ResponseStream != null)
                    {
                        long transferred = 0L;
                        BufferedStream stream2 = new BufferedStream(objectResponse.Result.ResponseStream);
                        byte[] buffer = new byte[0x2000];
                        int count = 0;
                        while ((count = stream2.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            file.Write(buffer, 0, count);
                        }
                    }
                    return file;
                }
                catch (AmazonS3Exception ex)
                {
                    if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                        return null;
                }
            }
            return null;
        }

        public static bool S3FileIsExists(string fileKey)
        {
            using (IAmazonS3 client = AwsAuthentication())
            {
                GetObjectRequest request = new GetObjectRequest();
                request.BucketName = AwsBuketName;
                request.Key = AwsFolderName + "/" + fileKey;
                try
                {
                    Task<GetObjectResponse> response = client.GetObjectAsync(request);
                    if (response.Result.ResponseStream != null)
                    {
                        return true;
                    }
                }
                catch (Amazon.S3.AmazonS3Exception ex)
                {
                    if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                        return false;
                    //status wasn't not found, so throw the exception
                    //throw;
                }
            }
            return false;
        }

        public static string GetS3File(string fileKey)
        {
            var jsonString = "";
            using (IAmazonS3 client = AwsAuthentication())
            {
                GetObjectRequest request = new GetObjectRequest();
                request.BucketName = AwsBuketName;
                request.Key = AwsFolderName + "/" + fileKey;
                try
                {
                    Task<GetObjectResponse> response = client.GetObjectAsync(request);
                    using (Stream responseStream = response.Result.ResponseStream)
                    using (StreamReader reader = new StreamReader(responseStream))
                    {

                        jsonString = reader.ReadToEnd();

                    }
                    return jsonString;
                }
                catch (Amazon.S3.AmazonS3Exception ex)
                {
                    //if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                    //    return false;
                    //status wasn't not found, so throw the exception
                    //throw;
                }
            }
            return string.Empty;
        }

        public static bool CopyFileFormFolder(string sourceKey, string destinationKey)
        {
            try
            {
                using (IAmazonS3 client = AwsAuthentication())
                {
                    CopyObjectRequest request = new CopyObjectRequest();
                    request.SourceBucket = AwsBuketName;
                    request.SourceKey = AwsFolderName + "/" + sourceKey;
                    request.DestinationBucket = AwsBuketName;
                    request.DestinationKey = AwsFolderName + "/" + destinationKey;
                    request.CannedACL = S3CannedACL.PublicReadWrite;
                    request.StorageClass = S3StorageClass.Standard;

                    Task<CopyObjectResponse> response = client.CopyObjectAsync(request);

                    if (response.Result.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return true;
                    }
                }
            }
            catch (Amazon.S3.AmazonS3Exception ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
    }
}