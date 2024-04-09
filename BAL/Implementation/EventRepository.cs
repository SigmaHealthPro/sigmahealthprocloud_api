using BAL.Repository;
using BAL.RequestModels;
using Data.Models;
using Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using BAL.Constant;
using BAL.Pagination;
using System.Data.Common;

namespace BAL.Implementation
{
    public class EventRepository : IGenericRepository<EventModel>, IEventRepository
    {
        private SigmaproIisContext context;
        private readonly SigmaproIisContextUdf _dbContextudf;
        private ILogger<UnitOfWork> _logger;
        private readonly string _corelationId = string.Empty;
        public EventRepository(SigmaproIisContext _context, ILogger<UnitOfWork> logger, SigmaproIisContextUdf dbContextudf)
        {
            context = _context;
            _logger = logger;
            _dbContextudf = dbContextudf;
        }
        public async Task<IEnumerable<EventModel>> Find(Expression<Func<EventModel, bool>> predicate)
        {
            return (IEnumerable<EventModel>)await context.Set<EventModel>().FindAsync(predicate);
        }
        public async Task<IEnumerable<EventModel>> GetAllAsync()
        {
            return await context.Set<EventModel>().ToListAsync();
        }
        public async Task<IEnumerable<EventModel>> GetAllAsync(SearchEventParams search)
        {
            var eventModelList = new List<EventModel>();
            var query = (
                        from events in context.Events
                        join provider in context.Providers on events.ProviderId equals provider.Id
                        join site in context.Sites on events.SiteId equals site.Id
                        where (
                            (string.IsNullOrWhiteSpace(search.keyword) ||
                            events.EventName.ToLower().IndexOf(search.keyword.ToLower()) >= 0 ||
                            events.EventDate.ToString().ToLower().IndexOf(search.keyword.ToLower()) >= 0 ||
                            provider.ProviderName.ToLower().IndexOf(search.keyword.ToLower()) >= 0 ||
                            site.SiteName.ToLower().IndexOf(search.keyword.ToLower()) >= 0)
                            &&
                            (string.IsNullOrWhiteSpace(search.EventName) || events.EventName.ToLower().IndexOf(search.EventName.ToLower()) >= 0)
                            &&
                            (string.IsNullOrWhiteSpace(search.EventDate.ToString()) || events.EventDate.ToString().ToLower().IndexOf(search.EventDate.ToString().ToLower()) >= 0)

                            &&
                            (string.IsNullOrWhiteSpace(search.ProviderName) || provider.ProviderName.ToLower().IndexOf(search.ProviderName.ToLower()) >= 0)
                             &&
                            (string.IsNullOrWhiteSpace(search.SiteName) || site.SiteName.IndexOf(search.SiteName) >= 0)
                        )
                        select new EventModel
                        {
                            Id = events.Id,
                            EventName = events.EventName,
                            EventDate = events.EventDate,
                            SiteName = site.SiteName,

                            ProviderName = provider.ProviderName

                            // Additional fields as necessary
                        });

            // Applying ordering if specified in search parameters
            if (!string.IsNullOrWhiteSpace(search.orderby))
            {
                switch (search.orderby.ToLower())
                {
                    case "event_name":
                        query = query.OrderBy(s => s.EventName);
                        break;

                }
            }
            Console.WriteLine(query);
            var eventList = await query.ToPagedListAsync(search.pagenumber, search.pagesize);

            eventModelList.AddRange(eventList);

            return eventModelList;

        }
        public async Task<List<EventModel>> GetAllEvents()
        {
            try
            {
                var eventlist = new List<EventModel>();
                var events = await context.Events.ToListAsync();
                foreach (var c in events)
                {
                    var eventmod = new EventModel()
                    {
                        Id = c.Id,
                        EventName = c.EventName,

                    };
                    eventlist.Add(eventmod);
                }

                return eventlist;
            }
            catch (Exception ex)
            {
                _logger.LogError($"CorelationId: {_corelationId} -Exception occurred in Method: {nameof(GetAllEvents)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
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




        public async Task<ApiResponse<string>> InsertAsync(EventModel eventModel)
        {

            try
            {
                var newevent = new Event()
                {

                    EventName = eventModel.EventName,
                    EventDate = eventModel.EventDate.HasValue ? DateTime.SpecifyKind(eventModel.EventDate.Value, DateTimeKind.Utc) : DateTime.UtcNow,
                    // Convert to UTC
                    ProviderId = eventModel.ProviderId,
                    SiteId = eventModel.SiteId,
                    CreatedDate = eventModel.CreatedDate, // or providerModel.CreatedDate if you want to set it from the model
                    UpdatedDate = eventModel.UpdatedDate, // or providerModel.UpdatedDate if you want to set it from the model
                    CreatedBy = eventModel.CreatedBy, // Ensure this is provided in the modelv
                    UpdatedBy = eventModel.UpdatedBy  // Ensure this is provided in the model
                };
                if (eventModel.Id.ToString() == "3fa85f64-5717-4562-b3fc-2c963f66afa6")
                {
                    context.Events.Add(newevent);
                }
                else
                {
                    newevent.Id = eventModel.Id;
                    context.Events.Update(newevent);
                }
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(newevent.Id.ToString(), "provider created successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while creating the Provider.");
            }
        }
        public async Task<ApiResponse<string>> UpdateAsync(EventModel entity)
        {
            if (entity == null)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Invalid input. EditproviderRequest object is null in Method: {nameof(UpdateAsync)}");
                return ApiResponse<string>.Fail("Invalid input. EditEventRequest object is null.");
            }
            try
            {
                var updateEvent = await context.Events.FindAsync(entity.Id);
                if (updateEvent != null)
                {


                    context.Events.Update(updateEvent);
                    await context.SaveChangesAsync();

                    return ApiResponse<string>.Success(updateEvent.Id.ToString(), "Event record updated successfully.");
                }
                return ApiResponse<string>.Fail("Event with the given ID not found.");

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
                var entity = await context.Set<Event>().FindAsync(id);
                if (entity == null)
                {
                    _logger.LogError($"Provider with ID {id} not found.");
                    return ApiResponse<string>.Fail("Provider not found.");
                }

                context.Events.Remove(entity);
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(null, "Event deleted successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(DeleteAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("Event with the given ID not found.");
            }
        }
        public async Task<EventModel> GetByIdAsync(int id)
        {
            return await context.Set<EventModel>().FindAsync(id);
        }


    }
}
