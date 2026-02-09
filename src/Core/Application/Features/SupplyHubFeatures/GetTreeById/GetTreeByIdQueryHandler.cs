using Application.Abstractions;
using Application.DTOs;
using AutoMapper;
using Domain.Entities.ConstructionProject;
using MediatR;

namespace Application.Features.SupplyHubFeatures.GetTreeById
{
    public class GetTreeByIdQueryHandler : IRequestHandler<GetTreeByIdQuery, SupplyHubDTO>
    {
        private readonly ISupplyHubAbstractions _supplyHubRepository;
        private readonly IMapper _mapper;
        public GetTreeByIdQueryHandler(ISupplyHubAbstractions supplyHubRepository, IMapper mapper)
        {
            _supplyHubRepository = supplyHubRepository;
            _mapper = mapper;
        }
        public async Task<SupplyHubDTO> Handle(GetTreeByIdQuery request, CancellationToken cancellationToken)
        {
            SupplyHub? hubWithTree = await _supplyHubRepository
                .GetSupplyHubWithSubTree(request.Id);

            if (hubWithTree is null)
            {
                throw new KeyNotFoundException("Узел с указанным Id не найден");
            }

            SupplyHubDTO result = _mapper.Map<SupplyHubDTO>(hubWithTree);

            return result;
        }
    }
}
