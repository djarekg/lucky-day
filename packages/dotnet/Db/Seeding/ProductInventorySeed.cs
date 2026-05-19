namespace LuckyDay.Db.Seeding;

public static class ProductInventorySeed
{
  public static async Task SeedProductInventoriesAsync(LuckyDayDbContext context)
  {
    if (await context.ProductInventories.AnyAsync())
    {
      return;
    }

    var faker = new Faker();
    var productInventories = new List<ProductInventory>();
    var products = await context.Products.ToListAsync();
    var sizes = Enum.GetValues<Size>();

    foreach (var product in products)
    {
      var numberOfSizes = faker.Random.Int(2, 5);
      var selectedSizes = faker.Random.Shuffle(sizes).Take(numberOfSizes);

      foreach (var size in selectedSizes)
      {
        productInventories.Add(new ProductInventory
        {
          Id = Guid.NewGuid().ToString(),
          ProductId = product.Id,
          Size = size,
          Quantity = faker.Random.Int(1, 100)
        });
      }
    }

    await context.ProductInventories.AddRangeAsync(productInventories);
    await context.SaveChangesAsync();
  }
}
