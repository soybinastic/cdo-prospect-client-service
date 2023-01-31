using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace CDOProspectClient.Infrastructure.Helpers.Upload;

public interface IUploadService
{
    Task<ImageUploadResult> Image(IFormFile formFile, string directory);
    Task<List<ImageUploadResult>> Images(IFormFileCollection formFiles, string directory);
}