namespace LuckyDay.Db.Repositories;

public interface IProductColorRepository : IRepository<ProductColor>
{
  Task<IEnumerable<ProductColor>> GetByProductIdAsync(string productId);
}
