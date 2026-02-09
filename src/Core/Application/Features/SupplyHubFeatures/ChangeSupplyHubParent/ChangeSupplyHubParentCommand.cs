using MediatR;
using AutoMapper;

namespace Application.Features.SupplyHubFeatures.ChangeSupplyHubParent
{
    public class ChangeSupplyHubParentCommand : IRequest
    {
        public Guid SupplyHubId { get; set; }
        public Guid? NewParentId { get; set; }
    }
}
