using Application.DTOs;
using MediatR;
using AutoMapper;
using Application.Abstractions;
using Domain.Entities.ConstructionProject;

namespace Application.Features.BuildingObjectFeatures.GetBuildingObject
{
    public class GetBuildingObjectQueryHandler : IRequestHandler<GetBuildingObjectQuery, BuildingObjectDTO>
    {
        private readonly IBuildingObjectAbstractions _buildingObjectRepository;
        private readonly IMapper _mapper;
        public GetBuildingObjectQueryHandler(IBuildingObjectAbstractions buildingObjectRepository, IMapper mapper)
        {
            _buildingObjectRepository = buildingObjectRepository;
            _mapper = mapper;
        }


        public async Task<BuildingObjectDTO> Handle(GetBuildingObjectQuery request, CancellationToken cancellationToken)
        {
            BuildingObject buildingObject = await _buildingObjectRepository.GetBuildingObjectById(request.Id);
            BuildingObjectDTO result = _mapper.Map<BuildingObjectDTO>(buildingObject);
            return result;
        }
    }
}
