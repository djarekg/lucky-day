namespace Db.Seeding;

public static class FakerConfiguration
{
  public static Faker<T> CreateFaker<T>(int? seed = null) where T : class
  {
    // Note: Seeding can be done directly on the Faker instance
    // For now, we return a standard faker without explicit seeding
    return new Faker<T>();
  }
}
