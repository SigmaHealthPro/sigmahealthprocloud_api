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
    public interface IProviderRepository : IGenericRepository<ProviderModel>
    {
        public Task<IEnumerable<ProviderModel>> GetAllAsync(SearchProviderParams search);

        Task<ApiResponse<ProviderModel>> GetProviderDetailsById(Guid providerId);
        public Task<List<ProviderModel>> GetAllProviders();

    }
}
