using MediatR;
using AutoMapper;
using Application.Abstractions;

namespace Application.Features.SupplyHubFeatures.ChangeSupplyHubParent
{
    public class ChangeSupplyHubParentCommandHandler : IRequestHandler<ChangeSupplyHubParentCommand>
    {
        private readonly ISupplyHubAbstractions _SupplyHubRepository;
        public ChangeSupplyHubParentCommandHandler(ISupplyHubAbstractions supplyHubRepository)
        {
            _SupplyHubRepository = supplyHubRepository;
        }


        public async Task Handle(ChangeSupplyHubParentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _SupplyHubRepository.ChangeParent(request.SupplyHubId, request.NewParentId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Не удалось изменить родителя SupplyHub: {ex.Message}", ex);
            }
        }
    }
}
