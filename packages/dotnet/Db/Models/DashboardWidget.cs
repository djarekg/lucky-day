namespace LuckyDay.Db.Models;

public class DashboardWidget
{
  public string Id { get; set; } = Guid.NewGuid().ToString();
  public string Name { get; set; } = null!;
  public DashboardWidgetCategory Category { get; set; }
  public DashboardWidgetType Type { get; set; }

  // Navigation properties
  public ICollection<UserDashboard> UserDashboards { get; set; } = [];
}
