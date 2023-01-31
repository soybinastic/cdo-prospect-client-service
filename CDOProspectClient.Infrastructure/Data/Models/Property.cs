namespace CDOProspectClient.Infrastructure.Data.Models;

public class Property
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int AssignedTo { get; set; }
    public Agent Agent { get; set; } = null!;
    public int PropertyTypeId { get; set; }
    public PropertyType PropertyType { get; set; } = null!;
    public bool Available { get; set; }
    public DateTime DateCreated { get; set; }
    public string ImageUrl { get; set; } = null!;

}