using LuckyDay.Api.Models;
using LuckyDay.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LuckyDay.Api.Controllers;

[ApiController]
[Route("search")]
public class SearchController(SearchService searchService) : ControllerBase
{
  [HttpPost]
  [ProducesResponseType<IEnumerable<SearchResultModel>>(StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<SearchResultModel>?>> Search([FromBody] SearchResultParamsModel request)
  {
    var results = await searchService.SearchAsync(request);
    return Ok(results);
  }
}
