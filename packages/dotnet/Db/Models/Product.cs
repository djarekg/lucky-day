namespace Db.Models;

public class Product
{
  public string Id { get; set; } = Guid.NewGuid().ToString();
  public string Name { get; set; } = null!;
  public string Description { get; set; } = null!;
  public string Price { get; set; } = null!;
  public Gender Gender { get; set; }
  public ProductType ProductType { get; set; }
  public bool IsActive { get; set; } = false;
  public DateTime DateCreated { get; set; } = DateTime.UtcNow;
  public DateTime? DateUpdated { get; set; }

  // Navigation properties
  public ICollection<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();
  public ICollection<ProductSale> ProductSales { get; set; } = new List<ProductSale>();
  public ICollection<ProductColor> ProductColors { get; set; } = new List<ProductColor>();
}
