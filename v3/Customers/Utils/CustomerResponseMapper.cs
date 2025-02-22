using v3.Common;
using v3.Customers.Domain;
using v3.Customers.DTOs;

namespace v3.Customers.Utils;

public class CustomerResponseMapper
{
    
    public static CustomerResponseDto MapToDto(Customer customer)
    {
        return new CustomerResponseDto(
            customer.Id.ToString(),
            customer.Person.FullName(),
            AddressMapper.Map(customer.Address)
        );
    }
    
    public static List<CustomerResponseDto> MapToList(List<Customer> customers)
    {
        return customers.Select(MapToDto).ToList();
    }
}