using Application.Abstractions;
using Application.DTOs;
using AutoMapper;
using Domain.Entities.ConstructionProject;
using MediatR;

namespace Application.Features.BuildingObjectFeatures.DeleteBuildingObject
{
    public class DeleteBuildingObjectCommandHandler : IRequestHandler<DeleteBuildingObjectCommand, BuildingObjectDTO>
    {
        private readonly IMapper _mapper;
        private readonly IBuildingObjectAbstractions _buildingObjectRepository;
        public DeleteBuildingObjectCommandHandler(IMapper mapper, IBuildingObjectAbstractions buildingObjectRepository)
        {
            _mapper = mapper;
            _buildingObjectRepository = buildingObjectRepository;
        }


        public async Task<BuildingObjectDTO> Handle(DeleteBuildingObjectCommand request, CancellationToken cancellationToken)
        {
            BuildingObject buildingObject = await _buildingObjectRepository.GetBuildingObjectById(request.Id);
            if (buildingObject == null)
            {
                throw new Exception("Building Object not found");
            }
            BuildingObject deletedObject = await _buildingObjectRepository.DeleteBuildingObject(buildingObject);
            BuildingObjectDTO result = _mapper.Map<BuildingObjectDTO>(deletedObject);
            return result;
        }
    }
}
