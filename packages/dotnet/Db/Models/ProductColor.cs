namespace Db.Models;

public class ProductColor
{
  public string Id { get; set; } = Guid.NewGuid().ToString();
  public string ProductId { get; set; } = null!;
  public Color Color { get; set; }
  public DateTime DateCreated { get; set; } = DateTime.UtcNow;
  public DateTime? DateUpdated { get; set; }

  // Navigation properties
  public Product Product { get; set; } = null!;
}
