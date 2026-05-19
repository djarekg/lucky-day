namespace LuckyDay.Db.Seeding;

public static class ProductColorSeed
{
  public static async Task SeedProductColorsAsync(LuckyDayDbContext context)
  {
    if (await context.ProductColors.AnyAsync())
    {
      return;
    }

    var faker = new Faker();
    var productColors = new List<ProductColor>();
    var products = await context.Products.ToListAsync();
    var colors = Enum.GetValues<Color>();

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
