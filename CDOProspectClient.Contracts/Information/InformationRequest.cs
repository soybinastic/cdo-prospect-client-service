using System.ComponentModel.DataAnnotations;

namespace CDOProspectClient.Contracts.Information;

public record CreateInformationRequest(
    ProfileRequest Profile,
    AccountRequest Account
);

public record ProfileRequest(
    [Required]string Firstname,
    [Required]string Lastname,
    [Required]string Middlename,
    [Required]string Address,
    [EmailAddress, Required]string Email,
    [Phone, Required]string ContactNumber
);

public record AccountRequest(
    [Required]string Username,
    [Required]string Password
);
