namespace LuckyDay.Db.Repositories;

public class ProductSaleRepository(LuckyDayDbContext context) : Repository<ProductSale>(context), IProductSaleRepository
{
  public async Task<IEnumerable<ProductSale>> GetByProductIdAsync(string productId)
  {
    return await _dbSet.Where(ps => ps.ProductId == productId).ToListAsync();
  }

  public async Task<IEnumerable<ProductSale>> GetByCustomerIdAsync(string customerId)
  {
    return await _dbSet.Where(ps => ps.CustomerId == customerId).ToListAsync();
  }

  public async Task<IEnumerable<ProductSale>> GetByUserIdAsync(string userId)
  {
    return await _dbSet.Where(ps => ps.UserId == userId).ToListAsync();
  }

  public async Task<IEnumerable<(string UserId, string FirstName, string LastName, decimal TotalSales)>> GetTopUserSalesByYearAsync(int year, int take)
  {
    return await _dbSet
      .Where(ps => ps.DateCreated.Year == year)
      .GroupBy(ps => new { ps.UserId, ps.User.FirstName, ps.User.LastName })
      .Select(g => new
      {
        g.Key.UserId,
        g.Key.FirstName,
        g.Key.LastName,
        TotalSales = g.Sum(ps => ps.Price * ps.Quantity)
      })
      .OrderByDescending(x => x.TotalSales)
      .Take(take)
      .Select(x => ValueTuple.Create(x.UserId, x.FirstName, x.LastName, x.TotalSales))
      .ToListAsync();
  }
}
