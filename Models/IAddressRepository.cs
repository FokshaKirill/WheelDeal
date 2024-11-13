using System.Collections.Generic;
using WheelDeal.Entities;

namespace WheelDeal.Models
{
    public interface IAddressRepository
    {
        void Add(Address address);
        IEnumerable<Address> GetAll();
        Address Find(string key);
        Address Remove(string key);
        void Update(Address address);
    }
}
