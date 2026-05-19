using System.Data;
using System.Text;
using System.Text.Json;
using Microsoft.Data.Sqlite;

namespace LuckyDay.Db.Repositories;

public static class SearchResultType
{
  public const int User = 1;
  public const int Customer = 2;
  public const int CustomerContact = 3;
  public const int Product = 4;
}

public record SearchResultRecord(int Type, double Rank, string Json);

public class SearchRepository(LuckyDayDbContext context) : ISearchRepository
{
  private readonly LuckyDayDbContext _context = context;

  public async Task<IReadOnlyList<SearchResultRecord>> SearchAsync(
    string query,
    string highlightStartTag,
    string highlightEndTag)
  {
    var searchQuery = BuildMatchQuery(query);
    var results = new List<SearchResultRecord>();

    await using var connection = (SqliteConnection)_context.Database.GetDbConnection();
    if (connection.State != ConnectionState.Open)
    {
      await connection.OpenAsync();
    }

    await MatchTableAsync(results, connection, SearchResultType.User, searchQuery, highlightStartTag, highlightEndTag,
      "User_Fts", ["id", "firstName", "lastName", "email", "jobTitle", "streetAddress", "streetAddress2", "city", "phone"]);

    await MatchTableAsync(results, connection, SearchResultType.Customer, searchQuery, highlightStartTag, highlightEndTag,
      "Customer_Fts", ["id", "name", "streetAddress", "streetAddress2", "city", "phone"]);

    await MatchTableAsync(results, connection, SearchResultType.CustomerContact, searchQuery, highlightStartTag, highlightEndTag,
      "CustomerContact_Fts", ["id", "customerId", "firstName", "lastName", "email", "streetAddress", "streetAddress2", "city", "phone"]);

    await MatchTableAsync(results, connection, SearchResultType.Product, searchQuery, highlightStartTag, highlightEndTag,
      "Product_Fts", ["id", "name", "description", "price"]);

    return [.. results.OrderBy(result => result.Rank)];
  }

  private static string BuildMatchQuery(string query)
  {
    var escaped = query.Trim().Replace("\"", "\"\"");
    return $"\"{escaped}\"";
  }

  private static async Task MatchTableAsync(
    ICollection<SearchResultRecord> results,
    SqliteConnection connection,
    int type,
    string query,
    string highlightStartTag,
    string highlightEndTag,
    string ftsTable,
    IReadOnlyList<string> fields)
  {
    var sql = BuildQuerySql(ftsTable, fields);

    await using var command = connection.CreateCommand();
    command.CommandText = sql;
    command.Parameters.AddWithValue("$query", query);
    command.Parameters.AddWithValue("$highlightStartTag", highlightStartTag);
    command.Parameters.AddWithValue("$highlightEndTag", highlightEndTag);

    await using var reader = await command.ExecuteReaderAsync();
    while (await reader.ReadAsync())
    {
      var payload = new Dictionary<string, string?>(fields.Count);
      for (var fieldIndex = 0; fieldIndex < fields.Count; fieldIndex++)
      {
        payload[fields[fieldIndex]] = reader.IsDBNull(fieldIndex) ? null : reader.GetString(fieldIndex);
      }

      var rankIndex = fields.Count;
      var rank = Convert.ToDouble(reader.GetValue(rankIndex));

      results.Add(new SearchResultRecord(type, rank, JsonSerializer.Serialize(payload)));
    }
  }

  private static string BuildQuerySql(string ftsTable, IReadOnlyList<string> fields)
  {
    var columnSelect = BuildColumnHighlightSelect(ftsTable, fields);

    return $"""
      SELECT
        {columnSelect},
        rank
      FROM {ftsTable}
      WHERE {ftsTable} MATCH $query
      ORDER BY rank;
      """;
  }

  private static string BuildColumnHighlightSelect(string ftsTable, IReadOnlyList<string> fields)
  {
    var builder = new StringBuilder();

    for (var index = 0; index < fields.Count; index++)
    {
      var field = fields[index];

      if (index > 0)
      {
        builder.Append(", ");
      }

      if (field == "id" || field.EndsWith("Id", StringComparison.Ordinal))
      {
        builder.Append(field);
        continue;
      }

      builder.Append($"highlight({ftsTable}, {index}, $highlightStartTag, $highlightEndTag) AS {field}");
    }

    return builder.ToString();
  }
}
