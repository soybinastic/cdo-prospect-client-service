namespace CDOProspectClient.Infrastructure.Helpers.Jwt;

public interface IJwtService
{
    string GenerateToken(
        string userId,
        string userCredential,
        List<string> roles
    );
}