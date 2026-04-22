
using Amazon.S3;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddControllers();

// configurar extensões para organizar o código
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddScoped<S3Service>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var s3Client = scope.ServiceProvider.GetRequiredService<IAmazonS3>();
    var bucketName = "meu-bucket"; 
    
    var exists = await Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(s3Client, bucketName);
    if (!exists)
    {
        await s3Client.PutBucketAsync(bucketName);
        Console.WriteLine($"[LocalStack] Bucket {bucketName} criado com sucesso.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.Run();

