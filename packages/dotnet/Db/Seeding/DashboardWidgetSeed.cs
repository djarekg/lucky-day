namespace LuckyDay.Db.Seeding;

public static class DashboardWidgetSeed
{
  public static async Task SeedDashboardWidgetsAsync(LuckyDayDbContext context)
  {
    var widgets = new List<DashboardWidget>
    {
      new()
      {
        Id = DashboardWidgetSeedIds.TopUserSales,
        Name = "Top User Sales",
        Category = DashboardWidgetCategory.Sales,
        Type = DashboardWidgetType.TotalList
      }
    };

    var existingIds = await context
      .Set<DashboardWidget>()
      .Select(widget => widget.Id)
      .ToHashSetAsync();

    var missingWidgets = widgets
      .Where(widget => !existingIds.Contains(widget.Id))
      .ToList();

    if (missingWidgets.Count == 0)
    {
      return;
    }

    await context.Set<DashboardWidget>().AddRangeAsync(missingWidgets);
    await context.SaveChangesAsync();
  }
}
