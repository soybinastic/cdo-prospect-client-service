using System.ComponentModel.DataAnnotations;

namespace CDOProspectClient.Contracts.Property;


public record SavePropertyRequest(
    int AgentId, 
    int PropertyTypeId,
    [Required]string Name,
    string Description,
    [Required]decimal Price);

public record UpdatePropertyImage(int PropertyId);

