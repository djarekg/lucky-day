namespace LuckyDay.Db.Models;

public class User
{
  public string Id { get; set; } = Guid.NewGuid().ToString();
  public string FirstName { get; set; } = null!;
  public string LastName { get; set; } = null!;
  public Gender Gender { get; set; }
  public string Email { get; set; } = null!;
  public string StreetAddress { get; set; } = null!;
  public string? StreetAddress2 { get; set; }
  public string City { get; set; } = null!;
  public string StateId { get; set; } = null!;
  public string Zip { get; set; } = null!;
  public string Phone { get; set; } = null!;
  public string JobTitle { get; set; } = null!;
  public int ImageId { get; set; }
  public bool IsActive { get; set; } = false;
  public DateTime DateCreated { get; set; } = DateTime.UtcNow;
  public DateTime? DateUpdated { get; set; }

  // Navigation properties
  public State State { get; set; } = null!;
  public UserCredential? UserCredential { get; set; }
  public ICollection<ProductSale> ProductSales { get; set; } = [];
  public ICollection<UserDashboard> UserDashboards { get; set; } = [];
}
