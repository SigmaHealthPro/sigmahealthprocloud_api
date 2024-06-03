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
using System.Xml.Linq;

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
                query = query.Where(x => x.Isdelete == false);

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
                    UserType = a.UserType,
                    Password=a.Password

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
        public async Task<ApiResponse<PersonDetailsResponse>> GetPersons(GetDataByCountRequest requestObject)
        {
            try
            {
                var query = _dbContext.Set<Person>().AsQueryable();
                query = query.Where(x => x.Isdelete == false);
                var resultQuery = query.Select(a => new PersonDetailsResponse
                {
                    Id = a.Id,
                    CreatedBy = a.CreatedBy,
                    CreatedDate = a.CreatedDate,
                    BirthOrder = a.BirthOrder,
                    BirthStateId = a.BirthStateId,
                    DateOfBirth = a.DateOfBirth,
                    FirstName = a.FirstName,
                    Gender = a.Gender,
                    Isdelete = a.Isdelete,
                    LastName = a.LastName,
                    MiddleName = a.MiddleName,
                    MotherFirstName = a.MotherFirstName,
                    MotherMaidenLastName = a.MotherMaidenLastName,
                    MotherLastName = a.MotherLastName,
                    PersonId = a.PersonId,
                    PersonType = a.PersonType,
                    UpdatedBy = a.UpdatedBy,
                    UpdatedDate = a.UpdatedDate
                })
                .OrderBy(a => a.CreatedDate);


                var result = await resultQuery.ToListAsync();

                result = result.Take(requestObject.RecordCount ?? 500).ToList();

                return ApiResponse<PersonDetailsResponse>.SuccessList(result, "Persons fetched successfully!");
            }
            catch (DbException ex)
            {
                _logger.LogError($"CorrelationId: {_correlationId} - Database exception: {ex.Message}, Stack trace: {ex.StackTrace}");
                return ApiResponse<PersonDetailsResponse>.Fail($"A database error occurred while fetching Persons: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"CorrelationId: {_correlationId} - Exception occurred in GetPersons: {ex.Message}, Stack trace: {ex.StackTrace}");
                return ApiResponse<PersonDetailsResponse>.Fail($"An error occurred while fetching Persons: {ex.Message}");
            }
        }
        public async Task<ApiResponse<PersonDetailsResponse>> GetPersonsById(Guid id)
        {
            try
            {
                var personData = await _dbContext.People.FindAsync(id);

                if (personData == null)
                {
                    return ApiResponse<PersonDetailsResponse>.Fail("No data found.");
                }
                var personDataDetails = PersonDetailsResponse.FromPersonEntity(personData);
                return ApiResponse<PersonDetailsResponse>.Success(personDataDetails);
            }
            catch (Exception exp)
            {
                _logger.LogError($"An error occurred: {exp.Message}, Stack trace: {exp.StackTrace}");
                return ApiResponse<PersonDetailsResponse>.Fail("An error occurred while fetching details.");
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
                            CreatedDate = DateTime.UtcNow,
                            Designation = userRequest.Designation,
                            Status = true,
                            Gender = userRequest.Gender,
                            ImageUrl = userRequest.ImageUrl,
                            Isdelete = false,
                            PersonId = userRequest.PersonId,
                            SequenceId = userRequest.SequenceId,
                            UpdatedBy = userRequest.UpdatedBy,
                            UpdatedDate = DateTime.UtcNow,
                            UserId = userRequest.UserId,
                            UserType = userRequest.UserType,
                            Password = userRequest.Password
                        };

                        _dbContext.Users.Add(newUser);

                        await _dbContext.SaveChangesAsync();

                        transaction.Commit();

                        return ApiResponse<string>.Success(null, $"User added successfully");
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
                        existingUser.UpdatedDate = DateTime.UtcNow ;
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


        public async Task<ApiResponse<ProfileResponse>> GetUserRoleAccessFeaturesAndProfiles(Guid lovMasterRoleId)
        {
            try
            {
                var featureQuery = _dbContext.UserRoleAccesses
                    .Where(ura => ura.LovMasterRoleId == lovMasterRoleId && ura.LinkType == "feature")
                    .Join(_dbContext.Features,
                          ura => ura.LinkId,
                          f => f.Featureid,
                          (ura, f) => new { ura, f })
                    .Join(_dbContext.Profiles,
                          uf => uf.f.Profileid,
                          p => p.Profileid,
                          (uf, p) => new { uf.ura, uf.f, p });

                var subFeatureQuery = _dbContext.UserRoleAccesses
                    .Where(ura => ura.LovMasterRoleId == lovMasterRoleId && ura.LinkType == "subfeature")
                    .Join(_dbContext.Subfeatures,
                          ura => ura.LinkId,
                          sf => sf.SubfeatureId,
                          (ura, sf) => new { ura, sf })
                    .Join(_dbContext.Features,
                          us => us.sf.Featureid,
                          f => f.Featureid,
                          (us, f) => new { us.ura, us.sf, f })
                    .Join(_dbContext.Profiles,
                          usf => usf.f.Profileid,
                          p => p.Profileid,
                          (usf, p) => new { usf.ura, usf.sf, usf.f, p });

                var featureResult = await featureQuery.ToListAsync();
                var subFeatureResult = await subFeatureQuery.ToListAsync();

                var profiles = new List<ProfileResponse>();

                foreach (var fr in featureResult)
                {
                    var profile = profiles.FirstOrDefault(p => p.ProfileId == fr.p.Profileid);
                    if (profile == null)
                    {
                        profile = new ProfileResponse
                        {
                            ProfileId = fr.p.Profileid,
                            ProfileName = fr.p.ProfileName,
                            IconCode = fr.p.IconCode,
                            ViewOrder = fr.p.ViewOrder,
                            Features = new List<FeatureResponse>()
                        };
                        profiles.Add(profile);
                    }

                    var feature = new FeatureResponse
                    {
                        FeatureId = fr.f.Featureid,
                        FeatureName = fr.f.FeatureName,
                        FeatureLink = fr.f.Featurelink,
                        HasSubFeature = fr.f.HasSubfeature,
                        IconCode = fr.f.IconCode,
                        ViewOrder = fr.f.ViewOrder,
                        Element= fr.f.Element,
                        SubFeatures = new List<SubFeatureResponse>()
                    };

                    profile.Features.Add(feature);
                }

                foreach (var sfr in subFeatureResult)
                {
                    var profile = profiles.FirstOrDefault(p => p.ProfileId == sfr.p.Profileid);
                    if (profile == null)
                    {
                        profile = new ProfileResponse
                        {
                            ProfileId = sfr.p.Profileid,
                            ProfileName = sfr.p.ProfileName,
                            IconCode = sfr.p.IconCode,
                            ViewOrder = sfr.p.ViewOrder,

                            Features = new List<FeatureResponse>()
                        };
                        profiles.Add(profile);
                    }

                    var feature = profile.Features.FirstOrDefault(f => f.FeatureId == sfr.f.Featureid);
                    if (feature == null)
                    {
                        feature = new FeatureResponse
                        {
                            FeatureId = sfr.f.Featureid,
                            FeatureName = sfr.f.FeatureName,
                            FeatureLink = sfr.f.Featurelink,
                            HasSubFeature = sfr.f.HasSubfeature,
                            IconCode = sfr.f.IconCode,
                            ViewOrder = sfr.f.ViewOrder,
                            Element= sfr.f.Element,
                            SubFeatures = new List<SubFeatureResponse>()
                        };
                        profile.Features.Add(feature);
                    }

                    var subFeature = new SubFeatureResponse
                    {
                        SubFeatureId = sfr.sf.SubfeatureId,
                        SubFeatureName = sfr.sf.SubfeatureName,
                        SubFeatureLink = sfr.sf.SubfeatureLink,
                        IconCode = sfr.sf.IconCode,
                        Element = sfr.sf.Element,
                        ViewOrder = sfr.sf.ViewOrder
                    };

                    feature.SubFeatures.Add(subFeature);
                }

                // Order the lists by ViewOrder
                profiles = profiles.OrderBy(p => p.ViewOrder).ToList();
                foreach (var profile in profiles)
                {
                    profile.Features = profile.Features.OrderBy(f => f.ViewOrder).ToList();
                    foreach (var feature in profile.Features)
                    {
                        feature.SubFeatures = feature.SubFeatures.OrderBy(sf => sf.ViewOrder).ToList();
                    }
                }

                return ApiResponse<ProfileResponse>.SuccessList(profiles, "UserRoleAccess features and profiles fetched successfully!");
            }
            catch (DbException ex)
            {
                _logger.LogError($"CorrelationId: {_correlationId} - Database exception: {ex.Message}, Stack trace: {ex.StackTrace}");
                return ApiResponse<ProfileResponse>.Fail($"A database error occurred while fetching UserRoleAccess features and profiles: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"CorrelationId: {_correlationId} - Exception occurred in GetUserRoleAccessFeaturesAndProfiles: {ex.Message}, Stack trace: {ex.StackTrace}");
                return ApiResponse<ProfileResponse>.Fail($"An error occurred while fetching UserRoleAccess features and profiles: {ex.Message}");
            }
        }




        #region 
        //Private Function
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
        #endregion
    }
}
