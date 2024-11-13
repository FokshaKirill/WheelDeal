using System.Collections.Concurrent;
using WheelDeal.Entities;

namespace WheelDeal.Models
{
    public class AddressRepository : IAddressRepository
    {
        private static ConcurrentDictionary<string, Address> _addresses =
            new ConcurrentDictionary<string, Address>();

        public AddressRepository()
        {
            Add(new Address { Name = "Item1" });
        }

        public IEnumerable<Address> GetAll()
        {
            return _addresses.Values;
        }

        public void Add(Address item)
        {
            item.Key = Guid.NewGuid().ToString();
            _addresses[item.Key] = item;
        }

        public Address Find(string key)
        {
            Address address;
            _addresses.TryGetValue(key, out address);
            return address;
        }

        public Address Remove(string key)
        {
            Address item;
            _addresses.TryRemove(key, out item);
            return item;
        }

        public void Update(Address item)
        {
            _addresses[item.Key] = item;
        }
    }
}

