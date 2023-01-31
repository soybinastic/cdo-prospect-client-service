namespace CDOProspectClient.Contracts.Requirement;

public record RequirementStatusRequest(
    int Status,
    int RequirementId
);