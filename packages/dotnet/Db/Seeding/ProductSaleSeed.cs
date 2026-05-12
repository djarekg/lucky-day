namespace Db.Seeding;

public static class ProductSaleSeed
{
  public static async Task SeedProductSalesAsync(LuckyDayDbContext context)
  {
    if (await context.ProductSales.AnyAsync())
    {
      return;
    }

    var faker = new Faker();
    var productSales = new List<ProductSale>();

    var products = await context.Products.ToListAsync();
    var customers = await context.Customers.ToListAsync();
    var users = await context.Users.ToListAsync();

    for (int i = 0; i < 1000; i++)
    {
      var product = faker.PickRandom(products);
      var customer = faker.PickRandom(customers);
      var user = faker.PickRandom(users);

      productSales.Add(new ProductSale
      {
        Id = Guid.NewGuid().ToString(),
        ProductId = product.Id,
        CustomerId = customer.Id,
        UserId = user.Id,
        Quantity = faker.Random.Int(1, 10),
        Price = decimal.Parse(product.Price)
      });
    }

    await context.ProductSales.AddRangeAsync(productSales);
    await context.SaveChangesAsync();
  }
}
