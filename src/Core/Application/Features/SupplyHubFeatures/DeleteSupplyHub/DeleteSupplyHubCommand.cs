using Application.DTOs;
using MediatR;

namespace Application.Features.SupplyHubFeatures.DeleteSupplyHub
{
    public class DeleteSupplyHubCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
