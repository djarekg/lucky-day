namespace LuckyDay.Db.Repositories;

public interface ISearchRepository
{
  Task<IReadOnlyList<SearchResultRecord>> SearchAsync(string query, string highlightStartTag, string highlightEndTag);
}
