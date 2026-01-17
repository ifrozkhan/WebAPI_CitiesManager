using CitiesManager.Models;
using CitiesManager.Repositories;
using CitiesManager.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CitiesManager.Controllers
{
    //Entity Framework COntroller

    [Route("api/[Controller]")]
    [ApiController]
    public class Employee_Full_DetailsController : Controller
    {
        private readonly IEmployeeFullDetailsRepository _employeeFullDetailsRepository;

        public Employee_Full_DetailsController(IEmployeeFullDetailsRepository repo)
        {
            _employeeFullDetailsRepository = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee_Full_Details>>> GetAllEmployeeFullDetails()
        {
            return Ok(await _employeeFullDetailsRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeFullDetails(int id)
        {
            var employee = await _employeeFullDetailsRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> CretaeEmployee(Employee_Full_Details employee_full_details)
        {
            await _employeeFullDetailsRepository.AddAsync(employee_full_details);
            await _employeeFullDetailsRepository.SaveAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee_Full_Details employee_full_details)
        {
            if (id != employee_full_details.Id)
                return BadRequest();
            await _employeeFullDetailsRepository.UpdateAsync(employee_full_details);
            await _employeeFullDetailsRepository.SaveAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeFullDetailsRepository.DeleteAsync(id);
            await _employeeFullDetailsRepository.SaveAsync();
            return NoContent();
        }
    }
}
