using Application.Abstractions;
using Application.DTOs;
using Domain.Entities.ConstructionProject;
using MediatR;
using AutoMapper;

namespace Application.Features.BuildingObjectFeatures.CreateBuildingObject;

public class CreateBuildingObjectCommandHandler : IRequestHandler<CreateBuildingObjectCommand, BuildingObjectDTO>
{
    private readonly IBuildingObjectAbstractions _context;
    private readonly IMapper _mapper;
    public CreateBuildingObjectCommandHandler(IBuildingObjectAbstractions context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<BuildingObjectDTO> Handle(CreateBuildingObjectCommand request, CancellationToken cancellationToken)
    {
        try
        {
            BuildingObject newObject = BuildingObject.Create(request.Name, request.Description);
            BuildingObject createdObject = await _context.CreateBuildingObject(newObject);
            BuildingObjectDTO result = _mapper.Map<BuildingObjectDTO>(createdObject);
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }
}
