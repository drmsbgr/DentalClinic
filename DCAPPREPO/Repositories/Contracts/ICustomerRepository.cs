using DCAPPLIB.Entities;

namespace DCAPPREPO.Repositories.Contracts;

public interface ICustomerRepository : IRepositoryBase<Customer>
{
    IQueryable<Customer> GetAllCustomers(bool trackChanges);
    Customer? GetCustomerById(int id, bool trackChanges);
    void CreateCustomer(Customer customer);
    void DeleteCustomer(Customer customer);
    void UpdateCustomer(Customer customer);
}
