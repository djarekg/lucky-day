using LuckyDay.Api.Models;
using LuckyDay.Db.Models;
using LuckyDay.Db.Repositories;

namespace LuckyDay.Api.Services;

public class CustomerContactService(IUnitOfWork uow)
{
  public async Task<IEnumerable<CustomerContactResponseModel>> GetAllAsync()
  {
    var contacts = await uow.CustomerContacts.GetAllAsync();
    return contacts.Select(ToResponse);
  }

  public async Task<CustomerContactResponseModel?> GetByIdAsync(string id)
  {
    var contact = await uow.CustomerContacts.GetByEmailAsync(id);
    return contact is null ? null : ToResponse(contact);
  }

  public async Task<CustomerContactResponseModel> CreateAsync(CustomerContactCreateModel model)
  {
    var contact = new CustomerContact
    {
      CustomerId = model.CustomerId,
      FirstName = model.FirstName,
      LastName = model.LastName,
      Email = model.Email,
      StreetAddress = model.StreetAddress,
      StreetAddress2 = model.StreetAddress2,
      City = model.City,
      StateId = model.StateId,
      Zip = model.Zip,
      Phone = model.Phone,
      ImageId = model.ImageId,
      IsActive = model.IsActive
    };
    var createdContact = await uow.CustomerContacts.AddAsync(contact);
    return ToResponse(createdContact);
  }

  public async Task<CustomerContactResponseModel?> UpdateAsync(string id, CustomerContactUpdateModel model)
  {
    var contact = await uow.CustomerContacts.GetByEmailAsync(id);
    if (contact is null)
    {
      return null;
    }
    contact.CustomerId = model.CustomerId;
    contact.FirstName = model.FirstName;
    contact.LastName = model.LastName;
    contact.Email = model.Email;
    contact.StreetAddress = model.StreetAddress;
    contact.StreetAddress2 = model.StreetAddress2;
    contact.City = model.City;
    contact.StateId = model.StateId;
    contact.Zip = model.Zip;
    contact.Phone = model.Phone;
    contact.ImageId = model.ImageId;
    contact.IsActive = model.IsActive;
    contact.DateUpdated = DateTime.UtcNow;
    var updatedContact = await uow.CustomerContacts.UpdateAsync(contact);
    return ToResponse(updatedContact);
  }

  public async Task<bool> DeleteAsync(string id)
  {
    if (!await uow.CustomerContacts.ExistsAsync(id))
    {
      return false;
    }
    await uow.CustomerContacts.DeleteAsync(id);
    return true;
  }

  private static CustomerContactResponseModel ToResponse(CustomerContact contact)
  {
    return new CustomerContactResponseModel(
      contact.Id,
      contact.CustomerId,
      contact.FirstName,
      contact.LastName,
      contact.Email,
      contact.StreetAddress,
      contact.StreetAddress2,
      contact.City,
      contact.StateId,
      contact.Zip,
      contact.Phone,
      contact.ImageId,
      contact.IsActive,
      contact.DateCreated,
      contact.DateUpdated);
  }
}
