using DCAPPLIB.Entities;
using DCAPPLIB.Repositories.Contracts;

namespace DCAPPLIB.Repositories;

public class CustomerRepository(RepositoryContext context) : RepositoryBase<Customer>(context), ICustomerRepository
{
    public void CreateCustomer(Customer customer) => Create(customer);

    public void DeleteCustomer(Customer customer) => Delete(customer);

    public void UpdateCustomer(Customer customer) => Update(customer);
    public IQueryable<Customer> GetAllCustomers(bool trackChanges) => GetAll(trackChanges);
    public Customer? GetCustomerById(int id, bool trackChanges) => FindByCondition(x => x.Id == id, trackChanges);
}