using Microsoft.AspNetCore.Mvc;
using UploadS3Api.Features.UploadFile;

[ApiController]
[Route("api/[controller]")]
public class UploadFileController : ControllerBase
{
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadFile([FromServices] UploadFileHandler uploadFileHandler,IFormFile file)
    {           
            if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");


            await uploadFileHandler.ExecuteAsync(file);
            return Ok();
        }

    }
