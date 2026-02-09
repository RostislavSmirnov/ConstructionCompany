using Application.DTOs;
using MediatR;

namespace Application.Features.BuildingObjectFeatures.GetBuildingObject;

public class GetBuildingObjectQuery : IRequest<BuildingObjectDTO>
{
    public Guid Id { get; set; }
}
