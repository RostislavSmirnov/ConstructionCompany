using Application.Abstractions;
using Application.DTOs;
using AutoMapper;
using Domain.Entities.ConstructionProject;
using MediatR;

namespace Application.Features.SupplyHubFeatures.GetSupplyHub
{
    public class GetSupplyHubQueryHandler : IRequestHandler<GetSupplyHubQuery, SupplyHubDTO>
    {
        private readonly IMapper _mapper;
        private readonly ISupplyHubAbstractions _supplyHubRepository;
        public GetSupplyHubQueryHandler(IMapper mapper, ISupplyHubAbstractions supplyHubRepository)
        {
            _mapper = mapper;
            _supplyHubRepository = supplyHubRepository;
        }


        public async Task<SupplyHubDTO> Handle(GetSupplyHubQuery request, CancellationToken cancellationToken)
        {
            SupplyHub? supplyHub = await _supplyHubRepository.GetSupplyHubById(request.Id);
            if (supplyHub is null)
            {
                throw new Exception("SupplyHub не найден");
            }
            SupplyHubDTO result = _mapper.Map<SupplyHubDTO>(supplyHub);
            return result;
        }
    }
}
