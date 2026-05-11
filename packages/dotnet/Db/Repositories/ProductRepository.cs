namespace Db.Repositories;

public interface IProductRepository : IRepository<Product>
{
  Task<Product?> GetProductWithDetailsAsync(string id);
  Task<IEnumerable<Product>> GetProductsByTypeAsync(ProductType productType);
  Task<IEnumerable<Product>> GetActiveProductsAsync();
}

public class ProductRepository(LuckyDayDbContext context) : Repository<Product>(context), Repository<Product>, IProductRepository
{
  public async Task<Product?> GetProductWithDetailsAsync(string id)
  {
    return await _dbSet
        .Include(p => p.ProductColors)
        .Include(p => p.ProductInventories)
        .FirstOrDefaultAsync(p => p.Id == id);
  }

  public async Task<IEnumerable<Product>> GetProductsByTypeAsync(ProductType productType)
  {
    return await _dbSet.Where(p => p.ProductType == productType).ToListAsync();
  }

  public async Task<IEnumerable<Product>> GetActiveProductsAsync()
  {
    return await _dbSet.Where(p => p.IsActive).ToListAsync();
  }
}
