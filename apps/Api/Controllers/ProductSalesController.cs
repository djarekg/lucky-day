using LuckyDay.Api.Models;
using LuckyDay.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LuckyDay.Api.Controllers;

[ApiController]
[Route("product-sales")]
public class ProductSalesController(ProductSaleService productSaleService) : ControllerBase
{
  [HttpGet]
  [ProducesResponseType<IEnumerable<ProductSaleResponseModel>>(StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<ProductSaleResponseModel>>> GetAll()
  {
    var sales = await productSaleService.GetAllAsync();
    return Ok(sales);
  }

  [HttpGet("{id}")]
  [ProducesResponseType<ProductSaleResponseModel>(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<ProductSaleResponseModel>> GetById(string id)
  {
    var sale = await productSaleService.GetByIdAsync(id);
    if (sale is null)
    {
      return NotFound();
    }

    return Ok(sale);
  }

  [HttpPost]
  [ProducesResponseType<ProductSaleResponseModel>(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<ProductSaleResponseModel>> Create([FromBody] ProductSaleCreateModel request)
  {
    if (string.IsNullOrWhiteSpace(request.ProductId) ||
        string.IsNullOrWhiteSpace(request.CustomerId) || string.IsNullOrWhiteSpace(request.UserId))
    {
      return BadRequest("ProductId, CustomerId, and UserId are required.");
    }

    var createdSale = await productSaleService.CreateAsync(request);
    return CreatedAtAction(nameof(GetById), new { id = createdSale.Id }, createdSale);
  }

  [HttpPut("{id}")]
  [ProducesResponseType<ProductSaleResponseModel>(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<ProductSaleResponseModel>> Update(string id, [FromBody] ProductSaleUpdateModel request)
  {
    var updatedSale = await productSaleService.UpdateAsync(id, request);
    if (updatedSale is null)
    {
      return NotFound();
    }

    return Ok(updatedSale);
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> Delete(string id)
  {
    var deleted = await productSaleService.DeleteAsync(id);
    if (!deleted)
    {
      return NotFound();
    }

    return NoContent();
  }
}
