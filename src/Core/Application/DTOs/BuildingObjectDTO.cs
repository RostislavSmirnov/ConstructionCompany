using Domain.Entities.ConstructionProject;

namespace Application.DTOs;

public sealed class BuildingObjectDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<SupplyHubDTO> SupplyHubs { get; set; } = [];
}
