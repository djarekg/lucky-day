namespace Db.Seeding;

public class CustomerContactSeed
{
  public static async Task SeedCustomerContactsAsync(LuckyDayDbContext context)
  {
    if (context.CustomerContacts.Any())
    {
      return;
    }

    var faker = new Faker();
    var customerContacts = new List<CustomerContact>();
    var customers = context.Customers.ToList();

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
          StateId = StateSeed.GetRandomStateId(context),
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
