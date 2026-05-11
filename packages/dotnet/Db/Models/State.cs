namespace Db.Models;

public class State
{
  public string Id { get; set; } = Guid.NewGuid().ToString();
  public string Name { get; set; } = null!;
  public string Code { get; set; } = null!;

  // Navigation properties
  public ICollection<User> Users { get; set; } = new List<User>();
  public ICollection<Customer> Customers { get; set; } = new List<Customer>();
  public ICollection<CustomerContact> CustomerContacts { get; set; } = new List<CustomerContact>();
}
