using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Repository
{
    public interface IFacilitiesRepository : IGenericRepository<Facility>
    {
        public Task<IEnumerable<Facility>> GetAllFacilitiesbyjurdid(Guid jurdid);
    }
}
