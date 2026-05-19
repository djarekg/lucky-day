using LuckyDay.Api.Models;
using LuckyDay.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LuckyDay.Api.Controllers;

[ApiController]
[Route("product-inventories")]
public class ProductInventoriesController(ProductInventoryService productInventoryService) : ControllerBase
{
  [HttpGet]
  [ProducesResponseType<IEnumerable<ProductInventoryResponseModel>>(StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<ProductInventoryResponseModel>>> GetAll()
  {
    var inventories = await productInventoryService.GetAllAsync();
    return Ok(inventories);
  }

  [HttpGet("{id}")]
  [ProducesResponseType<ProductInventoryResponseModel>(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<ProductInventoryResponseModel>> GetById(string id)
  {
    var inventory = await productInventoryService.GetByIdAsync(id);
    if (inventory is null)
    {
      return NotFound();
    }

    return Ok(inventory);
  }

  [HttpPost]
  [ProducesResponseType<ProductInventoryResponseModel>(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<ProductInventoryResponseModel>> Create([FromBody] ProductInventoryCreateModel request)
  {
    if (string.IsNullOrWhiteSpace(request.ProductId))
    {
      return BadRequest("ProductId is required.");
    }

    var createdInventory = await productInventoryService.CreateAsync(request);
    return CreatedAtAction(nameof(GetById), new { id = createdInventory.Id }, createdInventory);
  }

  [HttpPut("{id}")]
  [ProducesResponseType<ProductInventoryResponseModel>(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<ProductInventoryResponseModel>> Update(string id, [FromBody] ProductInventoryUpdateModel request)
  {
    var updatedInventory = await productInventoryService.UpdateAsync(id, request);
    if (updatedInventory is null)
    {
      return NotFound();
    }

    return Ok(updatedInventory);
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> Delete(string id)
  {
    var deleted = await productInventoryService.DeleteAsync(id);
    if (!deleted)
    {
      return NotFound();
    }

    return NoContent();
  }
}
