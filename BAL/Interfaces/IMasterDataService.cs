using BAL.Constant;
using BAL.Request;
using BAL.Responses;
using Data.Models;
using System.Threading.Tasks;


namespace BAL.Interfaces
{
    public interface IMasterDataService
    {

        Task<ApiResponse<GenerateNextIdResponse>> GenerateNextId(GenerateNextIdRequest request);
        Task<ApiResponse<PatientNewRecord>> GetListOfPatientNewData();
        Task<ApiResponse<PatientNewRecord>> GetListOfPatientNewDataById(Guid id);
        Task<ApiResponse<PatientDuplicateRecord>> GetListOfPatientDuplicateData();
        Task<ApiResponse<PatientDuplicateRecord>> GetListOfPatientDuplicateDataById(Guid id);
        Task<ApiResponse<BestMatchResponse>> FindBestMatchPercentage(BestMatchRequest request);

    }
}
