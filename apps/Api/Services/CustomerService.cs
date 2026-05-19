using LuckyDay.Api.Models;
using LuckyDay.Db.Models;
using LuckyDay.Db.Repositories;

namespace LuckyDay.Api.Services;

public class CustomerService(IUnitOfWork uow)
{
  public async Task<IEnumerable<CustomerResponseModel>> GetAllAsync()
  {
    var customers = await uow.Customers.GetAllAsync();
    return customers.Select(ToResponse);
  }

  public async Task<CustomerResponseModel?> GetByIdAsync(string id)
  {
    var customer = await uow.Customers.GetCustomerWithContactsAsync(id);
    return customer is null ? null : ToResponse(customer);
  }

  public async Task<CustomerResponseModel> CreateAsync(CustomerCreateModel model)
  {
    var customer = new Customer
    {
      Name = model.Name,
      StreetAddress = model.StreetAddress,
      StreetAddress2 = model.StreetAddress2,
      City = model.City,
      StateId = model.StateId,
      Zip = model.Zip,
      Phone = model.Phone,
      IsActive = model.IsActive
    };
    var createdCustomer = await uow.Customers.AddAsync(customer);
    return ToResponse(createdCustomer);
  }

  public async Task<CustomerResponseModel?> UpdateAsync(string id, CustomerUpdateModel model)
  {
    var customer = await uow.Customers.GetByEmailAsync(id);
    if (customer is null)
    {
      return null;
    }
    customer.Name = model.Name;
    customer.StreetAddress = model.StreetAddress;
    customer.StreetAddress2 = model.StreetAddress2;
    customer.City = model.City;
    customer.StateId = model.StateId;
    customer.Zip = model.Zip;
    customer.Phone = model.Phone;
    customer.IsActive = model.IsActive;
    customer.DateUpdated = DateTime.UtcNow;
    var updatedCustomer = await uow.Customers.UpdateAsync(customer);
    return ToResponse(updatedCustomer);
  }

  public async Task<bool> DeleteAsync(string id)
  {
    if (!await uow.Customers.ExistsAsync(id))
    {
      return false;
    }
    await uow.Customers.DeleteAsync(id);
    return true;
  }

  private static CustomerResponseModel ToResponse(Customer customer)
  {
    return new CustomerResponseModel(
      customer.Id,
      customer.Name,
      customer.StreetAddress,
      customer.StreetAddress2,
      customer.City,
      customer.StateId,
      customer.Zip,
      customer.Phone,
      customer.IsActive,
      customer.DateCreated,
      customer.DateUpdated);
  }
}
