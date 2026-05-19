using LuckyDay.Api.Models;
using LuckyDay.Db.Models;
using LuckyDay.Db.Repositories;

namespace LuckyDay.Api.Services;

public class ProductColorService(IUnitOfWork uow)
{
  public async Task<IEnumerable<ProductColorResponseModel>> GetAllAsync()
  {
    var productColors = await uow.ProductColors.GetAllAsync();
    return productColors.Select(ToResponse);
  }

  public async Task<ProductColorResponseModel?> GetByIdAsync(string id)
  {
    var productColor = await uow.ProductColors.GetByEmailAsync(id);
    return productColor is null ? null : ToResponse(productColor);
  }

  public async Task<ProductColorResponseModel> CreateAsync(ProductColorCreateModel model)
  {
    var productColor = new ProductColor
    {
      ProductId = model.ProductId,
      Color = model.Color
    };
    var createdProductColor = await uow.ProductColors.AddAsync(productColor);
    return ToResponse(createdProductColor);
  }

  public async Task<ProductColorResponseModel?> UpdateAsync(string id, ProductColorUpdateModel model)
  {
    var productColor = await uow.ProductColors.GetByEmailAsync(id);
    if (productColor is null)
    {
      return null;
    }
    productColor.ProductId = model.ProductId;
    productColor.Color = model.Color;
    productColor.DateUpdated = DateTime.UtcNow;
    var updatedProductColor = await uow.ProductColors.UpdateAsync(productColor);
    return ToResponse(updatedProductColor);
  }

  public async Task<bool> DeleteAsync(string id)
  {
    if (!await uow.ProductColors.ExistsAsync(id))
    {
      return false;
    }
    await uow.ProductColors.DeleteAsync(id);
    return true;
  }

  private static ProductColorResponseModel ToResponse(ProductColor productColor)
  {
    return new ProductColorResponseModel(
      productColor.Id,
      productColor.ProductId,
      productColor.Color,
      productColor.DateCreated,
      productColor.DateUpdated);
  }
}
