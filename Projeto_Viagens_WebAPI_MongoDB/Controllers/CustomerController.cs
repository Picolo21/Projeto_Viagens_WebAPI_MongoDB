using Microsoft.AspNetCore.Mvc;
using Projeto_Viagens_WebAPI_MongoDB.Models;
using Projeto_Viagens_WebAPI_MongoDB.Services;

namespace Projeto_Viagens_WebAPI_MongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;
        private readonly AddressService _addressService;
        private readonly CityService _cityService;

        public CustomerController(CustomerService customerService, AddressService addressService, CityService cityService)
        {
            _customerService = customerService;
            _addressService = addressService;
            _cityService = cityService;
        }

        [HttpGet]
        public ActionResult<List<Customer>> Get() => _customerService.Get();

        [HttpGet("{id:length(24)}", Name = "GetCustomer")]
        public ActionResult<Customer> Get(string id)
        {
            var customer = _customerService.Get(id);
            if (customer is null) return NotFound();
            return customer;
        }

        [HttpPost]
        public ActionResult<Customer> Create(Customer customer)
        {
            var a = _addressService.Get().Find(x => x.Street == customer.Address.Street);
            if (a == null)
            {
                var c = _cityService.Get().Find(x => x.Name == customer.Address.City.Name);
                if (c == null)
                    _cityService.Create(customer.Address.City);
                else
                    customer.Address.City = c;
                _addressService.Create(customer.Address);
            }
            else
            {
                customer.Address = a;
            }
            return _customerService.Create(customer);
        }

        [HttpPut("{id:length(24)}")]
        public ActionResult Update(string id, Customer customer)
        {
            var c = _customerService.Get(id);
            if (c is null) return NotFound();
            _customerService.Update(id, customer);

            var address = _addressService.Get().Find(x => (x.Id == customer.Address.Id) && (x.Street != customer.Address.Street) && (x.Number != customer.Address.Number));
            if (address != null)
            {
                var city = _cityService.Get().Find(x => (x.Id == customer.Address.City.Id) && (x.Name != customer.Address.City.Name));
                if (city != null)
                    _cityService.Update(customer.Address.City.Id, customer.Address.City);

                _addressService.Update(customer.Address.Id, customer.Address);
            }
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete(string id)
        {
            if (id is null) return NotFound();

            var customer = _customerService.Get(id);
            if (customer is null) return NotFound();

            _customerService.Delete(id);
            return Ok();
        }
    }
}
