using Amazon.S3;
using Amazon.S3.Model;

public class S3Service
{
    private readonly IAmazonS3 _s3Client;
    private string _bucketName = "meu-bucket";

    public S3Service(IAmazonS3 s3Client) => _s3Client = s3Client;

    public async Task<string> UploadFileAsync(IFormFile file)
    {
        using var stream = file.OpenReadStream();
        var request = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = file.FileName,
            InputStream = stream,
            ContentType = file.ContentType
        };

        await _s3Client.PutObjectAsync(request);
        return file.FileName;
    }

    public async Task<List<string>> ListFilesAsync()
    {
        
        var request = new ListObjectsV2Request
        {
            BucketName = _bucketName
        };

        

        var response = await _s3Client.ListObjectsV2Async(request);
        
        return response.S3Objects.Select(o => o.Key).ToList();
    }

    public string GetFileUrl(string fileName)
    {
        return _s3Client.GetPreSignedURL(new GetPreSignedUrlRequest
        {
            BucketName = _bucketName,
            Key = fileName,
            Expires = DateTime.UtcNow.AddHours(1)
        });
    }
}