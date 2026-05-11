namespace Db.Seeding;

public class UserSeed
{
  public static async Task SeedUsersAsync(LuckyDayDbContext context)
  {
    if (context.Users.Any())
    {
      return;
    }

    var faker = new Faker();
    var users = new List<User>();

    // Create admin user
    users.Add(new User
    {
      Id = Guid.NewGuid().ToString(),
      FirstName = "Admin",
      LastName = "User",
      Gender = Gender.MALE,
      Email = "admin@fu.com",
      StreetAddress = "123 Admin St",
      City = "St. Augustine",
      StateId = StateSeed.GetRandomStateId(context),
      Zip = "32084",
      Phone = "123-456-7890",
      JobTitle = "Administrator",
      ImageId = 0,
      IsActive = true
    });

    // Create regular users
    for (int i = 0; i < 10; i++)
    {
      var gender = faker.PickRandom<Gender>();
      users.Add(new User
      {
        Id = Guid.NewGuid().ToString(),
        FirstName = faker.Name.FirstName(),
        LastName = faker.Name.LastName(),
        Gender = gender,
        Email = faker.Internet.Email(),
        StreetAddress = faker.Address.StreetAddress(),
        StreetAddress2 = faker.Address.SecondaryAddress(),
        City = faker.Address.City(),
        StateId = StateSeed.GetRandomStateId(context),
        Zip = faker.Address.ZipCode("#####"),
        Phone = faker.Phone.PhoneNumber("+1 (###) ###-####"),
        JobTitle = faker.Name.JobTitle(),
        ImageId = faker.Random.Int(1, 99),
        IsActive = faker.Random.Bool(0.8f)
      });
    }

    // Create sales users
    for (int i = 0; i < 10; i++)
    {
      users.Add(new User
      {
        Id = Guid.NewGuid().ToString(),
        FirstName = faker.Name.FirstName(),
        LastName = faker.Name.LastName(),
        Gender = faker.PickRandom<Gender>(),
        Email = faker.Internet.Email(),
        StreetAddress = faker.Address.StreetAddress(),
        StreetAddress2 = faker.Address.SecondaryAddress(),
        City = faker.Address.City(),
        StateId = StateSeed.GetRandomStateId(context),
        Zip = faker.Address.ZipCode("#####"),
        Phone = faker.Phone.PhoneNumber("+1 (###) ###-####"),
        JobTitle = faker.Name.JobTitle(),
        ImageId = faker.Random.Int(1, 99),
        IsActive = faker.Random.Bool(0.8f)
      });
    }

    // Create accounting users
    for (int i = 0; i < 5; i++)
    {
      users.Add(new User
      {
        Id = Guid.NewGuid().ToString(),
        FirstName = faker.Name.FirstName(),
        LastName = faker.Name.LastName(),
        Gender = faker.PickRandom<Gender>(),
        Email = faker.Internet.Email(),
        StreetAddress = faker.Address.StreetAddress(),
        StreetAddress2 = faker.Address.SecondaryAddress(),
        City = faker.Address.City(),
        StateId = StateSeed.GetRandomStateId(context),
        Zip = faker.Address.ZipCode("#####"),
        Phone = faker.Phone.PhoneNumber("+1 (###) ###-####"),
        JobTitle = faker.Name.JobTitle(),
        ImageId = faker.Random.Int(1, 99),
        IsActive = faker.Random.Bool(0.8f)
      });
    }

    await context.Users.AddRangeAsync(users);
    await context.SaveChangesAsync();
  }
}
