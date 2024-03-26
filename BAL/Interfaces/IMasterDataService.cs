using BAL.Constant;
using BAL.Request;
using BAL.Responses;
using Data.Models;


namespace BAL.Interfaces
{
    public interface IMasterDataService
    {

        Task<ApiResponse<GenerateNextIdResponse>> GenerateNextId(GenerateNextIdRequest request);
        Task<ApiResponse<GenerateNextIdResponse>> GetListOfPatientDuplicateData();
        Task<ApiResponse<GenerateNextIdResponse>> GetListOfPatientNewData();
        Task<ApiResponse<BestMatchResponse>> FindBestMatchPercentage(BestMatchRequest request);
    }
}
