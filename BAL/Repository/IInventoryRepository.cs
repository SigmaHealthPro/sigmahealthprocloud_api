using BAL.Constant;
using BAL.RequestModels;

namespace BAL.Repository
{
    public interface IInventoryRepository : IGenericRepository<InventoryModel>
    {
        public Task<IEnumerable<InventoryModel>> GetAllAsync(SearchInventoryParams search);
    }
}
