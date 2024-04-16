using BAL.Constant;
using BAL.Responses;

namespace BAL.Interfaces
{
    public interface IInventoryService
    {
        //Task<PaginationModel<FacilitySearchResponse>> FacilitySearch(FacilitySearchRequest request);
        Task<ApiResponse<InventoryDetailsResponse>> GetInventoryDetailsById(Guid inventoryId);
    }
}
