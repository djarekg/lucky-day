namespace LuckyDay.Db.Seeding;

public static class CustomerContactSeed
{
  public static async Task SeedCustomerContactsAsync(LuckyDayDbContext context)
  {
    if (await context.CustomerContacts.AnyAsync())
    {
      return;
    }

    var faker = new Faker();
    var stateIds = await context.States.Select(s => s.Id).ToListAsync();
    var customerContacts = new List<CustomerContact>();
    var customers = await context.Customers.ToListAsync();

    foreach (var customer in customers)
    {
      for (int i = 0; i < 10; i++)
      {
        customerContacts.Add(new CustomerContact
        {
          Id = Guid.NewGuid().ToString(),
          CustomerId = customer.Id,
          FirstName = faker.Name.FirstName(),
          LastName = faker.Name.LastName(),
          Email = faker.Internet.Email(),
          StreetAddress = faker.Address.StreetAddress(),
          StreetAddress2 = faker.Address.SecondaryAddress(),
          City = faker.Address.City(),
          StateId = faker.PickRandom(stateIds),
          Zip = faker.Address.ZipCode("#####"),
          Phone = faker.Phone.PhoneNumber("+1 (###) ###-####"),
          ImageId = faker.Random.Int(1, 99),
          IsActive = faker.Random.Bool(0.8f)
        });
      }
    }

    await context.CustomerContacts.AddRangeAsync(customerContacts);
    await context.SaveChangesAsync();
  }
}
