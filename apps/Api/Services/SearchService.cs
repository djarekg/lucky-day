using LuckyDay.Api.Models;
using LuckyDay.Db.Repositories;

namespace LuckyDay.Api.Services;

public class SearchService(ISearchRepository searchRepository)
{
  public async Task<IEnumerable<SearchResultModel>?> SearchAsync(SearchResultParamsModel request)
  {
    if (string.IsNullOrWhiteSpace(request.Query))
    {
      return null;
    }

    var results = await searchRepository.SearchAsync(
      request.Query,
      request.HighlightStartTag,
      request.HighlightEndTag);

    if (results.Count == 0)
    {
      return null;
    }

    return results
      .OrderBy(result => result.Rank)
      .Select(result => new SearchResultModel(result.Type, result.Rank, result.Json));
  }
}
