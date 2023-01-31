using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace CDOProspectClient.Infrastructure.Helpers.Upload;

public class UploadService : IUploadService
{
    private readonly Cloudinary _cloudinary;
    public UploadService(Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }
    public async Task<ImageUploadResult> Image(IFormFile formFile, string directory)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(formFile.FileName, formFile.OpenReadStream()),
            UniqueFilename = false,
            UseFilename = true,
            Overwrite = true,
            Folder = directory
        };

        var result = await _cloudinary.UploadAsync(uploadParams);
        return result;
    }

    public async Task<List<ImageUploadResult>> Images(IFormFileCollection formFiles, string directory)
    {
        var results = new List<ImageUploadResult>();

        foreach(var file in formFiles)
        {
            var result = await Image(file, directory);
            results.Add(result);
        }

        return results;
    }
}