namespace LuckyDay.Api.Models;

public record CustomerCreateModel(
  string Name,
  string StreetAddress,
  string? StreetAddress2,
  string City,
  string StateId,
  string Zip,
  string Phone,
  bool IsActive);

public record CustomerUpdateModel(
  string Name,
  string StreetAddress,
  string? StreetAddress2,
  string City,
  string StateId,
  string Zip,
  string Phone,
  bool IsActive);

public record CustomerResponseModel(
  string Id,
  string Name,
  string StreetAddress,
  string? StreetAddress2,
  string City,
  string StateId,
  string Zip,
  string Phone,
  bool IsActive,
  DateTime DateCreated,
  DateTime? DateUpdated);
