using v3.Common;
using v3.Customers.Domain;
using v3.Customers.DTOs;

namespace v3.Customers.Utils;

public class CustomerResponseMapper
{
    public CustomerResponseDto Map(Customer customer)
    {
        return new CustomerResponseDto(
            customer.Id.ToString(),
            customer.Person.FullName(),
            AddressMapper.Map(customer.Address)
        );
    }
}