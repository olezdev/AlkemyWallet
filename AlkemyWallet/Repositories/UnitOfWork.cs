using AlkemyWallet.DataAccess;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;

namespace AlkemyWallet.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly WalletDbContext _dbContext;

    private readonly IRepositoryBase<Role> _roleRepository;
    private readonly IRepositoryBase<User> _userRepository;
    private readonly IRepositoryBase<Account> _accountRepository;
    private readonly IRepositoryBase<Transaction> _transactionRepository;

    public IRepositoryBase<Role> RoleRepository =>
        _roleRepository ?? new RepositoryBase<Role>(_dbContext);

    public IRepositoryBase<User> UserRepository => 
        _userRepository ?? new RepositoryBase<User>(_dbContext);

    public IRepositoryBase<Account> AccountRepository =>
        _accountRepository ?? new RepositoryBase<Account>(_dbContext);

    public IRepositoryBase<Transaction> TransactionRepository =>
        _transactionRepository ?? new RepositoryBase<Transaction>(_dbContext);

    public UnitOfWork(WalletDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void SaveChanges() =>
        _dbContext.SaveChanges();

    public Task SaveChangesAsync() =>
        _dbContext.SaveChangesAsync();
}
