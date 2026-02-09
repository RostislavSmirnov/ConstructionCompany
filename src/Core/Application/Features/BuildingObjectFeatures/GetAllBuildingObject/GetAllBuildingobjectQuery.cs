using Application.DTOs;
using MediatR;

namespace Application.Features.BuildingObjectFeatures.GetAllBuildingObject
{
    public class GetAllBuildingobjectQuery : IRequest<List<BuildingObjectDTO>>
    {
    }
}
