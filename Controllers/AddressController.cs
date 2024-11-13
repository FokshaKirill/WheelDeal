using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WheelDeal.Entities;
using WheelDeal.Models;

namespace WheelDeal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        public AddressController(IAddressRepository addressItems)
        {
            AddressItems = addressItems;
        }
        public IAddressRepository AddressItems { get; set; }
        
        public IEnumerable<Address> GetAll()
        {
            return AddressItems.GetAll();
        }

        [HttpGet("{id}", Name = "GetAddress")]
        public IActionResult GetById(string id)
        {
            var address = AddressItems.Find(id);
            if (address == null)
            {
                return NotFound();
            }
            return new ObjectResult(address);
        }
    }
}
