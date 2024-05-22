using BAL.Interfaces;
using Data.Models;
using Data;
using Microsoft.Extensions.Logging;
using BAL.Constant;
using BAL.Request;
using Microsoft.EntityFrameworkCore;
using BAL.Responses;
using Npgsql;
using NpgsqlTypes;
using System.Reflection;
using System.Text;
using System.Diagnostics;
using FuzzySharp;

namespace BAL.Services
{
    public class MasterDataService : DataAccessProvider<BusinessConfiguration>, IMasterDataService
    {
        private readonly SigmaproIisContext _dbContext;
        private readonly SigmaproIisContextUdf _dbContextudf;
        private readonly ILogger<MasterDataService> _logger;
        private readonly string _corelationId = string.Empty;
        public MasterDataService(SigmaproIisContext dbContext, ILogger<MasterDataService> logger, SigmaproIisContextUdf dbContextudf) : base(dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
            _dbContextudf = dbContextudf;
        }

        #region Public Methods
        public async Task<ApiResponse<GenerateNextIdResponse>> GenerateNextId(GenerateNextIdRequest request)
        {
            try
            {
                   
            
                if (String.IsNullOrEmpty(request.output_table_name) || String.IsNullOrEmpty(request.output_column_name) || String.IsNullOrEmpty(request.start_column_name) || String.IsNullOrEmpty(request.suffix_column_name))
                {
                    _logger.LogError($"CorelationId: {_corelationId} - Invalid Request Data");

                    throw new ArgumentException("Invalid Request Data.");
                }

                string sql = $"SELECT * FROM public.udf_generate_next_id('{request.output_table_name}','{request.suffix_column_name}','{request.start_column_name}', '{request.output_column_name}')";

                var result = await _dbContextudf.GenerateNextIdResults.FromSqlRaw(sql).FirstOrDefaultAsync();

                if (result != null)
                {
                    return ApiResponse<GenerateNextIdResponse>.Success(result, $"Next {request.output_column_name} generated successfully.");
                }
                else
                {
                    return ApiResponse<GenerateNextIdResponse>.Fail($"{request.output_column_name} generation failed.");
                }
            }
            catch (Exception exp)
            {

                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred during {request.output_column_name} generation in Method: {nameof(GenerateNextId)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                throw new Exception(exp?.Message);

            }
        }
        public async Task<ApiResponse<PatientDuplicateRecord>> GetListOfPatientDuplicateData()
        {
            try
            {
                var duplicatePatientData = await _dbContext.PatientDuplicateRecords.ToListAsync();

                if (duplicatePatientData == null || !duplicatePatientData.Any())
                {
                    return ApiResponse<PatientDuplicateRecord>.Fail("No data found.");
                }

                return ApiResponse<PatientDuplicateRecord>.SuccessList(duplicatePatientData);
            }
            catch (Exception exp)
            {
                _logger.LogError($"An error occurred: {exp.Message}, Stack trace: {exp.StackTrace}");
                return ApiResponse<PatientDuplicateRecord>.Fail("An error occurred while fetching duplicate patient data.");
            }

        }

        public async Task<ApiResponse<PatientDuplicateRecord>> GetListOfPatientDuplicateDataById(Guid id)
        {
            try
            {
                var duplicatePatientData = await _dbContext.PatientDuplicateRecords.FindAsync(id);

                if (duplicatePatientData == null)
                {
                    return ApiResponse<PatientDuplicateRecord>.Fail("No data found.");
                }

                var patientDuplicateRecordDetails = new PatientDuplicateRecord
                {
                   
                    Id = duplicatePatientData.Id,
                    PersonType = duplicatePatientData.PersonType,
                    FirstName = duplicatePatientData.FirstName,
                    LastName = duplicatePatientData.LastName,
                    Gender = duplicatePatientData.Gender,
                    CreatedDate = duplicatePatientData.CreatedDate,
                    UpdatedDate = duplicatePatientData.UpdatedDate,
                    CreatedBy = duplicatePatientData.CreatedBy,
                    UpdatedBy = duplicatePatientData.UpdatedBy,
                    DateOfBirth = duplicatePatientData.DateOfBirth,
                    MiddleName = duplicatePatientData.MiddleName,
                    MotherFirstName = duplicatePatientData.MotherFirstName,
                    MotherLastName = duplicatePatientData.MotherLastName,
                    MotherMaidenLastName = duplicatePatientData.MotherMaidenLastName,
                    BirthOrder = duplicatePatientData.BirthOrder,
                    BirthStateId = duplicatePatientData.BirthStateId
                };

                return ApiResponse<PatientDuplicateRecord>.Success(patientDuplicateRecordDetails);
            }
            catch (Exception exp)
            {
                _logger.LogError($"An error occurred: {exp.Message}, Stack trace: {exp.StackTrace}");
                return ApiResponse<PatientDuplicateRecord>.Fail("An error occurred while fetching duplicate patient data.");
            }
        }

        public async Task<ApiResponse<PatientNewRecord>> GetListOfPatientNewData()
        {
            try
            {
                var patientNewData = await _dbContext.PatientNewRecords.ToListAsync();

                if (patientNewData == null || !patientNewData.Any())
                {
                    return ApiResponse<PatientNewRecord>.Fail("No data found.");
                }

                return ApiResponse<PatientNewRecord>.SuccessList(patientNewData);
            }
            catch (Exception exp)
            {
                _logger.LogError($"An error occurred: {exp.Message}, Stack trace: {exp.StackTrace}");
                return ApiResponse<PatientNewRecord>.Fail("An error occurred while fetching details.");
            }
        }
        public async Task<ApiResponse<PatientNewRecord>> GetListOfPatientNewDataById(Guid id)
        {
            try
            {
                var patientNewData = await _dbContext.PatientDuplicateRecords.FindAsync(id);

                if (patientNewData == null)
                {
                    return ApiResponse<PatientNewRecord>.Fail("No data found.");
                }

                var patientNewDataDetails = new PatientNewRecord
                {
                    Id = patientNewData.Id,
                    PersonType = patientNewData.PersonType,
                    FirstName = patientNewData.FirstName,
                    LastName = patientNewData.LastName,
                    Gender = patientNewData.Gender,
                    CreatedDate = patientNewData.CreatedDate,
                    UpdatedDate = patientNewData.UpdatedDate,
                    CreatedBy = patientNewData.CreatedBy,
                    UpdatedBy = patientNewData.UpdatedBy,
                    DateOfBirth = patientNewData.DateOfBirth,
                    MiddleName = patientNewData.MiddleName,
                    MotherFirstName = patientNewData.MotherFirstName,
                    MotherLastName = patientNewData.MotherLastName,
                    MotherMaidenLastName = patientNewData.MotherMaidenLastName,
                    BirthOrder = patientNewData.BirthOrder,
                    BirthStateId = patientNewData.BirthStateId
                };

                return ApiResponse<PatientNewRecord>.Success(patientNewDataDetails);
            }
            catch (Exception exp)
            {
                _logger.LogError($"An error occurred: {exp.Message}, Stack trace: {exp.StackTrace}");
                return ApiResponse<PatientNewRecord>.Fail("An error occurred while fetching details.");
            }
        }

        public async Task<ApiResponse<BestMatchResponse>> FindBestMatchPercentage(BestMatchRequest request)
        {
            try
            {
                var dataComposite = CreateCompositeString(request.Data);
                var targetComposites = await Task.WhenAll(request.TargetData.Select(obj => Task.Run(() => CreateCompositeString(obj))));

                var bestMatch = FuzzySharp.Process.ExtractOne(dataComposite, targetComposites);

                double matchScore = bestMatch?.Score ?? 0.0;

                if (matchScore == 0.0)
                {
                    return ApiResponse<BestMatchResponse>.Success(new BestMatchResponse { MatchScore = 0.0 }, "No match found.");
                }
                else
                {
                    return ApiResponse<BestMatchResponse>.Success(new BestMatchResponse { MatchScore = matchScore }, "Best match percentage calculated successfully.");
                }
            }
            catch (Exception ex)
            {
                return ApiResponse<BestMatchResponse>.Fail($"An error occurred: {ex.Message}");
            }
        }




        #endregion

        #region Private Methods

        private string CreateCompositeString(object model)
        {
            if (model == null)
                return string.Empty;

            var compositeString = string.Join(" ", model.GetType().GetProperties().Select(p => p.GetValue(model)?.ToString()));
            return compositeString.Trim();
        }
        private string CreateCompositeString1(object model)
        {
            StringBuilder compositeString = new StringBuilder();

            PropertyInfo[] properties = model.GetType().GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                object value = prop.GetValue(model);
                if (value != null)
                {
                    compositeString.Append(value.ToString());
                    compositeString.Append(" ");
                }
            }

            return compositeString.ToString().Trim();
        }

        #endregion
    }
}
