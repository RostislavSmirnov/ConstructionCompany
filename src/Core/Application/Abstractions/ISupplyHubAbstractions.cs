using Domain.Entities.ConstructionProject;

namespace Application.Abstractions
{
    public interface ISupplyHubAbstractions
    {
        Task<SupplyHub> CreateSupplyHub(SupplyHub supplyHub);
        Task<SupplyHub?> GetSupplyHubById(Guid id);
        Task<List<SupplyHub>> GetAllSupplyHubs();
        Task<SupplyHub> UpdateSupplyHub(SupplyHub supplyHub);
        Task DeleteSupplyHub(Guid id);
        Task ChangeParent(Guid supplyHubId, Guid? newParentId);
        Task<List<SupplyHub>> GetRootSupplyHubsWithTree();
        Task<SupplyHub?> GetSupplyHubWithSubTree(Guid id);
    }
}
