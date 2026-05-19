namespace LuckyDay.Db.Repositories;

public interface IProductInventoryRepository : IRepository<ProductInventory>
{
  Task<IEnumerable<ProductInventory>> GetByProductIdAsync(string productId);
}
