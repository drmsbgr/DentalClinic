using AutoMapper;
using DCAPPLIB.Entities;
using DCAPPLIB.Entities.Dtos.Customer;
using DCAPPLIB.Repositories.Contracts;
using DCAPPLIB.Services.Contracts;

namespace DCAPPLIB.Services;

public class CustomerManager(IRepositoryManager manager, IMapper mapper) : ICustomerService
{
    private readonly IRepositoryManager _manager = manager;
    private readonly IMapper _mapper = mapper;

    public void CreateCustomer(CustomerDtoForInsertion CustomerDto)
    {
        Customer Customer = _mapper.Map<Customer>(CustomerDto);
        _manager.Customer.CreateCustomer(Customer);
        _manager.Save();
    }

    public void DeleteCustomerById(int id)
    {
        var Customer = GetCustomerById(id, false);
        if (Customer is not null)
        {
            _manager.Customer.DeleteCustomer(Customer);
            _manager.Save();
        }
    }

    public IQueryable<Customer> GetAllCustomers(bool trackChanges) => _manager.Customer.GetAllCustomers(trackChanges);

    public CustomerDtoForUpdate GetCustomerByIdForUpdate(int id, bool trackChanges)
    {
        var Customer = GetCustomerById(id, trackChanges);
        var CustomerDto = _mapper.Map<CustomerDtoForUpdate>(Customer);
        return CustomerDto;
    }

    public Customer? GetCustomerById(int id, bool trackChanges)
    {
        var Customer = _manager.Customer.GetCustomerById(id, trackChanges) ?? throw new Exception("Böyle bir müşteri bulunamadı!");
        return Customer;
    }

    public void UpdateCustomer(CustomerDtoForUpdate CustomerDto)
    {
        var Customer = _mapper.Map<Customer>(CustomerDto);
        _manager.Customer.UpdateCustomer(Customer);
        _manager.Save();
    }
}