using LuckyDay.Db.Enums;

namespace LuckyDay.Api.Models;

public record ProductColorCreateModel(
  string ProductId,
  Color Color);

public record ProductColorUpdateModel(
  string ProductId,
  Color Color);

public record ProductColorResponseModel(
  string Id,
  string ProductId,
  Color Color,
  DateTime DateCreated,
  DateTime? DateUpdated);
