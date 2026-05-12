using Db.Models;

namespace Api.Models;

public record UserCreateModel(
  string FirstName,
  string LastName,
  Gender Gender,
  string Email,
  string StreetAddress,
  string? StreetAddress2,
  string City,
  string StateId,
  string Zip,
  string Phone,
  string JobTitle,
  int ImageId,
  bool IsActive);

public record UserUpdateModel(
  string FirstName,
  string LastName,
  Gender Gender,
  string Email,
  string StreetAddress,
  string? StreetAddress2,
  string City,
  string StateId,
  string Zip,
  string Phone,
  string JobTitle,
  int ImageId,
  bool IsActive);

public record UserResponseModel(
  string Id,
  string FirstName,
  string LastName,
  Gender Gender,
  string Email,
  string StreetAddress,
  string? StreetAddress2,
  string City,
  string StateId,
  string Zip,
  string Phone,
  string JobTitle,
  int ImageId,
  bool IsActive,
  DateTime DateCreated,
  DateTime? DateUpdated);
