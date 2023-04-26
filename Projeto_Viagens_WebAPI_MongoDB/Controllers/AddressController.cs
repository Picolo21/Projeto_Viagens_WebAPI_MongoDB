using Microsoft.AspNetCore.Mvc;
using Projeto_Viagens_WebAPI_MongoDB.Models;
using Projeto_Viagens_WebAPI_MongoDB.Services;

namespace Projeto_Viagens_WebAPI_MongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly AddressService _addressService;
        private readonly CityService _cityService;

        public AddressController(AddressService addressService, CityService cityService)
        {
            _addressService = addressService;
            _cityService = cityService;
        }

        [HttpGet]
        public ActionResult<List<Address>> Get() => _addressService.Get();

        [HttpGet("{id:length(24)}", Name = "GetAddress")]
        public ActionResult<Address> Get(string id)
        {
            var address = _addressService.Get(id);
            if (address is null) return NotFound();
            return address;
        }

        [HttpPost]
        public ActionResult<Address> Create(Address address)
        {
            var c = _cityService.Get().Find(x => x.Name.Equals(address.City.Name));
            if (c == null)
                _cityService.Create(address.City);
            else
                address.City = c;

            return _addressService.Create(address);
        }

        [HttpPut("{id:length(24)}")]
        public ActionResult Update(string id, Address address)
        {
            var a = _addressService.Get(id);
            if (a == null) return NotFound();

            _addressService.Update(id, address);
            var city = _cityService.Get().Find(x => (x.Id == address.City.Id) && (x.Name != address.City.Id));
            if (city != null)
                _cityService.Update(address.City.Id, address.City);

            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete(string id)
        {
            if (id is null) return NotFound();

            var address = _addressService.Get(id);
            if (address is null) return NotFound();

            _addressService.Delete(id);
            return Ok();
        }
    }
}
