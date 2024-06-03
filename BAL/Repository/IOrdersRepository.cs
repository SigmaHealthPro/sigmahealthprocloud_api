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
        public Task<ApiResponse<UserAddressModel>> GetAddressbyUserid(Guid userid);
        public Task<ApiResponse<ShipmentAddressModel>> GetAddressbyOrderid(Guid orderid);
        public Task<ApiResponse<IEnumerable<OrderItemsmodel>>> GetOrderdetailsbyOrderid(Guid orderid);
        public Task<ApiResponse<string>> Updateorderstatus(string status, Guid orderid,string comments);
    }
}
