using BAL.Repository;
using BAL.RequestModels;
using BAL.Constant;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using BAL.Responses;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using BAL.Interfaces;
using System.Reflection;
using BAL.Pagination;

namespace BAL.Implementation
{
    public class InventoryRepository : IGenericRepository<InventoryModel>, IInventoryRepository
        {
        private SigmaproIisContext context;
        private ILogger<UnitOfWork> _logger;
        private readonly string _corelationId = string.Empty;
        public InventoryRepository(SigmaproIisContext _context, ILogger<UnitOfWork> logger)
        {
            context = _context;
            _logger = logger;
        }
        public async Task<IEnumerable<InventoryModel>> Find(Expression<Func<InventoryModel, bool>> predicate)
        {
            return (IEnumerable<InventoryModel>)await context.Set<InventoryModel>().FindAsync(predicate);
        }
        public async Task<IEnumerable<InventoryModel>> GetAllAsync()
        {
            return await context.Set<InventoryModel>().ToListAsync();
        }

        public async Task<IEnumerable<InventoryModel>> GetAllAsync(SearchInventoryParams search)
        {
            var inventoryModelList = new List<InventoryModel>();

            var query = (
                        from inventorys in context.Inventories
                        join facility in context.Facilities on inventorys.FacilityId equals facility.Id
                        join site in context.Sites on inventorys.SiteId equals site.Id

                        select new InventoryModel
                        {
                            Id = inventorys.Id,
                            InventoryId = inventorys.InventoryId,
                            InventoryDate = inventorys.InventoryDate,
                            Facility = facility.FacilityName,
                            Site = site.SiteName,
                        });
            var inventoryList = await query.ToPagedListAsync(search.pagenumber, search.pagesize);
            inventoryModelList.AddRange(inventoryList);

            return inventoryModelList;
        }

        public Task<InventoryModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    
        public async Task<ApiResponse<string>> InsertAsync(InventoryModel inventoryModel)
        {
            try
            {
                var inventory = new Inventory();

                inventory.InventoryDate = inventoryModel.InventoryDate.HasValue ? DateTime.SpecifyKind(inventoryModel.InventoryDate.Value, DateTimeKind.Utc) : DateTime.UtcNow;
                inventory.QuantityRemaining = inventoryModel.QuantityRemaining;
                inventory.ExpirationDate = inventoryModel.ExpirationDate.HasValue ? DateTime.SpecifyKind(inventoryModel.ExpirationDate.Value, DateTimeKind.Utc) : DateTime.UtcNow;
                inventory.TempRecorded = inventoryModel.TempRecorded;
                inventory.UnitOfTemp = inventoryModel.UnitOfTemp;
                inventory.FacilityId = new Guid(inventoryModel.Facility);
                inventory.SiteId = new Guid(inventoryModel.Site);
                inventory.ProductId = new Guid(inventoryModel.Product);
                inventory.UserId = new Guid(inventoryModel.User);

                inventory.CreatedDate = inventoryModel.CreatedDate;
                inventory.UpdatedDate = inventoryModel.UpdatedDate;
                inventory.CreatedBy = inventoryModel.CreatedBy;
                inventory.UpdatedBy = inventoryModel.UpdatedBy;
                
                if (!inventoryModel.IsEdit)
                {
                    context.Inventories.Add(inventory);
                }
                else
                {
                    inventory.Id = inventoryModel.Id;
                    context.Inventories.Update(inventory);
                }
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(inventory.Id.ToString(), "provider created successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while creating the Provider.");
            }
        }

        public Task<ApiResponse<string>> UpdateAsync(InventoryModel entity)
        {
            throw new NotImplementedException();
        }

    
        public async Task<ApiResponse<string>> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await context.Set<Inventory>().FindAsync(id);
                if (entity != null)
                {
                    entity.Isdelete = true;
                    context.Inventories.Update(entity);

                    var facilityEntity = await context.Set<Facility>().FindAsync(entity.FacilityId);
                    facilityEntity.Isdelete = true;
                    context.Facilities.Update(facilityEntity);

                    await context.SaveChangesAsync();
                    return ApiResponse<string>.Success(id.ToString(), "Inventory deleted successfully.");
                }

                return ApiResponse<string>.Fail("Inventory with the given ID not found.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(DeleteAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("Inventorys with the given ID not found.");
            }
        }

    }
}
