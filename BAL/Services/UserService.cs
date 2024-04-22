using BAL.Interfaces;
using Data.Models;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using BAL.Constant;
using BAL.Request;
using BAL.Responses;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Linq.Expressions;
using Serilog.Core;

namespace BAL.Services
{
    public class UserService : DataAccessProvider<BusinessConfiguration>, IUserService
    {
        private readonly SigmaproIisContext _dbContext;
        private readonly SigmaproIisContextUdf _dbContextudf;
        private readonly ILogger<BusinessConfiguration> _logger;
        private readonly string _correlationId = string.Empty;
        public UserService(SigmaproIisContext dbContext, ILogger<BusinessConfiguration> logger, SigmaproIisContextUdf dbContextudf) : base(dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
            _dbContextudf = dbContextudf;
        }
        public async Task<ApiResponse<UserDetailsResponse>> GetUsers(GetDataByCountRequest requestObject)
        {
            try
            {
                var query = _dbContext.Set<User>().AsQueryable();

                Expression<Func<User, bool>> whereCondition = null;

                //query = query.Include(a => a.County)
                //              .Include(a => a.Country)
                //              .Include(a => a.State)
                //              .Include(a => a.City);

                if (!string.IsNullOrWhiteSpace(requestObject.identifier))
                {
                    whereCondition = BuildWhereCondition(requestObject.identifier);
                    query = query.Where(whereCondition);
                }

                var resultQuery = query.Select(a => new UserDetailsResponse
                {
                    Id = a.Id,
                    CreatedBy = a.CreatedBy,
                    CreatedDate= a.CreatedDate,
                    Designation = a.Designation,
                    Status = a.Status,
                    Gender = a.Gender,
                    ImageUrl= a.ImageUrl,
                    Isdelete = a.Isdelete,
                    PersonId = a.PersonId,
                    SequenceId = a.SequenceId,
                    UpdatedBy = a.UpdatedBy,
                    UpdatedDate = a.UpdatedDate,
                    UserId = a.UserId,
                    UserType = a.UserType
                    
                })
                .OrderBy(a => a.Id);


                var result = await resultQuery.ToListAsync();

                result = result.Take(requestObject.RecordCount ?? 500).ToList();

                return ApiResponse<UserDetailsResponse>.SuccessList(result, "Users fetched successfully!");
            }
            catch (DbException ex)
            {
                _logger.LogError($"CorrelationId: {_correlationId} - Database exception: {ex.Message}, Stack trace: {ex.StackTrace}");
                return ApiResponse<UserDetailsResponse>.Fail($"A database error occurred while fetching Users: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"CorrelationId: {_correlationId} - Exception occurred in GetUsers: {ex.Message}, Stack trace: {ex.StackTrace}");
                return ApiResponse<UserDetailsResponse>.Fail($"An error occurred while fetching Users: {ex.Message}");
            }
        }
        public async Task<ApiResponse<UserDetailsResponse>> GetUserById(Guid Id)
        {
            try
            {
                var user = await _dbContext.Users.FindAsync(Id);

                if (user != null)
                {
                    var userDetails = UserDetailsResponse.FromUser(user);

                    return ApiResponse<UserDetailsResponse>.Success(userDetails, "User details fetched successfully.");
                }
                _logger.LogError($"User with ID {Id} not found.");
                return ApiResponse<UserDetailsResponse>.Fail("User not found.");
            }
            catch (DbException ex)
            {
                _logger.LogError($"CorrelationId: {_correlationId} - Database exception: {ex.Message}, Stack trace: {ex.StackTrace}");
                return ApiResponse<UserDetailsResponse>.Fail($"A database error occurred while fetching Users: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"CorrelationId: {_correlationId} - Exception occurred in GetUsers: {ex.Message}, Stack trace: {ex.StackTrace}");
                return ApiResponse<UserDetailsResponse>.Fail($"An error occurred while fetching Users: {ex.Message}");
            }
        }
        public async Task<ApiResponse<string>> CreateUser(CreateUserRequest userRequest)
        {
            try
            {


                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {

                        var newUser = new User
                        {
                            
                            CreatedBy = userRequest.CreatedBy,
                            CreatedDate = userRequest.CreatedDate,
                            Designation = userRequest.Designation,
                            Status = userRequest.Status,
                            Gender = userRequest.Gender,
                            ImageUrl = userRequest.ImageUrl,
                            Isdelete = userRequest.Isdelete,
                            PersonId = userRequest.PersonId,
                            SequenceId = userRequest.SequenceId,
                            UpdatedBy = userRequest.UpdatedBy,
                            UpdatedDate = userRequest.UpdatedDate,
                            UserId = userRequest.UserId,
                            UserType = userRequest.UserType
                        };

                        _dbContext.Users.Add(newUser);

                        await _dbContext.SaveChangesAsync();

                        transaction.Commit();

                        return ApiResponse<string>.Success(null, $"Entity address inserted successfully.");
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
                _logger.LogError($"CorelationId: {_correlationId} - Exception occurred in Method: {nameof(CreateUser)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while creating user.");
            }
        }
        public async Task<ApiResponse<string>> UpdateUser(UpdateUserRequest userRequest)
        {
            try
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var existingUser = await _dbContext.Users.FindAsync(userRequest.Id);

                        if (existingUser == null)
                        {
                            transaction.Rollback();
                            return ApiResponse<string>.Fail("User not found.");
                        }

                        existingUser.Designation = userRequest.Designation ?? existingUser.CreatedBy;
                        existingUser.Gender = userRequest.Gender ?? existingUser.Gender;
                        existingUser.ImageUrl = userRequest.ImageUrl ?? existingUser.Gender ;
                        existingUser.PersonId = userRequest.PersonId  ?? existingUser.PersonId ;
                        
                        existingUser.UpdatedBy = userRequest.UpdatedBy  ?? existingUser.UpdatedBy ;
                        existingUser.UpdatedDate = userRequest.UpdatedDate  ?? existingUser.UpdatedDate ;
                        existingUser.UserId = userRequest.UserId  ?? existingUser.UserId;
                        existingUser.UserType = userRequest.UserType  ?? existingUser.UserType ;
                        existingUser.Password = userRequest.Password  ?? existingUser.Password ;
                       

                        await _dbContext.SaveChangesAsync();

                        transaction.Commit();
                        return ApiResponse<string>.Success(null, "User updated successfully.");
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
                _logger.LogError($"CorelationId: {_correlationId} - Exception occurred in Method: {nameof(UpdateUser)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while updating User.");
            }
        }
        public async Task<ApiResponse<string>> DeleteUser(Guid Id)
        {
            try
            {
                // using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                //{
                try
                {
                    var user = await _dbContext.Users.FindAsync(Id);

                    if (user == null)
                    {
                        //transaction.Rollback();
                        return ApiResponse<string>.Fail("User not found.");
                    }

                    user.Isdelete = true;
                    await _dbContext.SaveChangesAsync();

                    //transaction.Commit();

                    return ApiResponse<string>.Success(null, "User Deleted successfully.");
                }
                catch (Exception ex)
                {
                    //transaction.Rollback();
                    _logger.LogError($"CorrelationId: {_correlationId} - Exception occurred while deleting user: {ex.Message}, Stack trace: {ex.StackTrace}");
                    return ApiResponse<string>.Fail($"An error occurred while deleting user: {ex.Message}");
                }
                //}
            }
            catch (Exception ex)
            {
                _logger.LogError($"CorrelationId: {_correlationId} - Exception occurred while deleting user: {ex.Message}, Stack trace: {ex.StackTrace}");
                return ApiResponse<string>.Fail($"An error occurred while deleting user: {ex.Message}");
            }
        }
        public async Task<ApiResponse<string>> UpdateUserStatus(UpdateUserStatusRequest userRequest)
        {
            try
            {
                // using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                //{
                try
                {
                    var user = await _dbContext.Users.FindAsync(userRequest.Id);

                    if (user == null)
                    {
                        //transaction.Rollback();
                        return ApiResponse<string>.Fail("User not found.");
                    }

                    user.Status = userRequest.Status;
                    await _dbContext.SaveChangesAsync();

                    //transaction.Commit();

                    return ApiResponse<string>.Success(null, "User status updated successfully.");
                }
                catch (Exception ex)
                {
                    //transaction.Rollback();
                    _logger.LogError($"CorrelationId: {_correlationId} - Exception occurred while updating user: {ex.Message}, Stack trace: {ex.StackTrace}");
                    return ApiResponse<string>.Fail($"An error occurred while updating user: {ex.Message}");
                }
                //}
            }
            catch (Exception ex)
            {
                _logger.LogError($"CorrelationId: {_correlationId} - Exception occurred while updating user: {ex.Message}, Stack trace: {ex.StackTrace}");
                return ApiResponse<string>.Fail($"An error occurred while updating user: {ex.Message}");
            }
        }
        private Expression<Func<User, bool>> BuildWhereCondition(string identifier)
        {
            Expression<Func<User, bool>> whereCondition = null;

            var normalizedIdentifier = identifier.ToLower();
            var formattedIdentifier = $"%{normalizedIdentifier}%";

            whereCondition = CombineConditions(whereCondition, a => EF.Functions.Like(a.Designation.ToLower(), formattedIdentifier));
          


            return whereCondition;
        }
        private Expression<Func<User, bool>> CombineConditions(Expression<Func<User, bool>> existingCondition, Expression<Func<User, bool>> newCondition)
        {
            if (existingCondition == null)
                return newCondition;

            var parameter = Expression.Parameter(typeof(Address));
            var body = Expression.OrElse(
                Expression.Invoke(existingCondition, parameter),
                Expression.Invoke(newCondition, parameter)
            );

            return Expression.Lambda<Func<User, bool>>(body, parameter);
        }
    }
}
