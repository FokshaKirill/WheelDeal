using Microsoft.AspNetCore.Mvc;

namespace WheelDeal.Database.ValueObjects;

[ApiController]
[Route("[controller]")]
public class Address : ControllerBase
{
    public string Key { get; set; }    
    public string Name { get; set; }    
    public string City { get; set; }
    public string StreetAddress { get; set; }
    public string PhoneNumber { get; set; }
    
    // public Address(string key, string name, string phoneNumber, string streetAddress, string city)
    // {
    //     Key = key;
    //     Name = name;
    //     PhoneNumber = phoneNumber;
    //     StreetAddress = streetAddress;
    //     City = city;
    // }
}