namespace LuckyDay.Db.Models;

public class UserDashboard
{
  public string Id { get; set; } = Guid.NewGuid().ToString();
  public string UserId { get; set; } = null!;
  public string DashboardWidgetId { get; set; } = null!;
  public int Position { get; set; }

  // Navigation properties
  public User User { get; set; } = null!;
  public DashboardWidget DashboardWidget { get; set; } = null!;
}
