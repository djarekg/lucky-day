namespace Db.Seeding;

public static class UserCredentialSeed
{
  public static async Task SeedUserCredentialsAsync(LuckyDayDbContext context)
  {
    if (await context.UserCredentials.AnyAsync())
    {
      return;
    }

    var faker = new Faker();
    var credentials = new List<UserCredential>();

    // Get all users
    var users = await context.Users.ToListAsync();

    // Create credential for admin user
    var adminUser = users.First(u => u.FirstName == "Admin");
    credentials.Add(new UserCredential
    {
      Id = Guid.NewGuid().ToString(),
      UserId = adminUser.Id,
      Password = "admin123", // In production, this should be hashed
      Role = Role.Admin
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
