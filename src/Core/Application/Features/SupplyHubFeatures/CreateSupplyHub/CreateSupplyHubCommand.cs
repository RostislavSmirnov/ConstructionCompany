using MediatR;
using Application.DTOs;

namespace Application.Features.SupplyHubFeatures.CreateSupplyHub;

public class CreateSupplyHubCommand : IRequest<SupplyHubDTO>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Guid BuildingObjectId { get; set; }
    public Guid? ParentId { get; set; } 
}
