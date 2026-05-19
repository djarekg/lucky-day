using LuckyDay.Db.Enums;

namespace LuckyDay.Api.Models;

public record ProductInventoryCreateModel(
  string ProductId,
  Size Size,
  int Quantity);

public record ProductInventoryUpdateModel(
  string ProductId,
  Size Size,
  int Quantity);

public record ProductInventoryResponseModel(
  string Id,
  string ProductId,
  Size Size,
  int Quantity,
  DateTime DateCreated,
  DateTime? DateUpdated);
