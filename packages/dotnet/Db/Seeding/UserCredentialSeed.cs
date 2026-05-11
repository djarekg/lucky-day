namespace Db.Seeding;

public class UserCredentialSeed
{
  public static async Task SeedUserCredentialsAsync(LuckyDayDbContext context)
  {
    if (context.UserCredentials.Any())
    {
      return;
    }

    var faker = new Faker();
    var credentials = new List<UserCredential>();

    // Get all users
    var users = context.Users.ToList();

    // Create credential for admin user
    var adminUser = users.First(u => u.FirstName == "Admin");
    credentials.Add(new UserCredential
    {
      Id = Guid.NewGuid().ToString(),
      UserId = adminUser.Id,
      Password = "admin123", // In production, this should be hashed
      Role = Role.ADMIN
    });

    // Create credentials for other users with random roles
    var remainingUsers = users.Skip(1).ToList();
    foreach (var user in remainingUsers)
    {
      credentials.Add(new UserCredential
      {
        Id = Guid.NewGuid().ToString(),
        UserId = user.Id,
        Password = faker.Internet.Password(), // In production, this should be hashed
        Role = faker.PickRandom<Role>()
      });
    }

    await context.UserCredentials.AddRangeAsync(credentials);
    await context.SaveChangesAsync();
  }
}
