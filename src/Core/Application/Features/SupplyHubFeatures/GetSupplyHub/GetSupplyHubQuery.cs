using MediatR;
using Application.DTOs;

namespace Application.Features.SupplyHubFeatures.GetSupplyHub
{
    public class GetSupplyHubQuery : IRequest<SupplyHubDTO>
    {
        public Guid Id { get; set; }
    }
}
