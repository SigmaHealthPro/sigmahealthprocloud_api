using BAL.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync( );

        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
        Task<ApiResponse<string>> InsertAsync(T entity);
        Task<ApiResponse<string>> UpdateAsync(T entity);

        Task<ApiResponse<string>> DeleteAsync(Guid id);

    }
}
