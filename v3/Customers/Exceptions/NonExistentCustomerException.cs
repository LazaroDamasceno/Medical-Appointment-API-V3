namespace v3.Customers.Exceptions;

public class NonExistentCustomerException(string id)
    : Exception($"Customer whose id is {id} was not found.");