using LuckyDay.Api.Models;
using LuckyDay.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LuckyDay.Api.Controllers;

[ApiController]
[Route("products")]
public class ProductsController(ProductService productService) : ControllerBase
{
  [HttpGet]
  [ProducesResponseType<IEnumerable<ProductResponseModel>>(StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<ProductResponseModel>>> GetAll()
  {
    var products = await productService.GetAllAsync();
    return Ok(products);
  }

  [HttpGet("{id}")]
  [ProducesResponseType<ProductResponseModel>(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<ProductResponseModel>> GetById(string id)
  {
    var product = await productService.GetByIdAsync(id);
    if (product is null)
    {
      return NotFound();
    }

    return Ok(product);
  }

  [HttpPost]
  [ProducesResponseType<ProductResponseModel>(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<ProductResponseModel>> Create([FromBody] ProductCreateModel request)
  {
    if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Description) ||
        string.IsNullOrWhiteSpace(request.Price))
    {
      return BadRequest("Name, description, and price are required.");
    }

    var createdProduct = await productService.CreateAsync(request);
    return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
  }

  [HttpPut("{id}")]
  [ProducesResponseType<ProductResponseModel>(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<ProductResponseModel>> Update(string id, [FromBody] ProductUpdateModel request)
  {
    var updatedProduct = await productService.UpdateAsync(id, request);
    if (updatedProduct is null)
    {
      return NotFound();
    }

    return Ok(updatedProduct);
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> Delete(string id)
  {
    var deleted = await productService.DeleteAsync(id);
    if (!deleted)
    {
      return NotFound();
    }

    return NoContent();
  }
}
