namespace CDOProspectClient.Contracts.Information;

public record InformationResponse(
    ProfileRequest Profile,
    string ImageLink
);