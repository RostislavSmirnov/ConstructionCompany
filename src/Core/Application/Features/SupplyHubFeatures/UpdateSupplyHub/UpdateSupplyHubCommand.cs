using Application.DTOs;
using MediatR;
using AutoMapper;

namespace Application.Features.SupplyHubFeatures.UpdateSupplyHub
{
    public class UpdateSupplyHubCommand : IRequest<SupplyHubDTO>
    {
        public Guid Id { get; set; }
        public string? Name { get;  set; }
        public string? Description { get; set; }
    }
}
