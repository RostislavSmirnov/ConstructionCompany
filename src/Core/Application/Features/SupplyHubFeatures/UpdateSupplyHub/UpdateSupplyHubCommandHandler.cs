using MediatR;
using AutoMapper;
using Application.DTOs;
using Application.Abstractions;
using Domain.Entities.ConstructionProject;

namespace Application.Features.SupplyHubFeatures.UpdateSupplyHub
{
    public class UpdateSupplyHubCommandHandler : IRequestHandler<UpdateSupplyHubCommand, SupplyHubDTO>
    {
        private readonly ISupplyHubAbstractions _supplyHubRepository;
        private readonly IMapper _mapper;
        public UpdateSupplyHubCommandHandler(ISupplyHubAbstractions supplyHubRepository, IMapper mapper)
        {
            _supplyHubRepository = supplyHubRepository;
            _mapper = mapper;
        }
        public async Task<SupplyHubDTO> Handle(UpdateSupplyHubCommand request, CancellationToken cancellationToken)
        {
            SupplyHub? supplyHub = await _supplyHubRepository.GetSupplyHubById(request.Id);
            if (supplyHub is null)
            {
                throw new Exception("SupplyHub not found");
            }
            supplyHub.Update(request.Name!, request.Description!);
            SupplyHub updatedSupplyHub = await _supplyHubRepository.UpdateSupplyHub(supplyHub);
            SupplyHubDTO result = _mapper.Map<SupplyHubDTO>(updatedSupplyHub);
            return result;
        }
    }
}
