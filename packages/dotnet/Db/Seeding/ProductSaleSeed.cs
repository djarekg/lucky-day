namespace Db.Seeding;

public class ProductSaleSeed
{
  public static async Task SeedProductSalesAsync(LuckyDayDbContext context)
  {
    if (context.ProductSales.Any())
    {
      return;
    }

    var faker = new Faker();
    var productSales = new List<ProductSale>();

    var products = context.Products.ToList();
    var customers = context.Customers.ToList();
    var users = context.Users.ToList();

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
