namespace LuckyDay.Db.Models;

public class ProductSale
{
  public string Id { get; set; } = Guid.NewGuid().ToString();
  public string ProductId { get; set; } = null!;
  public string CustomerId { get; set; } = null!;
  public string UserId { get; set; } = null!;
  public int Quantity { get; set; }
  public decimal Price { get; set; }
  public DateTime DateCreated { get; set; } = DateTime.UtcNow;
  public DateTime? DateUpdated { get; set; }

  // Navigation properties
  public Product Product { get; set; } = null!;
  public Customer Customer { get; set; } = null!;
  public User User { get; set; } = null!;
}
