using LuckyDay.Db.Enums;

namespace LuckyDay.Api.Models;

public record ProductCreateModel(
  string Name,
  string Description,
  string Price,
  Gender Gender,
  ProductType ProductType,
  bool IsActive);

public record ProductUpdateModel(
  string Name,
  string Description,
  string Price,
  Gender Gender,
  ProductType ProductType,
  bool IsActive);

public record ProductResponseModel(
  string Id,
  string Name,
  string Description,
  string Price,
  Gender Gender,
  ProductType ProductType,
  bool IsActive,
  DateTime DateCreated,
  DateTime? DateUpdated);
