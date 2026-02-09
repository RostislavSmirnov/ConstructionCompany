using Application.Abstractions;
using Application.DTOs;
using AutoMapper;
using Domain.Entities.ConstructionProject;
using MediatR;

namespace Application.Features.SupplyHubFeatures.GetTree
{
    public class GetSupplyHubTreeQueryHandler : IRequestHandler<GetSupplyHubTreeQuery, List<SupplyHubDTO>>
    {
        private readonly ISupplyHubAbstractions _repository;
        private readonly IMapper _mapper;
        public GetSupplyHubTreeQueryHandler(ISupplyHubAbstractions repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<List<SupplyHubDTO>> Handle(GetSupplyHubTreeQuery request, CancellationToken cancellationToken)
        {
            // Загружаем только корни (ParentId == null) + всю иерархию
            List<SupplyHub> roots = await _repository.GetRootSupplyHubsWithTree();
            List<SupplyHubDTO> result =  _mapper.Map<List<SupplyHubDTO>>(roots);
            return result;
        }
    }
}
