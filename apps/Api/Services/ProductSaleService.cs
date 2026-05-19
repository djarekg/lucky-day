using LuckyDay.Api.Models;
using LuckyDay.Db.Models;
using LuckyDay.Db.Repositories;

namespace LuckyDay.Api.Services;

public class ProductSaleService(IUnitOfWork uow)
{
  public async Task<IEnumerable<ProductSaleResponseModel>> GetAllAsync()
  {
    var sales = await uow.ProductSales.GetAllAsync();
    return sales.Select(ToResponse);
  }

  public async Task<ProductSaleResponseModel?> GetByIdAsync(string id)
  {
    var sale = await uow.ProductSales.GetByEmailAsync(id);
    return sale is null ? null : ToResponse(sale);
  }

  public async Task<ProductSaleResponseModel> CreateAsync(ProductSaleCreateModel model)
  {
    var sale = new ProductSale
    {
      ProductId = model.ProductId,
      CustomerId = model.CustomerId,
      UserId = model.UserId,
      Quantity = model.Quantity,
      Price = model.Price
    };
    var createdSale = await uow.ProductSales.AddAsync(sale);
    return ToResponse(createdSale);
  }

  public async Task<ProductSaleResponseModel?> UpdateAsync(string id, ProductSaleUpdateModel model)
  {
    var sale = await uow.ProductSales.GetByEmailAsync(id);
    if (sale is null)
    {
      return null;
    }
    sale.ProductId = model.ProductId;
    sale.CustomerId = model.CustomerId;
    sale.UserId = model.UserId;
    sale.Quantity = model.Quantity;
    sale.Price = model.Price;
    sale.DateUpdated = DateTime.UtcNow;
    var updatedSale = await uow.ProductSales.UpdateAsync(sale);
    return ToResponse(updatedSale);
  }

  public async Task<bool> DeleteAsync(string id)
  {
    if (!await uow.ProductSales.ExistsAsync(id))
    {
      return false;
    }
    await uow.ProductSales.DeleteAsync(id);
    return true;
  }

  private static ProductSaleResponseModel ToResponse(ProductSale sale)
  {
    return new ProductSaleResponseModel(
      sale.Id,
      sale.ProductId,
      sale.CustomerId,
      sale.UserId,
      sale.Quantity,
      sale.Price,
      sale.DateCreated,
      sale.DateUpdated);
  }
}
