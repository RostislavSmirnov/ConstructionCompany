using Application.Abstractions;
using Application.DTOs;
using AutoMapper;
using Domain.Entities.ConstructionProject;
using MediatR;

namespace Application.Features.BuildingObjectFeatures.UpdateBuildingObject
{
    public class UpdateBuildingObjectCommandHandler : IRequestHandler<UpdateBuildingObjectCommand, BuildingObjectDTO>
    {
        private readonly IBuildingObjectAbstractions _context;
        private readonly IMapper _mapper;
        public UpdateBuildingObjectCommandHandler(IBuildingObjectAbstractions context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<BuildingObjectDTO> Handle(UpdateBuildingObjectCommand request, CancellationToken cancellationToken)
        {
            BuildingObject buildingObject = await _context.GetBuildingObjectById(request.Id);
            if (buildingObject == null) 
            {
                throw new Exception($"BuildingObject with Id {request.Id} not found.");
            }
            buildingObject.Update(request.Name!, request.Description!);
            BuildingObject updatedBuildingObject = await _context.UpdateBuildingObject(buildingObject);
            BuildingObjectDTO result = _mapper.Map<BuildingObjectDTO>(updatedBuildingObject);
            return result;
        }
    }
}
