namespace LuckyDay.Db.Repositories;

public class ProductColorRepository(LuckyDayDbContext context) : Repository<ProductColor>(context), IProductColorRepository
{
  public async Task<IEnumerable<ProductColor>> GetByProductIdAsync(string productId)
  {
    return await _dbSet.Where(pc => pc.ProductId == productId).ToListAsync();
  }
}
