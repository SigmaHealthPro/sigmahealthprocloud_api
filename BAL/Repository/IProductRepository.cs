using BAL.Constant;
using BAL.RequestModels;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Repository
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        public Task<PaginationModel<VaccinesModel>> GetAllVaccinesbyfacilityid(Guid facilityid,int pagenumber,int pagesize,Guid manufacturerid);
        public Task<ApiResponse<string>> InsertVaccinedata(VaccineModel entity);
    }
}
