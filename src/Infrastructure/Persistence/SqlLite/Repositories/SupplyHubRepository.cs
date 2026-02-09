using Application.Abstractions;
using Domain.Entities.ConstructionProject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence.SqlLite.DbContexts;


namespace Persistence.SqlLite.Repositories;

public class SupplyHubRepository : ISupplyHubAbstractions
{
    private readonly ConstructionCompanyDbContext _dbContext;

    public SupplyHubRepository(ConstructionCompanyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SupplyHub> CreateSupplyHub(SupplyHub supplyHub)
    {
        try
        {
            await _dbContext.SupplyHubs.AddAsync(supplyHub);
            await _dbContext.SaveChangesAsync();
            return supplyHub;
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка при создании SupplyHub: {ex.Message}", ex);
        }
    }

    public async Task<SupplyHub?> GetSupplyHubById(Guid id)
    {
        try
        {
            return await _dbContext.SupplyHubs
                .Include(h => h.Parent)
                .Include(h => h.Children)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка при получении SupplyHub: {ex.Message}", ex);
        }
    }

    public async Task<List<SupplyHub>> GetRootSupplyHubsWithTree()
    {
        return await _dbContext.SupplyHubs
            .Where(h => h.ParentId == null)
            .Include(h => h.Children)
                .ThenInclude(h => h.Children)
                    .ThenInclude(h => h.Children)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<SupplyHub?> GetSupplyHubWithSubTree(Guid id)
    {
        try
        {
            return await _dbContext.SupplyHubs
                .Include(h => h.Children)
                    .ThenInclude(h => h.Children)
                        .ThenInclude(h => h.Children)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка при получении поддерева SupplyHub: {ex.Message}", ex);
        }
    }

    public async Task<List<SupplyHub>> GetAllSupplyHubs()
    {
        try
        {
            return await _dbContext.SupplyHubs
                .Include(h => h.Parent)
                .Include(h => h.Children)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка при получении всех SupplyHub: {ex.Message}", ex);
        }
    }

    public async Task<SupplyHub> UpdateSupplyHub(SupplyHub supplyHub)
    {
        try
        {
            SupplyHub? existing = await _dbContext.SupplyHubs
                .FirstOrDefaultAsync(x => x.Id == supplyHub.Id);

            if (existing == null)
                throw new Exception("SupplyHub не найден");

            _dbContext.Entry(existing).CurrentValues.SetValues(supplyHub);
            await _dbContext.SaveChangesAsync();
            return existing;
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка при обновлении SupplyHub: {ex.Message}", ex);
        }
    }

    public async Task DeleteSupplyHub(Guid id)
    {
        try
        {
            SupplyHub? hub = await _dbContext.SupplyHubs
                .Include(h => h.Children)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (hub == null)
                return;

            if (hub.Children.Any())
                throw new Exception("Нельзя удалить SupplyHub с дочерними узлами");

            _dbContext.SupplyHubs.Remove(hub);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка при удалении SupplyHub: {ex.Message}", ex);
        }
    }

    public async Task ChangeParent(Guid supplyHubId, Guid? newParentId)
    {
        await using IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            SupplyHub? hub = await _dbContext.SupplyHubs
                .Include(h => h.Parent)
                .FirstOrDefaultAsync(x => x.Id == supplyHubId);

            if (hub is null)
            {
                throw new KeyNotFoundException("SupplyHub с указанным Id не найден");
            }

            SupplyHub? newParent = null;
            if (newParentId.HasValue)
            {
                newParent = await _dbContext.SupplyHubs
                    .FirstOrDefaultAsync(x => x.Id == newParentId.Value);

                if (newParent is null)
                {
                    throw new KeyNotFoundException("Новый родительский SupplyHub не найден");
                }

                if (SupplyHub.WouldCreateCycle(hub, newParent))
                {
                    throw new InvalidOperationException("Перемещение приведёт к созданию цикла в иерархии");
                }
            }

            hub.ChangeParent(newParent);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception($"Ошибка при смене родителя: {ex.Message}", ex);
        }
    }
}
