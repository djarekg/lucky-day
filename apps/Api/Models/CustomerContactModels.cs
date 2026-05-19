namespace LuckyDay.Api.Models;

public record CustomerContactCreateModel(
  string CustomerId,
  string FirstName,
  string LastName,
  string Email,
  string StreetAddress,
  string? StreetAddress2,
  string City,
  string StateId,
  string Zip,
  string Phone,
  int ImageId,
  bool IsActive);

public record CustomerContactUpdateModel(
  string CustomerId,
  string FirstName,
  string LastName,
  string Email,
  string StreetAddress,
  string? StreetAddress2,
  string City,
  string StateId,
  string Zip,
  string Phone,
  int ImageId,
  bool IsActive);

public record CustomerContactResponseModel(
  string Id,
  string CustomerId,
  string FirstName,
  string LastName,
  string Email,
  string StreetAddress,
  string? StreetAddress2,
  string City,
  string StateId,
  string Zip,
  string Phone,
  int ImageId,
  bool IsActive,
  DateTime DateCreated,
  DateTime? DateUpdated);
