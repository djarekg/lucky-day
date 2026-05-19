namespace LuckyDay.Db.Repositories;

public interface IUnitOfWork : IDisposable
{
  IUserRepository Users { get; }
  IProductRepository Products { get; }
  ICustomerRepository Customers { get; }
  IStateRepository States { get; }
  IUserCredentialRepository UserCredentials { get; }
  ICustomerContactRepository CustomerContacts { get; }
  IProductColorRepository ProductColors { get; }
  IProductInventoryRepository ProductInventories { get; }
  IProductSaleRepository ProductSales { get; }
  ITokenRevocationRepository TokenRevocations { get; }
  ISearchRepository Search { get; }
  IDashboardWidgetRepository DashboardWidgets { get; }
  IUserDashboardRepository UserDashboards { get; }

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
  private IStateRepository? _stateRepository;
  private IUserCredentialRepository? _userCredentialRepository;
  private ICustomerContactRepository? _customerContactRepository;
  private IProductColorRepository? _productColorRepository;
  private IProductInventoryRepository? _productInventoryRepository;
  private IProductSaleRepository? _productSaleRepository;
  private ITokenRevocationRepository? _tokenRevocationRepository;
  private ISearchRepository? _searchRepository;
  private IDashboardWidgetRepository? _dashboardWidgetRepository;
  private IUserDashboardRepository? _userDashboardRepository;

  public IUserRepository Users => _userRepository ??= new UserRepository(_context);
  public IProductRepository Products => _productRepository ??= new ProductRepository(_context);
  public ICustomerRepository Customers => _customerRepository ??= new CustomerRepository(_context);
  public IStateRepository States => _stateRepository ??= new StateRepository(_context);
  public IUserCredentialRepository UserCredentials => _userCredentialRepository ??= new UserCredentialRepository(_context);
  public ICustomerContactRepository CustomerContacts => _customerContactRepository ??= new CustomerContactRepository(_context);
  public IProductColorRepository ProductColors => _productColorRepository ??= new ProductColorRepository(_context);
  public IProductInventoryRepository ProductInventories => _productInventoryRepository ??= new ProductInventoryRepository(_context);
  public IProductSaleRepository ProductSales => _productSaleRepository ??= new ProductSaleRepository(_context);
  public ITokenRevocationRepository TokenRevocations => _tokenRevocationRepository ??= new TokenRevocationRepository(_context);
  public ISearchRepository Search => _searchRepository ??= new SearchRepository(_context);
  public IDashboardWidgetRepository DashboardWidgets => _dashboardWidgetRepository ??= new DashboardWidgetRepository(_context);
  public IUserDashboardRepository UserDashboards => _userDashboardRepository ??= new UserDashboardRepository(_context);

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
    _context.Dispose();
  }
}

