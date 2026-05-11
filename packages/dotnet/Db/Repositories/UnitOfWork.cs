namespace Db.Repositories;

public interface IUnitOfWork : IDisposable
{
  IUserRepository Users { get; }
  IProductRepository Products { get; }
  ICustomerRepository Customers { get; }
  IRepository<Models.State> States { get; }
  IRepository<Models.UserCredential> UserCredentials { get; }
  IRepository<Models.CustomerContact> CustomerContacts { get; }
  IRepository<Models.ProductColor> ProductColors { get; }
  IRepository<Models.ProductInventory> ProductInventories { get; }
  IRepository<Models.ProductSale> ProductSales { get; }

  Task<int> SaveChangesAsync();
  Task BeginTransactionAsync();
  Task CommitTransactionAsync();
  Task RollbackTransactionAsync();
}

public class UnitOfWork(LuckyDayDbContext context) : IUnitOfWork
{
  private readonly LuckyDayDbContext _context = context;
  private IUserRepository? _userRepository;
  private IProductRepository? _productRepository;
  private ICustomerRepository? _customerRepository;
  private IRepository<Models.State>? _stateRepository;
  private IRepository<Models.UserCredential>? _userCredentialRepository;
  private IRepository<Models.CustomerContact>? _customerContactRepository;
  private IRepository<Models.ProductColor>? _productColorRepository;
  private IRepository<Models.ProductInventory>? _productInventoryRepository;
  private IRepository<Models.ProductSale>? _productSaleRepository;

  public IUserRepository Users => _userRepository ??= new UserRepository(_context);
  public IProductRepository Products => _productRepository ??= new ProductRepository(_context);
  public ICustomerRepository Customers => _customerRepository ??= new CustomerRepository(_context);
  public IRepository<Models.State> States => _stateRepository ??= new Repository<Models.State>(_context);
  public IRepository<Models.UserCredential> UserCredentials => _userCredentialRepository ??= new Repository<Models.UserCredential>(_context);
  public IRepository<Models.CustomerContact> CustomerContacts => _customerContactRepository ??= new Repository<Models.CustomerContact>(_context);
  public IRepository<Models.ProductColor> ProductColors => _productColorRepository ??= new Repository<Models.ProductColor>(_context);
  public IRepository<Models.ProductInventory> ProductInventories => _productInventoryRepository ??= new Repository<Models.ProductInventory>(_context);
  public IRepository<Models.ProductSale> ProductSales => _productSaleRepository ??= new Repository<Models.ProductSale>(_context);

  public async Task<int> SaveChangesAsync()
  {
    return await _context.SaveChangesAsync();
  }

  public async Task BeginTransactionAsync()
  {
    await _context.Database.BeginTransactionAsync();
  }

  public async Task CommitTransactionAsync()
  {
    try
    {
      await _context.SaveChangesAsync();
      await _context.Database.CommitTransactionAsync();
    }
    catch
    {
      await RollbackTransactionAsync();
      throw;
    }
  }

  public async Task RollbackTransactionAsync()
  {
    try
    {
      await _context.Database.RollbackTransactionAsync();
    }
    catch
    {
      // Log the error if needed
    }
  }

  public void Dispose()
  {
    _context?.Dispose();
  }
}
