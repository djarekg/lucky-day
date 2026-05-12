using Api.Models;
using Db.Models;
using Db.Repositories;

namespace Api.Services;

public class UserService(IUnitOfWork uow)
{
  public async Task<IEnumerable<UserResponseModel>> GetAllAsync()
  {
    var users = await uow.Users.GetAllAsync();
    return users.Select(ToResponse);
  }

  public async Task<UserResponseModel?> GetByIdAsync(string id)
  {
    var user = await uow.Users.GetByIdAsync(id);
    return user is null ? null : ToResponse(user);
  }

  public async Task<UserResponseModel> CreateAsync(UserCreateModel model)
  {
    var user = new User
    {
      FirstName = model.FirstName,
      LastName = model.LastName,
      Gender = model.Gender,
      Email = model.Email,
      StreetAddress = model.StreetAddress,
      StreetAddress2 = model.StreetAddress2,
      City = model.City,
      StateId = model.StateId,
      Zip = model.Zip,
      Phone = model.Phone,
      JobTitle = model.JobTitle,
      ImageId = model.ImageId,
      IsActive = model.IsActive
    };

    var createdUser = await uow.Users.AddAsync(user);
    return ToResponse(createdUser);
  }

  public async Task<UserResponseModel?> UpdateAsync(string id, UserUpdateModel model)
  {
    var existingUser = await uow.Users.GetByIdAsync(id);
    if (existingUser is null)
    {
      return null;
    }

    existingUser.FirstName = model.FirstName;
    existingUser.LastName = model.LastName;
    existingUser.Gender = model.Gender;
    existingUser.Email = model.Email;
    existingUser.StreetAddress = model.StreetAddress;
    existingUser.StreetAddress2 = model.StreetAddress2;
    existingUser.City = model.City;
    existingUser.StateId = model.StateId;
    existingUser.Zip = model.Zip;
    existingUser.Phone = model.Phone;
    existingUser.JobTitle = model.JobTitle;
    existingUser.ImageId = model.ImageId;
    existingUser.IsActive = model.IsActive;
    existingUser.DateUpdated = DateTime.UtcNow;

    var updatedUser = await uow.Users.UpdateAsync(existingUser);
    return ToResponse(updatedUser);
  }

  public async Task<bool> DeleteAsync(string id)
  {
    var existingUser = await uow.Users.GetByIdAsync(id);
    if (existingUser is null)
    {
      return false;
    }

    await uow.Users.DeleteAsync(id);
    return true;
  }

  private static UserResponseModel ToResponse(User user)
  {
    return new UserResponseModel(
      user.Id,
      user.FirstName,
      user.LastName,
      user.Gender,
      user.Email,
      user.StreetAddress,
      user.StreetAddress2,
      user.City,
      user.StateId,
      user.Zip,
      user.Phone,
      user.JobTitle,
      user.ImageId,
      user.IsActive,
      user.DateCreated,
      user.DateUpdated);
  }
}
