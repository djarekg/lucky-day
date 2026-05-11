namespace Db.Seeding;

public class CustomerSeed
{
  public static async Task SeedCustomersAsync(LuckyDayDbContext context)
  {
    if (context.Customers.Any())
    {
      return;
    }

    var faker = new Faker();
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
        StateId = StateSeed.GetRandomStateId(context),
        Zip = faker.Address.ZipCode("#####"),
        Phone = faker.Phone.PhoneNumber("+1 (###) ###-####"),
        IsActive = faker.Random.Bool(0.8f)
      });
    }

    await context.Customers.AddRangeAsync(customers);
    await context.SaveChangesAsync();
  }
}
