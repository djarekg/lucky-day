using LuckyDay.Api.Models;
using LuckyDay.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LuckyDay.Api.Controllers;

[ApiController]
[Route("product-colors")]
public class ProductColorsController(ProductColorService productColorService) : ControllerBase
{
  [HttpGet]
  [ProducesResponseType<IEnumerable<ProductColorResponseModel>>(StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<ProductColorResponseModel>>> GetAll()
  {
    var colors = await productColorService.GetAllAsync();
    return Ok(colors);
  }

  [HttpGet("{id}")]
  [ProducesResponseType<ProductColorResponseModel>(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<ProductColorResponseModel>> GetById(string id)
  {
    var color = await productColorService.GetByIdAsync(id);
    if (color is null)
    {
      return NotFound();
    }

    return Ok(color);
  }

  [HttpPost]
  [ProducesResponseType<ProductColorResponseModel>(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<ProductColorResponseModel>> Create([FromBody] ProductColorCreateModel request)
  {
    if (string.IsNullOrWhiteSpace(request.ProductId))
    {
      return BadRequest("ProductId is required.");
    }

    var createdColor = await productColorService.CreateAsync(request);
    return CreatedAtAction(nameof(GetById), new { id = createdColor.Id }, createdColor);
  }

  [HttpPut("{id}")]
  [ProducesResponseType<ProductColorResponseModel>(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<ProductColorResponseModel>> Update(string id, [FromBody] ProductColorUpdateModel request)
  {
    var updatedColor = await productColorService.UpdateAsync(id, request);
    if (updatedColor is null)
    {
      return NotFound();
    }

    return Ok(updatedColor);
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> Delete(string id)
  {
    var deleted = await productColorService.DeleteAsync(id);
    if (!deleted)
    {
      return NotFound();
    }

    return NoContent();
  }
}
