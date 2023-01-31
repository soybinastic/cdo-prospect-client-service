using System.ComponentModel.DataAnnotations;

namespace CDOProspectClient.Contracts.Auth;

public record LoginRequest(
    [Required]string Username,
    [Required]string Password
);

public record LoginResponse(
    string UserId,
    string Credential,
    string Token
);