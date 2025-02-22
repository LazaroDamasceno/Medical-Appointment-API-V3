namespace v3.Common;

public class AddressMapper
{
    public static string Map(Address address)
    {
        return $"{address.PostalCode} {address.Street}, {address.City}, {address.State}";
    }
}