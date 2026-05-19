using LuckyDay.Api.Models;
using LuckyDay.Db.Models;
using LuckyDay.Db.Repositories;

namespace LuckyDay.Api.Services;

public class ProductService(IUnitOfWork uow)
{
  public async Task<IEnumerable<ProductResponseModel>> GetAllAsync()
  {
    var products = await uow.Products.GetAllAsync();
    return products.Select(ToResponse);
  }

  public async Task<ProductResponseModel?> GetByIdAsync(string id)
  {
    var product = await uow.Products.GetProductWithDetailsAsync(id);
    return product is null ? null : ToResponse(product);
  }

  public async Task<ProductResponseModel> CreateAsync(ProductCreateModel model)
  {
    var product = new Product
    {
      Name = model.Name,
      Description = model.Description,
      Price = model.Price,
      Gender = model.Gender,
      ProductType = model.ProductType,
      IsActive = model.IsActive
    };
    var createdProduct = await uow.Products.AddAsync(product);
    return ToResponse(createdProduct);
  }

  public async Task<ProductResponseModel?> UpdateAsync(string id, ProductUpdateModel model)
  {
    var product = await uow.Products.GetByEmailAsync(id);
    if (product is null)
    {
      return null;
    }
    product.Name = model.Name;
    product.Description = model.Description;
    product.Price = model.Price;
    product.Gender = model.Gender;
    product.ProductType = model.ProductType;
    product.IsActive = model.IsActive;
    product.DateUpdated = DateTime.UtcNow;
    var updatedProduct = await uow.Products.UpdateAsync(product);
    return ToResponse(updatedProduct);
  }

  public async Task<bool> DeleteAsync(string id)
  {
    if (!await uow.Products.ExistsAsync(id))
    {
      return false;
    }
    await uow.Products.DeleteAsync(id);
    return true;
  }

  private static ProductResponseModel ToResponse(Product product)
  {
    return new ProductResponseModel(
      product.Id,
      product.Name,
      product.Description,
      product.Price,
      product.Gender,
      product.ProductType,
      product.IsActive,
      product.DateCreated,
      product.DateUpdated);
  }
}
