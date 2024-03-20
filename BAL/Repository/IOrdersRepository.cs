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
   
    public interface IOrdersRepository : IGenericRepository<OrderModel>
    {
        public Task<PaginationModel<OrderModel>> GetAllAsync(SearchOrderParams search);
        public Task<PaginationModel<OrderModel>> GetAllOrders(int pagenumber,int pagesize);
        public Task<ApiResponse<string>> InsertOrdersAsync(RespOrderModel entity);

        public Task<IEnumerable<Mvx>> GetAllManufacturers();
    }
}
