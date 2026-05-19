namespace LuckyDay.Db.Seeding;

public static class UserDashboardSeed
{
  public static async Task SeedUserDashboardsAsync(LuckyDayDbContext context)
  {
    if (await context.Set<UserDashboard>().AnyAsync())
    {
      return;
    }

    var users = await context.Users.ToListAsync();
    var adminUser = users.First(u => u.FirstName == "Admin");
    var secondaryUser = users.First(u => u.Id != adminUser.Id);

    var userDashboards = new List<UserDashboard>
    {
      new()
      {
        Id = Guid.NewGuid().ToString(),
        UserId = adminUser.Id,
        DashboardWidgetId = DashboardWidgetSeedIds.TopUserSales,
        Position = 1
      },
      new()
      {
        Id = Guid.NewGuid().ToString(),
        UserId = secondaryUser.Id,
        DashboardWidgetId = DashboardWidgetSeedIds.TopUserSales,
        Position = 1
      }
    };

    await context.Set<UserDashboard>().AddRangeAsync(userDashboards);
    await context.SaveChangesAsync();
  }
}
