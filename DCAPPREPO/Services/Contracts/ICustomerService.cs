using DCAPPLIB.Entities;
using DCAPPLIB.Entities.Dtos.Customer;

namespace DCAPPREPO.Services.Contracts;

public interface ICustomerService
{
    IQueryable<Customer> GetAllCustomers(bool trackChanges);
    Customer? GetCustomerById(int id, bool trackChanges);
    void CreateCustomer(CustomerDtoForInsertion customer);
    void UpdateCustomer(CustomerDtoForUpdate customer);
    bool DeleteCustomerById(int id);
}