using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CDOProspectClient.Contracts.Validators;


public class FileLengthAttribute : ValidationAttribute
{
    private readonly int _maxLength;
    public FileLengthAttribute(int maxLength, string errorMessage = "Please upload a file")
    {
        _maxLength = maxLength;
        ErrorMessage = errorMessage;
    }
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value is IFormFileCollection files)
        {
            if(files.Count == _maxLength)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
        }
        
        return new ValidationResult($"Please upload {_maxLength} file/s");
    }
}