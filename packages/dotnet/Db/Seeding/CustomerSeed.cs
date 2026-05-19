namespace LuckyDay.Db.Seeding;

public static class CustomerSeed
{
  public static async Task SeedCustomersAsync(LuckyDayDbContext context)
  {
    if (await context.Customers.AnyAsync())
    {
      return;
    }

    var faker = new Faker();
    var stateIds = await context.States.Select(s => s.Id).ToListAsync();
    var customers = new List<Customer>();

    for (int i = 0; i < 120; i++)
    {
      customers.Add(new Customer
      {
        Id = Guid.NewGuid().ToString(),
        Name = faker.Company.CompanyName(),
        StreetAddress = faker.Address.StreetAddress(),
        StreetAddress2 = faker.Address.SecondaryAddress(),
        City = faker.Address.City(),
        StateId = faker.PickRandom(stateIds),
        Zip = faker.Address.ZipCode("#####"),
        Phone = faker.Phone.PhoneNumber("+1 (###) ###-####"),
        IsActive = faker.Random.Bool(0.8f)
      });
    }

    await context.Customers.AddRangeAsync(customers);
    await context.SaveChangesAsync();
  }
}
