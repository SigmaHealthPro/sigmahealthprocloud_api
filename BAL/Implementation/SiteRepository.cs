using BAL.Pagination;
using BAL.Repository;
using BAL.RequestModels;
using BAL.Constant;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using BAL.Responses;
using System.Data.Common;

namespace BAL.Implementation
{
    public class SiteRepository : IGenericRepository<SiteModel>, ISiteRepository
    {
        private SigmaproIisContext context;
        private ILogger<UnitOfWork> _logger;
        private readonly string _corelationId = string.Empty;
        public SiteRepository(SigmaproIisContext _context, ILogger<UnitOfWork> logger)
        {
            context = _context;
            _logger = logger;
        }
        public async Task<IEnumerable<SiteModel>> Find(Expression<Func<SiteModel, bool>> predicate)
        {
            return (IEnumerable<SiteModel>)await context.Set<SiteModel>().FindAsync(predicate);
        }
        public async Task<IEnumerable<SiteModel>> GetAllAsync()
        {
            return await context.Set<SiteModel>().ToListAsync();
        }
        public async Task<IEnumerable<SiteModel>> GetAllAsync(SearchParams search)                                                          
        {
            var siteModelList = new List<SiteModel>();
            var query = (
              from site in context.Sites
              join facility in context.Facilities on site.FacilityId.ToString() equals facility.Id.ToString()
              join entityAddress in context.EntityAddresses on site.Id equals entityAddress.EntityId
              join entityContact in context.Contacts on site.Id equals entityContact.EntityId
              join address in context.Addresses on entityAddress.Addressid equals address.Id
              join city in context.Cities on address.CityId equals city.Id
              join state in context.States on address.StateId equals state.Id
              where (
                  (string.IsNullOrWhiteSpace(search.keyword) ||
                  site.SiteName.ToLower().IndexOf(search.keyword.ToLower()) >= 0 ||
                  facility.FacilityName.ToLower().IndexOf(search.keyword.ToLower()) >= 0 ||
                  city.CityName.ToLower().IndexOf(search.keyword.ToLower()) >= 0 ||
                  state.StateName.ToLower().IndexOf(search.keyword.ToLower()) >= 0 ||
                  site.SiteType.ToLower().IndexOf(search.keyword.ToLower()) >= 0 ||
                  site.ParentSite.ToLower().IndexOf(search.keyword.ToLower()) >= 0)
                  &&
                  (string.IsNullOrWhiteSpace(search.site_name) || site.SiteName.ToLower().IndexOf(search.site_name.ToLower()) >= 0)
                  &&
                  (string.IsNullOrWhiteSpace(search.facility_name) || facility.FacilityName.ToLower().IndexOf(search.facility_name.ToLower()) >= 0)
                  &&
                  (string.IsNullOrWhiteSpace(search.city) || city.CityName.ToLower().IndexOf(search.city.ToLower()) >= 0)
                  &&
                  (string.IsNullOrWhiteSpace(search.state) || state.StateName.ToLower().IndexOf(search.state.ToLower()) >= 0)
                  &&
                  (string.IsNullOrWhiteSpace(search.site_type) || site.SiteType.ToLower().IndexOf(search.site_type.ToLower()) >= 0)
                  &&
                  (string.IsNullOrWhiteSpace(search.parent_site) || site.ParentSite.ToLower().IndexOf(search.parent_site.ToLower()) >= 0)
                  &&
                  (string.IsNullOrWhiteSpace(search.sitepinnumber) || site.SitePinNumber.ToLower().IndexOf(search.sitepinnumber.ToLower()) >= 0)
              )
              select new SiteModel
              {
                  Address = $"{address.Line1} {address.Line2} {address.Suite}",
                  FacilityName = facility.FacilityName,
                  SiteName = site.SiteName,
                  ParentSite = site.ParentSite,
                  SiteType = site.SiteType,
                  SitePinNumber = site.SitePinNumber,
                  IsImmunizationSite = site.IsImmunizationSite,
                  SiteId = site.SiteId,
                  CityName = city.CityName,
                  StateName = state.StateName,
                  ZipCode = address.ZipCode,
                  FacilityId = site.FacilityId,
                  SiteContactPerson = site.SiteContactPerson,
                  Id = site.Id,
                  AddressId = address.Id,
                  ContactId = entityContact.Id

              }) ;

            if (!string.IsNullOrWhiteSpace(search.orderby))
            {
                switch (search.orderby.ToLower())
                {
                    case "site_name":
                        query = query.OrderBy(s => s.SiteName);
                        break;
                      
                }
            }

            var siteList = await query.ToPagedListAsync(search.pagenumber, search.pagesize);

            siteModelList.AddRange(siteList);

            return siteModelList;
        }
        public async Task<List<SiteModel>> GetAllSites()
        {
            try
            {
                var sitelist = new List<SiteModel>();
                var sites = await context.Sites.ToListAsync();
                foreach (var c in sites)
                {
                    var sitemod = new SiteModel()
                    {
                        Id = c.Id,
                        SiteName = c.SiteName,

                    };
                    sitelist.Add(sitemod);
                }

                return sitelist;
            }
            catch (Exception ex)
            {
                _logger.LogError($"CorelationId: {_corelationId} -Exception occurred in Method: {nameof(GetAllSites)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw new Exception(ex.Message); ;
            }
        }
        public async Task<ApiResponse<SiteModel>> GetSiteDetailsById(Guid siteId)
        {
            try
            {
                var query = (
                    from site in context.Sites
                    join facility in context.Facilities on site.FacilityId.ToString() equals facility.Id.ToString()
                    join entityAddress in context.EntityAddresses on site.Id equals entityAddress.EntityId
                    join address in context.Addresses on entityAddress.Addressid equals address.Id
                    join city in context.Cities on address.CityId equals city.Id
                    join state in context.States on address.StateId equals state.Id
                    where site.Id == siteId
                    select new SiteModel
                    {
                        SiteId = site.SiteId.ToString(),
                        SiteName = site.SiteName,
                        SiteType = site.SiteType,
                        ParentSite = site.ParentSite,
                        SiteContactPerson = site.SiteContactPerson,
                        FacilityName = facility.FacilityName,
                        CityName = city.CityName,
                        StateName = state.StateName,
                        ZipCode = address.ZipCode,
                        IsImmunizationSite = site.IsImmunizationSite,
                        SitePinNumber = site.SitePinNumber,
                        Address = $"{address.Line1} {address.Line2} {address.Suite}",

                    });
                
                var result = await query.ToListAsync();
                if (result.Count == 0 || result != null)
                {
                    return ApiResponse<SiteModel>.SuccessList(result, "Site fetched successfully!");
                }
                else
                {
                    return ApiResponse<SiteModel>.Fail("Site not found.");
                }
            }
            catch (DbException ex)
            {
                _logger.LogError($"CorrelationId: {_corelationId} - Database exception: {ex.Message}, Stack trace: {ex.StackTrace}");
                return ApiResponse<SiteModel>.Fail($"A database error occurred while fetching sites: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"CorrelationId: {_corelationId} - Exception occurred in GetAddresses: {ex.Message}, Stack trace: {ex.StackTrace}");
                return ApiResponse<SiteModel>.Fail($"An error occurred while fetching site: {ex.Message}");
            }
        }
        public async Task<ApiResponse<string>> InsertAsync(SiteModel sitemod)
        {

            try
            {
                var newsite = new Site()
                {
                    SiteId = sitemod.SiteId,
                    SiteName = sitemod.SiteName,            
                    SiteType = sitemod.SiteType,
                    ParentSite = sitemod.ParentSite,
                    SiteContactPerson = sitemod.SiteContactPerson,
                    SitePinNumber = sitemod.SitePinNumber,
                    IsImmunizationSite = sitemod.IsImmunizationSite,
                    FacilityId = sitemod.FacilityId,
                    AddressId = sitemod.AddressId,
                    CreatedBy = sitemod.CreatedBy,
                    CreatedDate = sitemod.CreatedDate,
                    UpdatedBy = sitemod.UpdatedBy,
                    UpdatedDate = sitemod.UpdatedDate,
                    Isdelete = sitemod.Isdelete,
                };
                context.Sites.Add(newsite);
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(newsite.Id.ToString(), "Site created successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while creating the site.");
            }
        }

        public async Task<ApiResponse<string>> UpdateAsync(SiteModel entity)
        {
            if (entity == null)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Invalid input. EditSiteRequest object is null in Method: {nameof(UpdateAsync)}");
                return ApiResponse<string>.Fail("Invalid input. EditSiteRequest object is null.");
            }
            try
            {
                var updateSite = new Site();// await context.Sites.FindAsync(entity.Id);
                if (entity != null)
                {
                    updateSite.AddressId = entity.AddressId;
                    updateSite.SiteName = entity.SiteName;
                    updateSite.SiteContactPerson = entity.SiteContactPerson;
                    updateSite.SitePinNumber = entity.SitePinNumber;
                    updateSite.IsImmunizationSite = entity.IsImmunizationSite;
                    updateSite.SiteType = entity.SiteType;
                    context.Sites.Update(updateSite);
                    await context.SaveChangesAsync();

                    return ApiResponse<string>.Success(updateSite.Id.ToString(), "Organization record updated successfully.");
                }
                return ApiResponse<string>.Fail("Site with the given ID not found.");

            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(UpdateAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while updating the site.");
            }
        }
        public async Task<ApiResponse<string>> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await context.Set<Site>().FindAsync(id);
                if (entity != null)
                {
                    entity.Isdelete = true;
                    context.Sites.Update(entity);
                    await context.SaveChangesAsync();
                    return ApiResponse<string>.Success(id.ToString(), "Site deleted successfully.");
                }

                return ApiResponse<string>.Fail("Site with the given ID not found.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(DeleteAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("Site with the given ID not found.");
            }
        }
        public async Task<SiteModel> GetByIdAsync(int id)
        {
            return await context.Set<SiteModel>().FindAsync(id);
        }

    }
}
