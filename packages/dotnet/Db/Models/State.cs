namespace LuckyDay.Db.Models;

public class State
{
  public string Id { get; set; } = Guid.NewGuid().ToString();
  public string Name { get; set; } = null!;
  public string Code { get; set; } = null!;

  // Navigation properties
  public ICollection<User> Users { get; set; } = [];
  public ICollection<Customer> Customers { get; set; } = [];
  public ICollection<CustomerContact> CustomerContacts { get; set; } = [];
}
