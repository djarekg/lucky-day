using System.Text.Json;

namespace LuckyDay.Api.Configuration;

public sealed class UpperInvariantJsonNamingPolicy : JsonNamingPolicy
{
  public override string ConvertName(string name) => name.ToUpperInvariant();
}
