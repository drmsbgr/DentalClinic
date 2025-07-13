using DCAPPLIB.Entities;
using DCAPPLIB.Entities.Dtos.Customer;

namespace DCAPPLIB.Services.Contracts;

public interface ICustomerService
{
    IQueryable<Customer> GetAllCustomers(bool trackChanges);
    Customer? GetCustomerById(int id, bool trackChanges);
    void CreateCustomer(CustomerDtoForInsertion customer);
    void UpdateCustomer(CustomerDtoForUpdate customer);
    void DeleteCustomerById(int id);
}