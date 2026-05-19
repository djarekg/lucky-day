using LuckyDay.Api.Models;
using LuckyDay.Db.Models;
using LuckyDay.Db.Repositories;

namespace LuckyDay.Api.Services;

public class ProductInventoryService(IUnitOfWork uow)
{
  public async Task<IEnumerable<ProductInventoryResponseModel>> GetAllAsync()
  {
    var inventories = await uow.ProductInventories.GetAllAsync();
    return inventories.Select(ToResponse);
  }

  public async Task<ProductInventoryResponseModel?> GetByIdAsync(string id)
  {
    var inventory = await uow.ProductInventories.GetByEmailAsync(id);
    return inventory is null ? null : ToResponse(inventory);
  }

  public async Task<ProductInventoryResponseModel> CreateAsync(ProductInventoryCreateModel model)
  {
    var inventory = new ProductInventory
    {
      ProductId = model.ProductId,
      Size = model.Size,
      Quantity = model.Quantity
    };
    var createdInventory = await uow.ProductInventories.AddAsync(inventory);
    return ToResponse(createdInventory);
  }

  public async Task<ProductInventoryResponseModel?> UpdateAsync(string id, ProductInventoryUpdateModel model)
  {
    var inventory = await uow.ProductInventories.GetByEmailAsync(id);
    if (inventory is null)
    {
      return null;
    }
    inventory.ProductId = model.ProductId;
    inventory.Size = model.Size;
    inventory.Quantity = model.Quantity;
    inventory.DateUpdated = DateTime.UtcNow;
    var updatedInventory = await uow.ProductInventories.UpdateAsync(inventory);
    return ToResponse(updatedInventory);
  }

  public async Task<bool> DeleteAsync(string id)
  {
    if (!await uow.ProductInventories.ExistsAsync(id))
    {
      return false;
    }
    await uow.ProductInventories.DeleteAsync(id);
    return true;
  }

  private static ProductInventoryResponseModel ToResponse(ProductInventory inventory)
  {
    return new ProductInventoryResponseModel(
      inventory.Id,
      inventory.ProductId,
      inventory.Size,
      inventory.Quantity,
      inventory.DateCreated,
      inventory.DateUpdated);
  }
}
