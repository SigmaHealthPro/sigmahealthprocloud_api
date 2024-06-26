﻿using BAL.Constant;
using BAL.Pagination;
using BAL.Repository;
using BAL.Request;
using BAL.RequestModels;
using BAL.Responses;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace BAL.Implementation
{
    public class OrdersRepository : IGenericRepository<OrderModel>, IOrdersRepository
    {
        private SigmaproIisContext context;
        private ILogger<UnitOfWork> _logger;
        private readonly string _corelationId = string.Empty;
        public OrdersRepository(SigmaproIisContext _context, ILogger<UnitOfWork> logger)
        {
            context = _context;
            _logger = logger;
        }
        public async Task<ApiResponse<string>> DeleteAsync(Guid id)
        {
            try
            {
                var Orders = await context.Orders.FindAsync(id);

                if (Orders != null)
                {
                    Orders.Isdelete = true;
                    context.Orders.Update(Orders);
                    await context.SaveChangesAsync();

                    return ApiResponse<string>.Success(id.ToString(), "Order deleted successfully.");
                }

                return ApiResponse<string>.Fail("Order with the given ID not found.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(DeleteAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("Order with the given ID not found.");
            }
        }

        public async Task<IEnumerable<OrderModel>> Find(Expression<Func<OrderModel, bool>> predicate)
        {
            return (IEnumerable<OrderModel>)await context.Set<OrderModel>().FindAsync(predicate);
        }

        public async Task<IEnumerable<OrderModel>> GetAllAsync()
        {
            return await context.Set<OrderModel>().ToListAsync();
        }

        public async Task<OrderModel> GetByIdAsync(int id)
        {
            return await context.Set<OrderModel>().FindAsync(id);
        }

        public async Task<ApiResponse<string>> InsertAsync(OrderModel entity)
        {
            try
            {
                await context.Set<OrderModel>().AddAsync(entity);
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(null, "Order inserted successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while Inserting the Order.");
            }

        }

        public async Task<ApiResponse<string>> UpdateAsync(OrderModel entity)
        {
            if (entity == null)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Invalid input. EditOrderRequest object is null in Method: {nameof(UpdateAsync)}");
                return ApiResponse<string>.Fail("Invalid input. EditOrderRequest object is null.");
            }
            try
            {
                var updateOrders = await context.Orders.FindAsync(entity.Id);  
                
                if (updateOrders != null)
                {
                    var updateorderitems =await context.OrderItems.FindAsync(entity.OrderItemId);
                    updateOrders.OrderDate = entity.OrderDate;
                    updateOrders.OrderStatus = entity.OrderStatus; 
                    updateOrders.OrderTotal = entity.OrderTotal;
                    updateOrders.UpdatedBy = entity.UpdatedBy;
                    updateOrders.UpdatedDate = entity.UpdatedDate;
                    updateOrders.DiscountAmount = entity.DiscountAmount;
                    updateOrders.Incoterms = entity.Incoterms;
                    updateOrders.TaxAmount = entity.TaxAmount;
                    updateOrders.TermsConditionsId = entity.TermsConditionsId;                   
                    context.Orders.Update(updateOrders);
                    await context.SaveChangesAsync();
                    if (updateorderitems != null)
                    {
                        updateorderitems.OrderItemDesc = entity.OrderItemDesc;
                        updateorderitems.UpdatedDate = entity.UpdatedDate;
                        updateorderitems.OrderItemStatus = entity.OrderItemStatus;
                        updateorderitems.Quantity = entity.Quantity;
                        updateorderitems.ProductId = entity.ProductId;
                        updateorderitems.UnitPrice = entity.UnitPrice;
                        updateorderitems.UpdatedBy = entity.UpdatedBy;
                        context.OrderItems.Update(updateorderitems);
                        await context.SaveChangesAsync();
                    }


                    return ApiResponse<string>.Success(entity.Id.ToString(), "Order record updated successfully.");
                }
                return ApiResponse<string>.Success(null,"Order updated");

            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(UpdateAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while updating the order.");
            }
        }
        public async Task<ApiResponse<string>> Updateorderstatus(string status,Guid orderid,string comments)
        {
            try
            {
                var updateOrders = await context.Orders.FindAsync(orderid);
                if (updateOrders != null)
                {
                    updateOrders.OrderStatus = status;
                    var updateorderitems = await context.OrderItems.Where(i=>i.OrderId==orderid).ToListAsync();
                    context.Orders.Update(updateOrders);
                    await context.SaveChangesAsync(); 
                    if(updateorderitems != null)
                    {
                        foreach (var item in updateorderitems)
                        {
                            // Update the properties of the OrderItems as needed
                            item.OrderItemStatus = status; // Replace 'PropertyToUpdate' and 'newValue' with actual property and value
                                                              // Repeat the above line for other properties you want to update
                        }                       
                        
                        await context.SaveChangesAsync();
                    }
                    var comment = new Comment()
                    {
                        CommentsHistory = comments,
                        CommentSourceId = orderid,
                        CreatedBy = updateOrders.CreatedBy,
                        UpdatedBy = updateOrders.UpdatedBy,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                    };
                    context.Comments.Add(comment);
                    await context.SaveChangesAsync();
                    
                }
                return ApiResponse<string>.Success(null,"Order Approved.");

            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(UpdateAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while updating the order.");
            }

        }
        public async Task<PaginationModel<OrderModel>> GetAllAsync(SearchOrderParams search)
        {
            var orderModelList = new List<OrderModel>();            
            string keyword = search.keyword.IsNullOrEmpty() ? string.Empty : search.keyword.Trim().ToLower();           
            string orderdate = search.date_of_order.IsNullOrEmpty()? string.Empty : search.date_of_order.ToLower();            
            string orderstatus= search.order_status.IsNullOrEmpty() ? string.Empty : search.order_status.ToLower();
            string orderitemdesc = search.order_item_desc.IsNullOrEmpty() ? string.Empty : search.order_item_desc.ToLower();

            var orderlist = await  context.Orders.
                            Join(context.OrderItems, ord => ord.Id.ToString(), oi => oi.OrderId.ToString(), (ord, oi) => new { orders = ord, items = oi }).                            
                            Join(context.Facilities, o => o.orders.FacilityId, f => f.Id, (o, f) => new { orders = o.orders, o.items, facility = f }).
                            Join(context.Products, fa => fa.items.ProductId, p => p.Id, (fa, p) => new { orders = fa.orders, fa.items,fa.facility, product = p }).                            
                            Join(context.Cvxes, pr=>pr.product.CvxCodeId,c=>c.Id,(pr,c)=>new { orders = pr.orders, pr.items, product = pr.product, pr.facility,cvx = c}).
                            Join(context.Mvxes, pr => pr.product.MvxCodeId, m => m.Id, (pr, m) => new { orders = pr.orders, pr.items, product = pr.product, pr.facility,pr.cvx, mvx = m }).
                            Where(i =>(i.items.OrderItemDesc.ToLower().Contains(keyword) || i.facility.FacilityName.ToLower().Contains(keyword)||i.product.ProductName.ToLower().Contains(keyword)
                            ) && i.orders.OrderStatus=="Approved" && i.orders.Isdelete == false).
                            Select(i => new
                            {
                                i.orders.Id,
                                i.orders.OrderId,
                                i.product.ProductName,
                                i.items.OrderItemDesc,                                
                                i.cvx.CvxDescription,
                                i.items.OrderItemStatus,
                                i.items.Quantity,
                                i.product,
                                i.items.UnitPrice,
                                i.facility.FacilityName,
                                i.facility,
                                i.orders.DiscountAmount,
                                i.orders.Incoterms,
                                i.orders ,
                                i.mvx

                            }).Distinct().ToListAsync();//ToPagedListAsync(search.pagenumber, search.pagesize);

            Parallel.ForEach(orderlist, async i =>
            {
                var model = new OrderModel()
                {
                    Id = i.orders.Id,
                    OrderId = i.OrderId,
                    FacilityId =i.facility.Id,
                   Facility=i.FacilityName,
                   DiscountAmount=i.DiscountAmount,
                   Incoterms=i.Incoterms,
                   OrderDate = i.orders.OrderDate,
                    OrderItemDesc = i.OrderItemDesc,
                    OrderItemStatus = i.OrderItemStatus,
                    OrderTotal =i.orders.OrderTotal,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity,
                    TaxAmount = i.orders.TaxAmount,
                   OrderStatus=i.orders.OrderStatus,
                   TermsConditionsId = i.orders.TermsConditionsId,
                   UserId=i.orders.UserId,
                    Product = i.ProductName,
                    CVXDesc =i.CvxDescription,
                   CreatedBy=i.orders.CreatedBy,
                   CreatedDate=i.orders.CreatedDate,
                    UpdatedBy = i.orders.UpdatedBy,
                    ProductId = i.product.Id,
                    manufacturername = i.mvx.ManufacturerName
                };
                orderModelList.Add(model);

            });
            Task.WhenAll();

            long? totalRows = orderModelList.Count();
            var response = orderModelList.Skip(search.pagesize * (search.pagenumber - 1)).Take(search.pagesize).ToList();
            return PaginationHelper.Paginate(response, search.pagenumber, search.pagesize, Convert.ToInt32(totalRows));

        }

        public async Task<PaginationModel<OrderModel>> GetAllOrders(int pagenumber,int pagesize)
        {
            var orderModelList = new List<OrderModel>();
            var orderlist = await context.Orders.
                            Join(context.OrderItems, ord => ord.Id.ToString(), oi => oi.OrderId.ToString(), (ord, oi) => new { orders = ord, items = oi }).
                            Join(context.Facilities, o => o.orders.FacilityId, f => f.Id, (o, f) => new { orders = o.orders, o.items, facility = f }).
                            Join(context.Products, fa => fa.items.ProductId, p => p.Id, (fa, p) => new { orders = fa.orders, fa.items, fa.facility, product = p }).                            
                            Join(context.Cvxes, pr => pr.product.CvxCodeId, c => c.Id, (pr, c) => new { orders = pr.orders, product = pr.product, pr.items,  pr.facility, cvx = c }).
                            Join(context.Mvxes, pr => pr.product.MvxCodeId, m => m.Id, (pr, m) => new { orders = pr.orders, pr.items, product = pr.product, pr.facility, pr.cvx, mvx = m }).
                            Where(i=>i.orders.Isdelete==false && (i.orders.OrderStatus=="Active")).Select
                            (i => new
                            {
                                i.orders.Id,
                                i.orders.OrderId,
                                i.product.ProductName,
                                i.product.ProductId,
                                i.product.CvxCodeId,
                                i.product,
                                i.items.OrderItemDesc,                                
                                i.cvx.CvxDescription,
                                i.items.OrderItemStatus,
                                i.items.Quantity,
                                i.items.UnitPrice,
                                i.facility.FacilityName,
                                i.facility,
                                i.orders.DiscountAmount,
                                i.orders.Incoterms,
                                i.orders,
                                i.mvx

                            }).ToPagedListAsync(pagenumber, pagesize);

            Parallel.ForEach(orderlist, async i =>
            {
                var model = new OrderModel()
                {
                    FacilityId = i.facility.Id,  
                    Facility=i.FacilityName,
                    DiscountAmount = i.DiscountAmount,
                    Incoterms = i.Incoterms,
                    OrderDate = i.orders.OrderDate,
                    OrderItemDesc = i.OrderItemDesc,
                    OrderItemStatus = i.OrderItemStatus,
                    Id = i.orders.Id,
                    OrderTotal = i.orders.OrderTotal,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity,
                    TaxAmount = i.orders.TaxAmount,
                    OrderStatus = i.orders.OrderStatus,
                    TermsConditionsId = i.orders.TermsConditionsId,
                    UserId = i.orders.UserId,
                    Product = i.ProductName,
                    CVXDesc = i.CvxDescription,
                    manufacturername=i.mvx.ManufacturerName,
                    CreatedBy = i.orders.CreatedBy,
                    CreatedDate = i.orders.CreatedDate,
                    UpdatedBy = i.orders.UpdatedBy,
                    OrderId = i.OrderId,
                    ProductId = i.product.Id
                };
                orderModelList.Add(model);

            });
            Task.WhenAll();

            long? totalRows = orderModelList.Count();
            var response = orderModelList.Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList();
            return PaginationHelper.Paginate(response, pagenumber, pagesize, Convert.ToInt32(totalRows));
           
        }
        public async Task<IEnumerable<Mvx>> GetAllManufacturers()
        {
            var manufacturerslist = new List<Mvx>();

            var Manufacturers = await context.Set<Mvx>().ToListAsync();
            foreach (var m in Manufacturers)
            {
                var mvxdet = new Mvx()
                {
                    Id = m.Id,
                    MvxId = m.MvxId,
                    ManufacturerName = m.ManufacturerName,
                    
                };
                manufacturerslist.Add(mvxdet);
            }
            return manufacturerslist;
        }

        public async Task<ApiResponse<string>> InsertOrdersAsync(RespOrderModel entity)
        {
            try
            {
                //var newAddressId = "";
                //var lastAddressId = context.Addresses.OrderByDescending(a => a.AddressId).Select(a => a.AddressId).FirstOrDefault();
                //if (lastAddressId != null)
                //{

                //    var numericPart = int.Parse(lastAddressId.Substring(3));                     
                //    numericPart++;
                //    // Format the incremented numeric part back into the address_id format
                //     newAddressId = $"AD_{numericPart:D3}"; // Assuming you want numeric part to have at least 3 digits

                //}
                var existingaddress = await context.Addresses.FindAsync(entity.Address.Id);



                var neworders = new Order()
                {
                    FacilityId = entity.FacilityId,
                    UserId = entity.UserId,
                    DiscountAmount = entity.DiscountAmount,
                    Incoterms = entity.Incoterms,
                    OrderDate = entity.OrderDate,
                    OrderStatus = entity.OrderStatus,
                    OrderTotal = entity.OrderTotal,
                    TaxAmount = entity.TaxAmount,
                    TermsConditionsId = entity.TermsConditionsId,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = DateTime.UtcNow,
                    Isdelete = entity.Isdelete
                };
                context.Orders.Add(neworders);
                await context.SaveChangesAsync();
                for (int i = 0; i < entity.OrderofItems.Count; i++)
                {
                    var neworditem = new OrderItem()
                    {
                        OrderItemDesc = entity.OrderofItems[i].OrderItemDesc,
                        OrderId = neworders.Id,
                        OrderItemStatus = entity.OrderofItems[i].OrderItemStatus,
                        ProductId = entity.OrderofItems[i].ProductId,
                        Isdelete = false,
                        Quantity = entity.OrderofItems[i].Quantity,
                        UnitPrice = entity.OrderofItems[i].UnitPrice,
                        CreatedBy = entity.CreatedBy,
                        CreatedDate = entity.CreatedDate,
                        UpdatedBy = entity.UpdatedBy,
                        UpdatedDate = DateTime.UtcNow,
                    };
                    context.OrderItems.Add(neworditem);
                    await context.SaveChangesAsync();
                }

                if (existingaddress != null)
                {
                    var neworderaddress = new Address()
                    {
                        Line1 = entity.Address.Line1,
                        Line2 = entity.Address.Line2,
                        Suite = entity.Address.Suite,
                        CountryId = entity.Address.Countryid,
                        CountyId = entity.Address.Countyid,
                        StateId = entity.Address.Stateid,
                        CityId = entity.Address.Cityid,
                        ZipCode = entity.Address.ZipCode,
                        CreatedBy = entity.CreatedBy,
                        CreatedDate = entity.CreatedDate,
                        UpdatedBy = entity.UpdatedBy,
                        UpdatedDate = DateTime.UtcNow,                        
                    };
                    context.Addresses.Update(neworderaddress);
                    await context.SaveChangesAsync();
                }
                
                var newshippment = new Shipment()
                {
                    ShipmentDate = entity.Shiping.ShipmentDate.Value.ToUniversalTime(),
                    ExpectedDeliveryDate = entity.Shiping.Expecteddeliverydate.Value.ToUniversalTime(),
                    PackageSize = entity.Shiping.PackageSize,
                    PakegeLength = entity.Shiping.PackageLength,
                    PakegeWidth = entity.Shiping.PackageWidth,
                    PakegeHeight = entity.Shiping.PackageHeight,
                    SizeUnitOfMesure = entity.Shiping.SizeUnitofMesure,
                    WeightUnitOfMeasure = entity.Shiping.WeightUnitofMeasure,
                    TypeOfPackagingMaterial = entity.Shiping.TypeofPackagingMaterial,
                    TypeOfPackage = entity.Shiping.TypeofPackage,
                    StoringTemparture = entity.Shiping.Storingtemparature,
                    TemperatureUnitOfMeasure = entity.Shiping.TemperatureUnitofmeasure,
                    NoOfPackages = entity.Shiping.NoofPackages,
                    TrackingNumber = entity.Shiping.TrackingNumber,
                    ReceiverId = entity.Shiping.RecieverId,
                    ReceivingHours = entity.Shiping.RecievingHours,
                    IsSignatureNeeded = entity.Shiping.IsSignatureneeded,
                    Isdelete = entity.Isdelete,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate,
                    ShipmentAddressId = entity.Address.Id,
                    OrderId=neworders.Id
                };
                context.Shipments.Add(newshippment);
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(neworders.Id.ToString(), "Order created successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while creating the Order.");
            }

        }
        public async Task<ApiResponse<UserAddressModel>> GetAddressbyUserid(Guid userid)
        {
            try
            {

                var useraddress = await context.Addresses.
                                 Join(context.EntityAddresses, ad => ad.Id, ea => ea.Addressid, (ad, ea) => new { address = ad, entityaddress = ea }).
                                 Join(context.Users, e => e.entityaddress.EntityId, u => u.Id, (e, u) => new { e.address, e.entityaddress, users = u }).
                                 Join(context.Countries, a => a.address.CountryId, c => c.Id, (a, c) => new { a.address, a.entityaddress, a.users,country=c }).
                                 Join(context.States, a => a.address.StateId, s => s.Id, (a, s) => new { a.address, a.entityaddress, a.users,a.country,state=s }).
                                 Join(context.Counties, a => a.address.CountyId, cu => cu.Id, (a, cu) => new { a.address, a.entityaddress, a.users, a.country,a.state,county=cu}).
                                 Join(context.Cities, a => a.address.CityId, ct => ct.Id, (a, ct) => new { a.address, a.entityaddress, a.users, a.country,a.state,a.county,city=ct }).
                                 Where(i => i.users.Id == userid).Select(i => new UserAddressModel
                                 { 
                                     id=i.address.Id,
                                     Addressid=i.address.AddressId,
                                     Cityname=i.city.CityName,
                                     Countyname=i.county.CountyName,
                                     Statename=i.state.StateName,
                                     Countryname=i.country.CountryName,
                                     Cityid=i.address.CityId,
                                     Countryid=i.address.CountryId,
                                     Countyid=i.address.CountyId,
                                     Stateid = i.address.StateId,
                                     Line1 = i.address.Line1,
                                     Line2 = i.address.Line2,
                                     Suite = i.address.Suite,
                                     ZipCode = i.address.ZipCode
                                 }).FirstOrDefaultAsync();
                if (useraddress != null)
                {
                    return  ApiResponse<UserAddressModel>.Success(useraddress, "User address fetched successfully."); ;
                }
                else
                {
                    return  ApiResponse<UserAddressModel>.Fail("Address not found for the given user ID");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}, Stack trace: {ex.StackTrace}");
                return ApiResponse<UserAddressModel>.Fail("An error occurred while fetching Address details.");
            }
        
        }
        public async Task<ApiResponse<ShipmentAddressModel>> GetAddressbyOrderid(Guid orderid)
        {
            try
            { 
            var shipmentaddress = await context.Addresses.
                                 Join(context.Shipments, ad => ad.Id, s => s.ShipmentAddressId, (ad, s) => new { address = ad, shipment = s }).
                                 Join(context.Orders, s => s.shipment.OrderId, o => o.Id, (s, o) => new { s.address, s.shipment, orders = o }).
                                 Join(context.Users, o => o.orders.UserId, u => u.Id, (o, u) => new { o.address, o.shipment, o.orders, users = u }).
                                 Join(context.Countries, a => a.address.CountryId, c => c.Id, (a, c) => new { a.address, a.shipment,a.orders, a.users, country = c }).
                                 Join(context.States, a => a.address.StateId, s => s.Id, (a, s) => new { a.address, a.shipment, a.orders, a.users, a.country, state = s }).
                                 Join(context.Counties, a => a.address.CountyId, cu => cu.Id, (a, cu) => new { a.address, a.shipment, a.orders, a.users, a.country, a.state, county = cu }).
                                 Join(context.Cities, a => a.address.CityId, ct => ct.Id, (a, ct) => new { a.address, a.shipment, a.orders, a.users, a.country, a.state, a.county, city = ct }).
                                 Where(i => i.orders.Id == orderid).Select(i => new ShipmentAddressModel
                                 { 
                                  username=i.users.UserId,
                                  id=i.orders.Id,
                                  Line1=i.address.Line1,
                                  Line2=i.address.Line2,
                                  Suite=i.address.Suite,
                                  Cityname=i.city.CityName,
                                  Countyname=i.county.CountyName,
                                  Statename=i.state.StateName,
                                  Countryname=i.country.CountryName,
                                  ZipCode=i.address.ZipCode,
                                  Addressid=i.address.AddressId,
                                  Cityid=i.city.Id,
                                  Countyid=i.county.Id,
                                  Stateid=i.state.Id,
                                  Countryid=i.country.Id,
                                  ShipmentDate=i.shipment.ShipmentDate.ToString(),
                                 }).FirstOrDefaultAsync();
                if (shipmentaddress != null)
                {
                    return ApiResponse<ShipmentAddressModel>.Success(shipmentaddress, "Shippment address fetched successfully."); ;
                }
                else
                {
                    return ApiResponse<ShipmentAddressModel>.Fail("Address not found for the given order ID");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}, Stack trace: {ex.StackTrace}");
                return ApiResponse<ShipmentAddressModel>.Fail("An error occurred while fetching Address details.");
            }
        }
        public async Task<ApiResponse<IEnumerable<OrderItemsmodel>>> GetOrderdetailsbyOrderid(Guid orderid)
        {
            try
            {
                var itemslist = new List<OrderItemsmodel>();
                var orderitems = await context.OrderItems.
                                    Join(context.Shipments, oi => oi.OrderId, s => s.OrderId, (oi, s) => new { orderitems = oi, shipment = s }).
                                    Join(context.Orders, s => s.orderitems.OrderId, o => o.Id, (s, o) => new { orderitems = s.orderitems,shipment=s.shipment,orders=o }).
                                    Where(i => i.orderitems.OrderId == orderid).Select(i => new OrderItemsmodel
                                    {
                                        id = i.orderitems.OrderId,
                                        itemid = i.orderitems.Id,
                                        quantity = i.orderitems.Quantity,
                                        orderitemdesc = i.orderitems.OrderItemDesc,
                                        typeofpackage = i.shipment.TypeOfPackage,
                                        unitprice = i.orderitems.UnitPrice,
                                        DiscountAmount=i.orders.DiscountAmount,
                                        TaxAmount=i.orders.TaxAmount,

                                    }).ToListAsync();
                Parallel.ForEach(orderitems, async i =>
                {
                    var model = new OrderItemsmodel()
                    {
                        id = i.id,
                        itemid = i.itemid,
                        orderitemdesc = i.orderitemdesc,
                        quantity = i.quantity,
                        typeofpackage = i.typeofpackage,
                        unitprice = i.unitprice,
                        TaxAmount=i.TaxAmount,
                        DiscountAmount=i.DiscountAmount
                    };
                    itemslist.Add(model);
                });
                Task.WhenAll();
                if (orderitems != null)
                {
                    return ApiResponse<IEnumerable<OrderItemsmodel>>.Success(orderitems, "OrderItems fetched successfully."); ;
                }
                else
                {
                    return ApiResponse<IEnumerable<OrderItemsmodel>>.Fail("OrderItems not found for the given user ID");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}, Stack trace: {ex.StackTrace}");
                return ApiResponse<IEnumerable<OrderItemsmodel>>.Fail("An error occurred while fetching OrderItems details.");
            }
        }
    }
}
