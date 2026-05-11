namespace Db.Seeding;

public class ProductColorSeed
{
  public static async Task SeedProductColorsAsync(LuckyDayDbContext context)
  {
    if (context.ProductColors.Any())
    {
      return;
    }

    var faker = new Faker();
    var productColors = new List<ProductColor>();
    var products = context.Products.ToList();
    var colors = Enum.GetValues(typeof(Color)).Cast<Color>().ToList();

    foreach (var product in products)
    {
      var numberOfColors = faker.Random.Int(1, 3);
      var selectedColors = faker.Random.Shuffle(colors).Take(numberOfColors);

      foreach (var color in selectedColors)
      {
        productColors.Add(new ProductColor
        {
          Id = Guid.NewGuid().ToString(),
          ProductId = product.Id,
          Color = color
        });
      }
    }

    await context.ProductColors.AddRangeAsync(productColors);
    await context.SaveChangesAsync();
  }
}
