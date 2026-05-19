using LuckyDay.Api.Models;
using LuckyDay.Db.Models;
using LuckyDay.Db.Repositories;

namespace LuckyDay.Api.Services;

public class UserCredentialService(IUnitOfWork uow)
{
  public async Task<IEnumerable<UserCredentialResponseModel>> GetAllAsync()
  {
    var credentials = await uow.UserCredentials.GetAllAsync();
    return credentials.Select(ToResponse);
  }

  public async Task<UserCredentialResponseModel?> GetByIdAsync(string id)
  {
    var credential = await uow.UserCredentials.GetByEmailAsync(id);
    return credential is null ? null : ToResponse(credential);
  }

  public async Task<UserCredentialResponseModel> CreateAsync(UserCredentialCreateModel model)
  {
    var credential = new UserCredential
    {
      UserId = model.UserId,
      Password = model.Password,
      Role = model.Role
    };
    var createdCredential = await uow.UserCredentials.AddAsync(credential);
    return ToResponse(createdCredential);
  }

  public async Task<UserCredentialResponseModel?> UpdateAsync(string id, UserCredentialUpdateModel model)
  {
    var credential = await uow.UserCredentials.GetByEmailAsync(id);
    if (credential is null)
    {
      return null;
    }
    credential.UserId = model.UserId;
    credential.Password = model.Password;
    credential.Role = model.Role;
    var updatedCredential = await uow.UserCredentials.UpdateAsync(credential);
    return ToResponse(updatedCredential);
  }

  public async Task<bool> DeleteAsync(string id)
  {
    if (!await uow.UserCredentials.ExistsAsync(id))
    {
      return false;
    }
    await uow.UserCredentials.DeleteAsync(id);
    return true;
  }

  private static UserCredentialResponseModel ToResponse(UserCredential credential)
  {
    return new UserCredentialResponseModel(credential.Id, credential.UserId, credential.Role);
  }
}
