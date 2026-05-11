namespace Db.Seeding;

public class ProductSeed
{
  public static async Task SeedProductsAsync(LuckyDayDbContext context)
  {
    if (context.Products.Any())
    {
      return;
    }

    var faker = new Faker();
    var products = new List<Product>();

    var productTypes = Enum.GetValues(typeof(ProductType)).Cast<ProductType>().ToList();
    var genders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();

    // Create multiple products for each type and gender combination
    foreach (var productType in productTypes)
    {
      foreach (var gender in genders)
      {
        for (int i = 0; i < 2; i++)
        {
          products.Add(new Product
          {
            Id = Guid.NewGuid().ToString(),
            Name = $"{faker.Commerce.ProductAdjective()} {productType.ToString().ToLower()}",
            Description = faker.Commerce.ProductDescription(),
            Price = faker.Commerce.Price(10, 150),
            ProductType = productType,
            Gender = gender,
            IsActive = true
          });
        }
      }
    }

    await context.Products.AddRangeAsync(products);
    await context.SaveChangesAsync();
  }
}
