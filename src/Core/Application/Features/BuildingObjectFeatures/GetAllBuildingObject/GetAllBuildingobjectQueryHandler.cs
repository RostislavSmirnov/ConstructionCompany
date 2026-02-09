using Application.Abstractions;
using Application.DTOs;
using MediatR;
using AutoMapper;
using Domain.Entities.ConstructionProject;

namespace Application.Features.BuildingObjectFeatures.GetAllBuildingObject
{
    public class GetAllBuildingobjectQueryHandler : IRequestHandler<GetAllBuildingobjectQuery, List<BuildingObjectDTO>>
    {
        private readonly IBuildingObjectAbstractions _buildingObjectRepository;
        private readonly IMapper _mapper;
        public GetAllBuildingobjectQueryHandler(IBuildingObjectAbstractions buildingObjectRepository, IMapper mapper)
        {
            _buildingObjectRepository = buildingObjectRepository;
            _mapper = mapper;
        }


        public async Task<List<BuildingObjectDTO>> Handle(GetAllBuildingobjectQuery request, CancellationToken cancellationToken)
        {
            List<BuildingObject> buildingObjects = await _buildingObjectRepository.GetAllBuildingObject();
            List<BuildingObjectDTO> result = _mapper.Map<List<BuildingObjectDTO>>(buildingObjects);
            return result;
        }
    }
}
