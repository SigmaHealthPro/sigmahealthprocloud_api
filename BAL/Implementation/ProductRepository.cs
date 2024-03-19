using BAL.Constant;
using BAL.Pagination;
using BAL.Repository;
using BAL.RequestModels;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Implementation
{
    public class ProductRepository:IGenericRepository<Product>, IProductRepository
    {
        private SigmaproIisContext context;
        private ILogger<UnitOfWork> _logger;
        private readonly string _corelationId = string.Empty;
        public ProductRepository(SigmaproIisContext _context, ILogger<UnitOfWork> logger)
        {
            context = _context;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> Find(Expression<Func<Product, bool>> predicate)
        {
            return await context.Products.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var productlist = new List<Product>();

            var products = await context.Set<Product>().ToListAsync();
            foreach (var p in products)
            {
                var product = new Product()
                {
                    Id = p.Id,
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    CvxCodeId = p.CvxCodeId,
                    MvxCodeId = p.MvxCodeId                   
                    
                };
                productlist.Add(product);
            }
            return productlist;

        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await context.Set<Product>().FindAsync(id);
        }

        public async Task<ApiResponse<string>> InsertAsync(Product entity)
        {
            try
            {
                await context.Set<Product>().AddAsync(entity);
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(null, "Product inserted successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while Inserting the Product.");
            }
        }

        public async Task<ApiResponse<string>> UpdateAsync(Product entity)
        {
            try
            {
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(null, "Product Updated successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while Updating the Product.");
            }
        }
        public async Task<ApiResponse<string>> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await context.Set<Product>().FindAsync(id);
                if (entity != null)
                {
                    context.Set<Product>().Remove(entity);
                    await context.SaveChangesAsync();
                    return ApiResponse<string>.Success(id.ToString(), "Product deleted successfully.");
                }

                return ApiResponse<string>.Fail("Product with the given ID not found.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(DeleteAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("Product with the given ID not found.");
            }
        }

        public async Task<PaginationModel<VaccinesModel>> GetAllVaccinesbyfacilityid(Guid facilityid,int pagenumber,int pagesize)
        {
            var vaccinelist = new List<VaccinesModel>();

            var vaccines = await context.Products.
                            Join(context.Inventories, p => p.Id.ToString(), i => i.ProductId.Value.ToString(), (p, i) => new {products = p,inventory=i}).
                            Join(context.Facilities, i => i.inventory.FacilityId.Value.ToString(), f => f.Id.ToString(), (i, f) => new {product=i.products,inventory=i.inventory,facility=f}).
                            Join(context.Cvxes, pr=>pr.product.CvxCodeId.Value.ToString(), cv => cv.Id.ToString(), (pr, cv) => new {product=pr.product,inventory=pr.inventory,facility=pr.facility,cvx=cv}).
                            Join(context.Mvxes, c=>c.product.MvxCodeId.Value.ToString(), mv=>mv.Id.ToString(),(c,mv)=> new {product=c.product,inventory=c.inventory,facility=c.facility,cvx=c.cvx,mvx=mv}).
                            Join(context.VaccinePrices, va=>va.cvx.Id.ToString(), vp=>vp.CvxId.Value.ToString(),(va,vp)=>new {product=va.product,inventory=va.inventory,facility=va.facility,cvx=va.cvx,mvx=va.mvx,vp=vp}).
                            Where(j=>j.facility.Id==facilityid && j.inventory.Isdelete == false).Select(k => new
                            {
                                k.inventory,
                                k.product,
                                k.facility,
                                k.cvx,
                                k.mvx,
                                k.vp
                            }).GroupBy(x => new { x.product.ProductId, x.cvx.Id })
    .Select(group => group.First())
                            .ToListAsync();
            Parallel.ForEach(vaccines, async i =>
            {
                var model = new VaccinesModel()
                {
                    Product = i.product.ProductName,
                    manufacturer = i.mvx.ManufacturerName,
                    Vaccine = i.cvx.CvxDescription,
                    Productid = i.product.Id,
                    Vaccineid = i.cvx.Id,
                    manufacturerid=i.mvx.Id,
                    inventoryid=i.inventory.Id,
                    price=i.vp.CostPerDose

                };
                vaccinelist.Add(model);
 
            });
            Task.WhenAll();
            long? totalRows = vaccinelist.Count();
            var response = vaccinelist.Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList();
            return PaginationHelper.Paginate(response, pagenumber, pagesize, Convert.ToInt32(totalRows));
            

        }
    }
}

