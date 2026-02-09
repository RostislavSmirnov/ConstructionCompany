using Domain.Entities.ConstructionProject;

namespace Application.Abstractions;

public interface IBuildingObjectAbstractions
{
    Task<BuildingObject> CreateBuildingObject(BuildingObject buildingObject);
    Task<BuildingObject> DeleteBuildingObject(BuildingObject buildingObject);
    Task<List<BuildingObject>> GetAllBuildingObject();
    Task<BuildingObject> GetBuildingObjectById(Guid Id);
    Task<BuildingObject> UpdateBuildingObject(BuildingObject buildingObject);
}
