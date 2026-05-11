namespace Db.Models;

public class Customer
{
  public string Id { get; set; } = Guid.NewGuid().ToString();
  public string Name { get; set; } = null!;
  public string StreetAddress { get; set; } = null!;
  public string? StreetAddress2 { get; set; }
  public string City { get; set; } = null!;
  public string StateId { get; set; } = null!;
  public string Zip { get; set; } = null!;
  public string Phone { get; set; } = null!;
  public bool IsActive { get; set; } = false;
  public DateTime DateCreated { get; set; } = DateTime.UtcNow;
  public DateTime? DateUpdated { get; set; }

  // Navigation properties
  public State State { get; set; } = null!;
  public ICollection<CustomerContact> CustomerContacts { get; set; } = new List<CustomerContact>();
  public ICollection<ProductSale> ProductSales { get; set; } = new List<ProductSale>();
}
