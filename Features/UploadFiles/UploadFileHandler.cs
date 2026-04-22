using Amazon.S3;
using Amazon.S3.Model;

namespace UploadS3Api.Features.UploadFile;

public class UploadFileHandler
{
    private readonly IAmazonS3 _s3Client;
    public UploadFileHandler(IAmazonS3 s3Client) => _s3Client = s3Client;

    public async Task ExecuteAsync(IFormFile file)
    {
        using var stream = file.OpenReadStream();
        var request = new PutObjectRequest
        {
            BucketName = "meu-bucket",
            Key = file.FileName,
            InputStream = stream
        };
        await _s3Client.PutObjectAsync(request);
    }
}