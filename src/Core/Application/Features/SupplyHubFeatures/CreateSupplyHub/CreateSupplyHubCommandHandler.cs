using Application.Abstractions;
using Application.DTOs;
using AutoMapper;
using Domain.Entities.ConstructionProject;
using MediatR;

namespace Application.Features.SupplyHubFeatures.CreateSupplyHub;

public class CreateSupplyHubCommandHandler : IRequestHandler<CreateSupplyHubCommand, SupplyHubDTO>
{
    private readonly ISupplyHubAbstractions _SupplyHubRepository;
    private readonly IBuildingObjectAbstractions _BuildingObjectRepository;
    private readonly IMapper _mapper;

    public CreateSupplyHubCommandHandler(ISupplyHubAbstractions supplyHubRepository, IBuildingObjectAbstractions buildingObjectRepository, IMapper mapper)
    {
        _SupplyHubRepository = supplyHubRepository;
        _BuildingObjectRepository = buildingObjectRepository;
        _mapper = mapper;
    }

    public async Task<SupplyHubDTO> Handle(CreateSupplyHubCommand request, CancellationToken cancellationToken)
    {
        try
        {
            BuildingObject building = await _BuildingObjectRepository.GetBuildingObjectById(request.BuildingObjectId);
            if (building == null)
                throw new Exception("BuildingObject не найден");

            SupplyHub? parent = null;
            if (request.ParentId.HasValue)
            {
                parent = await _SupplyHubRepository.GetSupplyHubById(request.ParentId.Value);
                if (parent == null)
                    throw new Exception("Родитель не найден");
            }

            SupplyHub newHub = SupplyHub.Create(request.Name, request.Description, parent);
            newHub.AttachTo(building);

            newHub.GetType().GetProperty("BuildingObjectId")?.SetValue(newHub, request.BuildingObjectId);

            SupplyHub created = await _SupplyHubRepository.CreateSupplyHub(newHub);

            return _mapper.Map<SupplyHubDTO>(created);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
