namespace LuckyDay.Api.Models;

public record ProductSaleCreateModel(
  string ProductId,
  string CustomerId,
  string UserId,
  int Quantity,
  decimal Price);

public record ProductSaleUpdateModel(
  string ProductId,
  string CustomerId,
  string UserId,
  int Quantity,
  decimal Price);

public record ProductSaleResponseModel(
  string Id,
  string ProductId,
  string CustomerId,
  string UserId,
  int Quantity,
  decimal Price,
  DateTime DateCreated,
  DateTime? DateUpdated);
