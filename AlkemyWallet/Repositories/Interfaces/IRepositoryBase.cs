﻿using AlkemyWallet.Entities;

namespace AlkemyWallet.Repositories.Interfaces;

public interface IRepositoryBase<T> where T : EntityBase
{
    Task<T> AddAsync(T entity);
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(int id);
}