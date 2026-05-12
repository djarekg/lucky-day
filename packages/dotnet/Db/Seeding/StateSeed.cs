namespace Db.Seeding;

public static class StateSeed
{
  public static async Task SeedStatesAsync(LuckyDayDbContext context)
  {
    if (await context.States.AnyAsync())
    {
      return;
    }

    var states = new List<State>
        {
            new() { Id = Guid.NewGuid().ToString(), Name = "Alabama", Code = "AL" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Alaska", Code = "AK" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Arizona", Code = "AZ" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Arkansas", Code = "AR" },
            new() { Id = Guid.NewGuid().ToString(), Name = "California", Code = "CA" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Colorado", Code = "CO" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Connecticut", Code = "CT" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Delaware", Code = "DE" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Florida", Code = "FL" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Georgia", Code = "GA" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Hawaii", Code = "HI" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Idaho", Code = "ID" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Illinois", Code = "IL" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Indiana", Code = "IN" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Iowa", Code = "IA" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Kansas", Code = "KS" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Kentucky", Code = "KY" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Louisiana", Code = "LA" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Maine", Code = "ME" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Maryland", Code = "MD" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Massachusetts", Code = "MA" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Michigan", Code = "MI" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Minnesota", Code = "MN" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Mississippi", Code = "MS" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Missouri", Code = "MO" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Montana", Code = "MT" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Nebraska", Code = "NE" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Nevada", Code = "NV" },
            new() { Id = Guid.NewGuid().ToString(), Name = "New Hampshire", Code = "NH" },
            new() { Id = Guid.NewGuid().ToString(), Name = "New Jersey", Code = "NJ" },
            new() { Id = Guid.NewGuid().ToString(), Name = "New Mexico", Code = "NM" },
            new() { Id = Guid.NewGuid().ToString(), Name = "New York", Code = "NY" },
            new() { Id = Guid.NewGuid().ToString(), Name = "North Carolina", Code = "NC" },
            new() { Id = Guid.NewGuid().ToString(), Name = "North Dakota", Code = "ND" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Ohio", Code = "OH" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Oklahoma", Code = "OK" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Oregon", Code = "OR" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Pennsylvania", Code = "PA" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Rhode Island", Code = "RI" },
            new() { Id = Guid.NewGuid().ToString(), Name = "South Carolina", Code = "SC" },
            new() { Id = Guid.NewGuid().ToString(), Name = "South Dakota", Code = "SD" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Tennessee", Code = "TN" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Texas", Code = "TX" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Utah", Code = "UT" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Vermont", Code = "VT" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Virginia", Code = "VA" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Washington", Code = "WA" },
            new() { Id = Guid.NewGuid().ToString(), Name = "West Virginia", Code = "WV" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Wisconsin", Code = "WI" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Wyoming", Code = "WY" }
        };

    await context.States.AddRangeAsync(states);
    await context.SaveChangesAsync();
  }

  public static async Task<string> GetRandomStateIdAsync(LuckyDayDbContext context)
  {
    var stateCount = await context.States.CountAsync();
    var randomIndex = Random.Shared.Next(0, stateCount);
    return await context.States.Skip(randomIndex).Select(s => s.Id).FirstAsync();
  }
}
