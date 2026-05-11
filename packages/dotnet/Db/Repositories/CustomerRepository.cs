namespace Db.Repositories;

public interface ICustomerRepository : IRepository<Customer>
{
  Task<Customer?> GetCustomerWithContactsAsync(string id);
  Task<IEnumerable<Customer>> GetActiveCustomersAsync();
}

public class CustomerRepository(LuckyDayDbContext context) : Repository<Customer>(context), ICustomerRepository
{
  public async Task<Customer?> GetCustomerWithContactsAsync(string id)
  {
    return await _dbSet
        .Include(c => c.CustomerContacts)
        .FirstOrDefaultAsync(c => c.Id == id);
  }

  public async Task<IEnumerable<Customer>> GetActiveCustomersAsync()
  {
    return await _dbSet.Where(c => c.IsActive).ToListAsync();
  }
}
