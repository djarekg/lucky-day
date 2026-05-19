namespace LuckyDay.Db.Repositories;

public class ProductInventoryRepository(LuckyDayDbContext context) : Repository<ProductInventory>(context), IProductInventoryRepository
{
  public async Task<IEnumerable<ProductInventory>> GetByProductIdAsync(string productId)
  {
    return await _dbSet.Where(pi => pi.ProductId == productId).ToListAsync();
  }
}
