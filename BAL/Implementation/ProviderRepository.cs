using BAL.Constant;
using BAL.Pagination;
using BAL.Repository;
using BAL.Request;
using BAL.RequestModels;
using BAL.Services;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;
using BAL.Responses;


namespace BAL.Implementation
{
    public class ProviderRepository : IGenericRepository<ProviderModel>, IProviderRepository
    {
        private SigmaproIisContext context;
        private readonly SigmaproIisContextUdf _dbContextudf;
        private ILogger<UnitOfWork> _logger;
        private readonly string _corelationId = string.Empty;
        public ProviderRepository(SigmaproIisContext _context, ILogger<UnitOfWork> logger, SigmaproIisContextUdf dbContextudf)
        {
            context = _context;
            _logger = logger;
            _dbContextudf = dbContextudf;
        }
        public async Task<IEnumerable<ProviderModel>> Find(Expression<Func<ProviderModel, bool>> predicate)
        {
            return (IEnumerable<ProviderModel>)await context.Set<ProviderModel>().FindAsync(predicate);
        }
        public async Task<IEnumerable<ProviderModel>> GetAllAsync()
        {
            return await context.Set<ProviderModel>().ToListAsync();
        }
        public async Task<IEnumerable<ProviderModel>> GetAllAsync(SearchProviderParams search)
        {
            var providerModelList = new List<ProviderModel>();
            var query = (
                        from provider in context.Providers
                        join facility in context.Facilities on provider.FacilityId.ToString() equals facility.Id.ToString()
                        join entityAddress in context.EntityAddresses on facility.Id equals entityAddress.EntityId
                        join address in context.Addresses on entityAddress.Addressid equals address.Id
                        join city in context.Cities on address.CityId equals city.Id
                        join state in context.States on city.StateId equals state.Id
                        where (
                            (string.IsNullOrWhiteSpace(search.keyword) ||
                            provider.ProviderName.ToLower().IndexOf(search.keyword.ToLower()) >= 0 ||
                            facility.FacilityName.ToLower().IndexOf(search.keyword.ToLower()) >= 0 ||
                            facility.FacilityId.ToLower().IndexOf(search.keyword.ToLower()) >= 0 ||
                            provider.ContactNumber.ToLower().IndexOf(search.keyword.ToLower()) >= 0 ||
                            city.CityName.ToLower().IndexOf(search.keyword.ToLower()) >= 0 ||
                            state.StateName.ToLower().IndexOf(search.keyword.ToLower()) >= 0 ||
                            provider.ProviderType.ToLower().IndexOf(search.keyword.ToLower()) >= 0 ||
                            provider.Email.ToLower().IndexOf(search.keyword.ToLower()) >= 0 ||
                            provider.Specialty.ToLower().IndexOf(search.keyword.ToLower()) >= 0)
                            &&
                            (string.IsNullOrWhiteSpace(search.providerName) || provider.ProviderName.ToLower().IndexOf(search.providerName.ToLower()) >= 0)
                            &&
                            (string.IsNullOrWhiteSpace(search.contact_number) || provider.ContactNumber.ToLower().IndexOf(search.contact_number.ToLower()) >= 0)
                            &&
                            (string.IsNullOrWhiteSpace(search.speciality) || provider.Specialty.ToLower().IndexOf(search.speciality.ToLower()) >= 0)
                            &&
                            (string.IsNullOrWhiteSpace(search.providerType) || provider.ProviderType.ToLower().IndexOf(search.providerType.ToLower()) >= 0)
                            &&
                            (string.IsNullOrWhiteSpace(search.facility_name) || facility.FacilityName.ToLower().IndexOf(search.facility_name.ToLower()) >= 0)
                            &&
                            (string.IsNullOrWhiteSpace(search.city) || city.CityName.ToLower().IndexOf(search.city.ToLower()) >= 0)
                            &&
                            (string.IsNullOrWhiteSpace(search.state) || state.StateName.ToLower().IndexOf(search.state.ToLower()) >= 0)
                            &&
                            (string.IsNullOrWhiteSpace(search.zipcode) || address.ZipCode.IndexOf(search.zipcode) >= 0)
                            &&
                            (string.IsNullOrWhiteSpace(search.facilityid) || facility.FacilityId.IndexOf(search.facilityid) >= 0)
                             &&
                            (string.IsNullOrWhiteSpace(search.email) || provider.Email.IndexOf(search.email) >= 0)
                        )
                        select new ProviderModel
                        {

                            ProviderName = provider.ProviderName,
                            ProviderType = provider.ProviderType,
                            ContactNumber = provider.ContactNumber,
                            Email = provider.Email,
                            Speciality = provider.Specialty,
                            FacilityName = facility.FacilityName,
                            CityName = city.CityName,
                            StateName = state.StateName,
                            ZipCode = address.ZipCode,
                            AddressId = address.Id
                            // Additional fields as necessary
                        });

            // Applying ordering if specified in search parameters
            if (!string.IsNullOrWhiteSpace(search.orderby))
            {
                switch (search.orderby.ToLower())
                {
                    case "provider_name":
                        query = query.OrderBy(s => s.ProviderName);
                        break;

                }
            }
            Console.WriteLine(query);
            var providerList = await query.ToPagedListAsync(search.pagenumber, search.pagesize);

            providerModelList.AddRange(providerList);

            return providerModelList;

        }
        public async Task<List<ProviderModel>> GetAllProviders()
        {
            try
            {
                var providerlist = new List<ProviderModel>();
                var providers = await context.Providers.ToListAsync();
                foreach (var c in providers)
                {
                    var providermod = new ProviderModel()
                    {
                        Id = c.Id,
                        ProviderName = c.ProviderName,

                    };
                    providerlist.Add(providermod);
                }

                return providerlist;
            }
            catch (Exception ex)
            {
                _logger.LogError($"CorelationId: {_corelationId} -Exception occurred in Method: {nameof(GetAllProviders)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw new Exception(ex.Message); ;
            }
        }

        public async Task<ApiResponse<ProviderModel>> GetProviderDetailsById(Guid providerId)
        {
            try
            {

                var query = (
                     from provider in context.Providers
                     join facility in context.Facilities on provider.FacilityId equals facility.Id
                     join entityAddress in context.EntityAddresses on provider.Id equals entityAddress.EntityId
                     join address in context.Addresses on entityAddress.Addressid equals address.Id
                     join city in context.Cities on address.CityId equals city.Id
                     join state in context.States on city.StateId equals state.Id
                     where provider.Id == providerId
                     select new ProviderModel
                     {
                         ProviderId = provider.Id.ToString(),
                         ProviderName = provider.ProviderName,
                         ProviderType = provider.ProviderType,
                         ContactNumber = provider.ContactNumber,
                         Email = provider.Email,
                         Speciality = provider.Specialty,
                         FacilityName = facility.FacilityName,
                         CityName = city.CityName,
                         StateName = state.StateName,
                         ZipCode = address.ZipCode,
                         AddressId = address.Id
                         // Additional fields as necessary
                     });


                var result = await query.ToListAsync();
                if (result.Count == 0 || result != null)
                {
                    return ApiResponse<ProviderModel>.SuccessList(result, "Provider fetched successfully!");
                }
                else
                {
                    return ApiResponse<ProviderModel>.Fail("Provider not found.");
                }
            }
            catch (DbException ex)
            {
                _logger.LogError($"CorrelationId: {_corelationId} - Database exception: {ex.Message}, Stack trace: {ex.StackTrace}");
                return ApiResponse<ProviderModel>.Fail($"A database error occurred while fetching Providers: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"CorrelationId: {_corelationId} - Exception occurred in GetAddresses: {ex.Message}, Stack trace: {ex.StackTrace}");
                return ApiResponse<ProviderModel>.Fail($"An error occurred while fetching Provider: {ex.Message}");
            }
        }




        public async Task<ApiResponse<string>> InsertAsync(ProviderModel providerModel)
        {

            try
            {
                var newprovider = new Provider()
                {

                    ProviderName = providerModel.ProviderName,
                    Email = providerModel.Email,
                    ProviderType = providerModel.ProviderType,
                    Specialty = providerModel.Speciality,
                    ContactNumber = providerModel.ContactNumber,
                    FacilityId = providerModel.FacilityId,
                    CreatedDate = DateTime.UtcNow, // or providerModel.CreatedDate if you want to set it from the model
                    UpdatedDate = DateTime.UtcNow, // or providerModel.UpdatedDate if you want to set it from the model
                    CreatedBy = providerModel.CreatedBy, // Ensure this is provided in the model
                    UpdatedBy = providerModel.UpdatedBy  // Ensure this is provided in the model
                };
                if (providerModel.Id.ToString() == "3fa85f64-5717-4562-b3fc-2c963f66afa6")
                {
                    context.Providers.Add(newprovider);
                }
                else
                {
                    newprovider.Id = providerModel.Id;
                    context.Providers.Update(newprovider);
                }
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(newprovider.Id.ToString(), "provider created successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while creating the Provider.");
            }
        }

        public async Task<ApiResponse<string>> UpdateAsync(ProviderModel entity)
        {
            if (entity == null)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Invalid input. EditproviderRequest object is null in Method: {nameof(UpdateAsync)}");
                return ApiResponse<string>.Fail("Invalid input. EditProviderRequest object is null.");
            }
            try
            {
                var updateProvider = await context.Providers.FindAsync(entity.Id);
                if (updateProvider != null)
                {
                    updateProvider.ProviderName = entity.ProviderName;
                    updateProvider.ProviderType = entity.ProviderType;
                    updateProvider.ContactNumber = entity.ContactNumber;
                    updateProvider.Email = entity.Email;
                    updateProvider.Specialty = entity.Speciality;

                    context.Providers.Update(updateProvider);
                    await context.SaveChangesAsync();

                    return ApiResponse<string>.Success(updateProvider.Id.ToString(), "Organization record updated successfully.");
                }
                return ApiResponse<string>.Fail("Provider with the given ID not found.");

            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(UpdateAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while updating the Provider.");
            }
        }
        public async Task<ApiResponse<string>> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await context.Set<Provider>().FindAsync(id);
                if (entity == null)
                {
                    _logger.LogError($"Provider with ID {id} not found.");
                    return ApiResponse<string>.Fail("Provider not found.");
                }

                context.Providers.Remove(entity);
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(null, "Provider deleted successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(DeleteAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("Provider with the given ID not found.");
            }
        }
        public async Task<ProviderModel> GetByIdAsync(int id)
        {
            return await context.Set<ProviderModel>().FindAsync(id);
        }

    }
}
