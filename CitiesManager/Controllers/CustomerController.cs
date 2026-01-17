using Microsoft.AspNetCore.Mvc;
using CitiesManager.WebAPI.Repositories;
using CitiesManager.WebAPI.Models;
using System.Threading.Tasks;

namespace CitiesManager.WebAPI.Controllers
{
    // Using Sql Connection Querys Controller

    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repo;

        public CustomerController(ICustomerRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var customers = await _repo.GetAllAsync();
            return Ok(customers);
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var customer = await _repo.GetByIdAsync(id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        // POST: api/Customer
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Employee_Details customer)
        {
            var result = await _repo.CreateAsync(customer);
            return Ok(result);
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] Employee_Details customer)
        {
            var result = await _repo.UpdateAsync(customer, id);
            return Ok(result);
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _repo.DeleteAsync(id);
            return Ok(result);
        }
    }
}