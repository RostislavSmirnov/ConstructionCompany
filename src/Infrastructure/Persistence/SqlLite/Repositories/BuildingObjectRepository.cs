using Application.Abstractions;
using Domain.Entities.ConstructionProject;
using Microsoft.EntityFrameworkCore;
using Persistence.SqlLite.DbContexts;

namespace Persistence.SqlLite.Repositories;

public class BuildingObjectRepository : IBuildingObjectAbstractions
{
    private readonly ConstructionCompanyDbContext _dbContext;
    public BuildingObjectRepository(ConstructionCompanyDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<BuildingObject> CreateBuildingObject(BuildingObject buildingObject)
    {
        try
        {
            await _dbContext.BuildingObjects.AddAsync(buildingObject);
            await _dbContext.SaveChangesAsync();
            return buildingObject;
        }
        catch (Exception ex)
        {
            throw new Exception($"Произошла ошибка при создании объекта строительства: {ex.Message}", ex);
        }
    }

    public async Task<BuildingObject> DeleteBuildingObject(BuildingObject buildingObjectId)
    {
        try
        {
            BuildingObject? buildingObject = await _dbContext.BuildingObjects
                .Include(b => b.SupplyHubs)
                .FirstOrDefaultAsync(x => x.Id == buildingObjectId.Id);

            if (buildingObject == null)
            {
                throw new Exception("Объект строительства не найден");
            }

            if (buildingObject.SupplyHubs.Any())
            {
                throw new InvalidOperationException("Нельзя удалить объект строительства, у которого есть связанные узлы поставок");
            }

            _dbContext.BuildingObjects.Remove(buildingObject);
            await _dbContext.SaveChangesAsync();
            return buildingObject;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<BuildingObject>> GetAllBuildingObject()
    {
        try
        {
            List<BuildingObject> buildingObjects = await _dbContext.BuildingObjects
                .Include(b => b.SupplyHubs)
                .ThenInclude(h => h.Children)
                .AsNoTracking()
                .ToListAsync();

            return buildingObjects;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<BuildingObject> GetBuildingObjectById(Guid Id)
    {
        try
        {
            BuildingObject? buildingObject = await _dbContext.BuildingObjects
                .FirstOrDefaultAsync(x => x.Id == Id);

            if (buildingObject == null)
            {
                throw new Exception("Объект строительства не найден");
            }

            return buildingObject;
        }
        catch (Exception ex)
        {
            throw new Exception($"Произошла ошибка при получении объекта строительства: {ex.Message}", ex);
        }
    }

    public async Task<BuildingObject> UpdateBuildingObject(BuildingObject buildingObject)
    {
        try
        {
            BuildingObject? existingBuildingObject = await _dbContext.BuildingObjects
                .FirstOrDefaultAsync(x => x.Id == buildingObject.Id);

            if (existingBuildingObject == null)
            {
                throw new Exception("Объект строительства не найден");
            }

            _dbContext.BuildingObjects.Update(existingBuildingObject);
            await _dbContext.SaveChangesAsync();
            return existingBuildingObject;
        }
        catch (Exception ex)
        {
            throw new Exception($"Произошла ошибка при обновлении объекта строительства: {ex.Message}", ex);
        }
    }
}
