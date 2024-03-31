using BAL.Constant;
using BAL.Request;
using BAL.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Repository
{
    public interface IEventRepository : IGenericRepository<EventModel>
    {
        public Task<IEnumerable<EventModel>> GetAllAsync(SearchEventParams search);
        public Task<List<EventModel>> GetAllEvents();
    }
}
