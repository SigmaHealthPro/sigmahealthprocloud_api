using BAL.Constant;
using BAL.RequestModels;

namespace BAL.Repository
{
    public interface IInventoryRepository : IGenericRepository<InventoryModel>
    {
        public Task<PaginationModel<InventoryModel>> GetAllAsync(SearchInventoryParams search);
    }
}
