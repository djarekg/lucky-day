namespace LuckyDay.Db.Repositories;

public interface ICustomerContactRepository : IRepository<CustomerContact>
{
  Task<IEnumerable<CustomerContact>> GetByCustomerIdAsync(string customerId);
  Task<IEnumerable<CustomerContact>> GetActiveByCustomerIdAsync(string customerId);
}
