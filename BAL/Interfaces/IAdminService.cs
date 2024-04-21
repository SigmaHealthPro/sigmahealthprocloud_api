using BAL.Constant;
using BAL.Request;
using BAL.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IAdminService
    {
        Task<ApiResponse<string>> UpdateFacilityStatus(Guid facilityId);
        Task<ApiResponse<string>> UpdateOrganizationStatus(Guid organizationId);
        Task<ApiResponse<string>> UpdateLovMasterStatus(Guid lovMasterId);
        Task<ApiResponse<LovMasterResponse>> GetLovMasterData(GetLovMasterRequest request);
        Task<ApiResponse<List<string>>> GetDistinctLovTypes();

    }
}
