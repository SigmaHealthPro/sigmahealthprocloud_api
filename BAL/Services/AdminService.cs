using BAL.Interfaces;
using Data.Models;
using Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.Constant;
using BAL.Request;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using BAL.Responses;

namespace BAL.Services
{
    public class AdminService : DataAccessProvider<BusinessConfiguration>, IAdminService
    {
        private readonly SigmaproIisContext _dbContext;
        private readonly SigmaproIisContextUdf _dbContextudf;
        private readonly ILogger<BusinessConfiguration> _logger;
        private readonly string _correlationId = string.Empty;
        public AdminService(SigmaproIisContext dbContext, ILogger<BusinessConfiguration> logger, SigmaproIisContextUdf dbContextudf) : base(dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
            _dbContextudf = dbContextudf;
        }
        public async Task<ApiResponse<string>> UpdateFacilityStatus(Guid facilityId)
        {
            try
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var existingFacilityData = await _dbContext.Facilities.FindAsync(facilityId);

                        if (existingFacilityData == null)
                        {
                            transaction.Rollback();
                            return ApiResponse<string>.Fail("Data not found.");
                        }
                        if (existingFacilityData != null)
                        {
                            if (existingFacilityData.Status == true)
                            {
                                existingFacilityData.Status = false;
                            }
                            else
                            {
                                existingFacilityData.Status = true;
                            }
                        }

                        await _dbContext.SaveChangesAsync();

                        transaction.Commit();
                        return ApiResponse<string>.Success(null, "Status updated successfully.");
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_correlationId} - Exception occurred in Method: {nameof(UpdateFacilityStatus)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while updating lov master status.");
            }
        }

        public async Task<ApiResponse<string>> UpdateOrganizationStatus(Guid organizationId)
        {
            try
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var existingOrganizationData = await _dbContext.Facilities.FindAsync(organizationId);

                        if (existingOrganizationData == null)
                        {
                            transaction.Rollback();
                            return ApiResponse<string>.Fail("Data not found.");
                        }
                        if (existingOrganizationData != null)
                        {
                            if (existingOrganizationData.Status == true)
                            {
                                existingOrganizationData.Status = false;
                            }
                            else
                            {
                                existingOrganizationData.Status = true;
                            }
                        }

                        await _dbContext.SaveChangesAsync();

                        transaction.Commit();
                        return ApiResponse<string>.Success(null, "Status updated successfully.");
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_correlationId} - Exception occurred in Method: {nameof(UpdateOrganizationStatus)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while updating lov master status.");
            }
        }
        public async Task<ApiResponse<string>> UpdateLovMasterStatus(Guid lovMasterId)
        {
            try
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var existingLovMasterData = await _dbContext.LovMasters.FindAsync(lovMasterId);

                        if (existingLovMasterData == null)
                        {
                            transaction.Rollback();
                            return ApiResponse<string>.Fail("Data not found.");
                        }
                        if(existingLovMasterData != null)
                        {
                            if (existingLovMasterData.Status==true)
                            {
                                existingLovMasterData.Status=false;
                            }
                            else
                            {
                                existingLovMasterData.Status = true;
                            }
                        }

                        await _dbContext.SaveChangesAsync();

                        transaction.Commit();
                        return ApiResponse<string>.Success(null, "Status updated successfully.");
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_correlationId} - Exception occurred in Method: {nameof(UpdateLovMasterStatus)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while updating lov master status.");
            }
        }

        public async Task<ApiResponse<LovMasterResponse>> GetLovMasterData(GetLovMasterRequest request)
        {
            try
            {
                IQueryable<LovMaster> query;

                if (string.IsNullOrWhiteSpace(request.LovType))
                {
                   
                    query = _dbContext.Set<LovMaster>()
                                     .OrderByDescending(lov => lov.CreatedDate)
                                     .Take(50);
                }
                else
                {
                    
                    query = _dbContext.Set<LovMaster>()
                                     .Where(lov => lov.LovType == request.LovType);
                }

                var result = await query.ToListAsync();

                var response = result.Select(lov => new LovMasterResponse
                {
                    Id = lov.Id,
                    ReferenceId = lov.ReferenceId,
                    Key = lov.Key,
                    Value = lov.Value,
                    LovType = lov.LovType,
                    CreatedDate = lov.CreatedDate,
                    UpdatedDate = lov.UpdatedDate,
                    CreatedBy = lov.CreatedBy,
                    UpdatedBy = lov.UpdatedBy,
                    Isdelete = lov.Isdelete,
                    LongDescription = lov.LongDescription,
                    Status = lov.Status
                }).ToList();

                return ApiResponse<LovMasterResponse>.SuccessList(response, "LovMaster data fetched successfully!");
            }
            catch (DbException ex)
            {
                _logger.LogError($"CorrelationId: {_correlationId} - Database exception: {ex.Message}, Stack trace: {ex.StackTrace}");
                return ApiResponse<LovMasterResponse>.Fail($"A database error occurred while fetching LovMaster data: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"CorrelationId: {_correlationId} - Exception occurred in GetLovMasterData: {ex.Message}, Stack trace: {ex.StackTrace}");
                return ApiResponse<LovMasterResponse>.Fail($"An error occurred while fetching LovMaster data: {ex.Message}");
            }
        }
        public async Task<ApiResponse<List<string>>> GetDistinctLovTypes()
        {
            try
            {
                var distinctLovTypes = await _dbContext.Set<LovMaster>()
                    .Select(lov => lov.LovType)
                    .Where(type => !string.IsNullOrEmpty(type))
                    .Distinct()
                    .ToListAsync();

                return ApiResponse<List<string>>.Success(distinctLovTypes, "Distinct LovTypes fetched successfully!");
            }
            catch (DbException ex)
            {
                _logger.LogError($"CorrelationId: {_correlationId} - Database exception: {ex.Message}, Stack trace: {ex.StackTrace}");
                return ApiResponse<List<string>>.Fail($"A database error occurred while fetching distinct LovTypes: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"CorrelationId: {_correlationId} - Exception occurred in GetDistinctLovTypes: {ex.Message}, Stack trace: {ex.StackTrace}");
                return ApiResponse<List<string>>.Fail($"An error occurred while fetching distinct LovTypes: {ex.Message}");
            }
        }

    }
}
