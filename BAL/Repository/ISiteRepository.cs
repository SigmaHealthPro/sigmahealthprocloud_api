using BAL.RequestModels;
using BAL.Constant;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.Responses;

namespace BAL.Repository
{
    public interface ISiteRepository : IGenericRepository<SiteModel>
    {
        public Task<IEnumerable<SiteModel>> GetAllAsync(SearchParams search);
        Task<ApiResponse<SiteModel>> GetSiteDetailsById(Guid siteId);
        public Task<List<SiteModel>> GetAllSites();
    }
}
