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
       
        public async Task<PaginationModel<InventoryModel>> GetAllAsync(SearchInventoryParams search)
        {
            var inventoryModelList = new List<InventoryModel>();

            var query = (
                        from inventorys in context.Inventories
                        join facility in context.Facilities on inventorys.FacilityId equals facility.Id
                        join site in context.Sites on inventorys.SiteId equals site.Id
                        join product in context.Products on inventorys.ProductId equals product.Id
                        //where (
                        //   (string.IsNullOrWhiteSpace(search.keyword) ||
                        //   // inventorys.InventoryId.ToString().IndexOf(search.keyword.ToString()) >= 0 ||
                        //    inventorys.InventoryDate.ToString().IndexOf(search.keyword.ToLower()) >= 0 ||
                        //    facility.FacilityName.ToLower().IndexOf(search.keyword.ToLower()) >= 0 ||
                        //    site.SiteName.ToLower().IndexOf(search.keyword.ToLower()) >= 0 ||
                        //    product.ProductName.ToLower().IndexOf(search.keyword.ToLower()) >= 0)
                        //    &&
                        //    (string.IsNullOrWhiteSpace(search.InventoryId.ToString()) || inventorys.InventoryId.ToString().IndexOf(search.InventoryId.ToString()) >= 0)
                        //    &&
                        //    (string.IsNullOrWhiteSpace(search.InventoryDate.ToString()) || inventorys.InventoryDate.ToString().ToLower().IndexOf(search.InventoryDate.ToString().ToLower()) >= 0)

                        //    &&
                        //    (string.IsNullOrWhiteSpace(search.FacilityName) || facility.FacilityName.ToLower().IndexOf(search.FacilityName.ToLower()) >= 0)
                        //     &&
                        //    (string.IsNullOrWhiteSpace(search.SiteName) || site.SiteName.IndexOf(search.SiteName) >= 0)
                        //    &&
                        //    (string.IsNullOrWhiteSpace(search.ProductName) || product.ProductName.IndexOf(search.SiteName) >= 0)
                        //   &&
                        //         inventorys.Isdelete == false
                        //    )

                        select new InventoryModel
                        {
                            Id = inventorys.Id,
                            InventoryId = inventorys.InventoryId,
                            InventoryDate = inventorys.InventoryDate,
                            Facility = facility.FacilityName,
                            FacilityId=facility.Id,
                            SiteId=site.Id,
                            Site = site.SiteName,
                            ProductId = product.Id,
                            Product = product.ProductName,
                            TempRecorded = inventorys.TempRecorded,
                            UnitOfTemp = inventorys.UnitOfTemp,
                            ExpirationDate = inventorys.ExpirationDate,
                            QuantityRemaining = inventorys.QuantityRemaining,

                        });

            Task.WhenAll();

            long? totalRows = query.Count();
            var response = query.Skip(search.pagesize * (search.pagenumber - 1)).Take(search.pagesize).ToList();
            return PaginationHelper.Paginate(response, search.pagenumber, search.pagesize, Convert.ToInt32(totalRows));
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
                inventory.InventoryId = inventoryModel.InventoryId;
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


        public async Task<ApiResponse<InventoryDetailsResponse>> GetInventoryDetailsById(Guid inventoryId)
        {
            try
            {
                var inventory = await context.Inventories.FindAsync(inventoryId);
                if (inventory != null)
                {
                    var facility = await context.Facilities.FindAsync(inventory.FacilityId);
                    var site = await context.Sites.FindAsync(inventory.SiteId);
                    var product = await context.Products.FindAsync(inventory.ProductId);

                    var inventoryDetails = InventoryDetailsResponse.FromInventoryEntity(inventory, facility,site,product);

                    return ApiResponse<InventoryDetailsResponse>.Success(inventoryDetails, "Inventory details fetched successfully.");
                }

                _logger.LogError($"Inventory with ID {inventoryId} not found.");
                return ApiResponse<InventoryDetailsResponse>.Fail("Inventory not found.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"An error occurred: {exp.Message}, Stack trace: {exp.StackTrace}");
                return ApiResponse<InventoryDetailsResponse>.Fail("An error occurred while fetching facility details.");
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

                    var siteEntity = await context.Set<Site>().FindAsync(entity.SiteId);
                    siteEntity.Isdelete = true;
                    context.Sites.Update(siteEntity);

                    var productEntity = await context.Set<Product>().FindAsync(entity.ProductId);
                    productEntity.Isdelete = true;
                    context.Products.Update(productEntity);

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
