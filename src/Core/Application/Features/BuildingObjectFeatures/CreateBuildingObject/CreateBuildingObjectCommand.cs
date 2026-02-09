using Application.DTOs;
using MediatR;

namespace Application.Features.BuildingObjectFeatures.CreateBuildingObject;

public class CreateBuildingObjectCommand : IRequest<BuildingObjectDTO>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}
