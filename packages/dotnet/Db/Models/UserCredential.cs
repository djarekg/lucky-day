namespace Db.Models;

public class UserCredential
{
  public string Id { get; set; } = Guid.NewGuid().ToString();
  public string UserId { get; set; } = null!;
  public string Password { get; set; } = null!;
  public Role Role { get; set; }

  // Navigation properties
  public User User { get; set; } = null!;
}
