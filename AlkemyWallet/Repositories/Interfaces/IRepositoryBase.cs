using AlkemyWallet.Entities;
using System.Linq.Expressions;

namespace AlkemyWallet.Repositories.Interfaces;

public interface IRepositoryBase<T> where T : EntityBase
{
    Task<T> AddAsync(T entity);
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(int id);
    Task<T> ExpressionGetAsync(
        Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = "");
}