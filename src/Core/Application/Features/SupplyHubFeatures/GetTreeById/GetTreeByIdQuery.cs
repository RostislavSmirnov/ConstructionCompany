using Application.DTOs;
using MediatR;

namespace Application.Features.SupplyHubFeatures.GetTreeById
{
    public class GetTreeByIdQuery : IRequest<SupplyHubDTO>
    {
        public Guid Id { get; set; }
    }
}
