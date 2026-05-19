namespace LuckyDay.Db.Models;

public class ProductInventory
{
  public string Id { get; set; } = Guid.NewGuid().ToString();
  public string ProductId { get; set; } = null!;
  public Size Size { get; set; }
  public int Quantity { get; set; }
  public DateTime DateCreated { get; set; } = DateTime.UtcNow;
  public DateTime? DateUpdated { get; set; }

  // Navigation properties
  public Product Product { get; set; } = null!;
}
