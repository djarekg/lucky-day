namespace LuckyDay.Db.Repositories;

public interface IProductSaleRepository : IRepository<ProductSale>
{
  Task<IEnumerable<ProductSale>> GetByProductIdAsync(string productId);
  Task<IEnumerable<ProductSale>> GetByCustomerIdAsync(string customerId);
  Task<IEnumerable<ProductSale>> GetByUserIdAsync(string userId);
  Task<IEnumerable<(string UserId, string FirstName, string LastName, decimal TotalSales)>> GetTopUserSalesByYearAsync(int year, int take);
}
