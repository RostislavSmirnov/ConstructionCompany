using Domain.Entities.ConstructionProject;
using MediatR;

namespace Application.DTOs
{
    public class SupplyHubDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
        public string Description { get; init; } = null!;
        public Guid? ParentId { get; init; }
        public List<SupplyHubDTO> Children { get; init; } = [];
    }
}
