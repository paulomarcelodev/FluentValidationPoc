namespace DomainModel;

public class Address
{
    public Address(string state, string city, string street, string zipCode)
    {
        State = state;
        City = city;
        Street = street;
        ZipCode = zipCode;
    }

    public string State { get; }
    public string City { get; }
    public string Street { get; }
    public string ZipCode { get; }
}
