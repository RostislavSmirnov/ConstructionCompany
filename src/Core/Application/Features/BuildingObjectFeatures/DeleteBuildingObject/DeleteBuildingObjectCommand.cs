using Application.DTOs;
using MediatR;

namespace Application.Features.BuildingObjectFeatures.DeleteBuildingObject
{
    public class DeleteBuildingObjectCommand : IRequest<BuildingObjectDTO>
    {
        public Guid Id { get; set; }
    }
}
