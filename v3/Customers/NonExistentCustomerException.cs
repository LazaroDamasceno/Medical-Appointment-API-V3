namespace v3.Customers;

public class NonExistentCustomerException(string id)
    : Exception($"Customer whose id is {id} was not found.");