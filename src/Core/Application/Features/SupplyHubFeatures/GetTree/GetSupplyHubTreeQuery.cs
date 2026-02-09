using Application.DTOs;
using MediatR;

namespace Application.Features.SupplyHubFeatures.GetTree
{
    public class GetSupplyHubTreeQuery : IRequest<List<SupplyHubDTO>>
    {
    }
}
