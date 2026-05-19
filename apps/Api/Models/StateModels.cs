namespace LuckyDay.Api.Models;

public record StateCreateModel(string Name, string Code);

public record StateUpdateModel(string Name, string Code);

public record StateResponseModel(
  string Id,
  string Name,
  string Code);
