using Application.Abstractions;
using Application.DTOs;
using AutoMapper;
using MediatR;

namespace Application.Features.SupplyHubFeatures.DeleteSupplyHub;

public class DeleteSupplyHubCommandHandler : IRequestHandler<DeleteSupplyHubCommand>
{
    private readonly ISupplyHubAbstractions _SupplyHubRepository;
    public DeleteSupplyHubCommandHandler(ISupplyHubAbstractions supplyHubRepository)
    {
        _SupplyHubRepository = supplyHubRepository;
    }


    public async Task Handle(DeleteSupplyHubCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _SupplyHubRepository.DeleteSupplyHub(request.Id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка при удалении SupplyHub: {ex.Message}", ex);
        }
    }
}
