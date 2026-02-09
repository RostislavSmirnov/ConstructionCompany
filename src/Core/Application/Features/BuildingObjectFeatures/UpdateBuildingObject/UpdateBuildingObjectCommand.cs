using Application.DTOs;
using MediatR;

namespace Application.Features.BuildingObjectFeatures.UpdateBuildingObject
{
    public class UpdateBuildingObjectCommand : IRequest<BuildingObjectDTO>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
    }
}
