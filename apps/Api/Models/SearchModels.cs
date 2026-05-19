namespace LuckyDay.Api.Models;

public static class SearchResultType
{
  public const int User = 1;
  public const int Customer = 2;
  public const int CustomerContact = 3;
  public const int Product = 4;
}

public record SearchResultParamsModel(
  string Query,
  string HighlightStartTag,
  string HighlightEndTag);

public record SearchResultModel(
  int Type,
  double Rank,
  string Json);
