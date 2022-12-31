using AlkemyWallet.DataAccess;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AlkemyWallet.Repositories;

public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
{
    private readonly WalletDbContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    public RepositoryBase(WalletDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<T>();
    }

    public async Task<T> AddAsync(T entity)
    {
        try
        {
            var result = await _dbSet.AddAsync(entity);
            return result.Entity;
        }
        catch (Exception ex)
        {
            throw new Exception("Error on Repository.Add:" + ex.Message);
        }
    }

    public async Task<List<T>> GetAllAsync()
    {
        try
        {
            return await _dbSet.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Error on Repository.GetAll:" + ex.Message);
        }
    }

    public async Task<T> GetByIdAsync(int id)
    {
        try
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) 
                return null;
            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception("Error on Repository.GetById: " + ex.Message);
        }
    }

    public async Task<T> UpdateAsync(T entity)
    {
        try
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return await Task.FromResult(entity);
        }
        catch (Exception ex)
        {
            throw new Exception("Error on Repository.Update: " + ex.Message);
        }
    }

    public async Task<T> DeleteAsync(int id)
    {
        try
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return null;
            else
            {
                _dbSet.Remove(entity);
                return entity;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error on Repository.Delete: " + ex.Message);
        }
    }

    public async Task<T> ExpressionGetAsync(
        Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = "")
    {
        IQueryable<T> query = _dbSet;

        if (predicate != null)
            query = query.Where(predicate);

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        return await query.FirstOrDefaultAsync();
    }
}
