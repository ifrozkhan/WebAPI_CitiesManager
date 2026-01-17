using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CitiesManager.WebAPI.DatabaseContext;
using CitiesManager.WebAPI.Models;

namespace CitiesManager.WebAPI.Controllers
{
    // Entity FrameWork Controller

    [ApiController]
    [Route("api/[controller]")]
    public class Employee_DetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public Employee_DetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Employee_Details
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee_Details>>> GetEmployeeDetails()
        {
            return await _context.Employee_Details.ToListAsync();
        }

        // GET: api/Employee_Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee_Details>> GetEmployeeDetails(int id)
        {
            var employee = await _context.Employee_Details.FindAsync(id);

            if (employee == null)
                return NotFound();

            return employee;
        }

        // PUT: api/Employee_Details/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeDetails(int id, Employee_Details employee)
        {
            if (id != employee.Emp_ID)
                return BadRequest();

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeDetailsExists(id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // POST: api/Employee_Details
        [HttpPost]
        public async Task<ActionResult<Employee_Details>> PostEmployeeDetails(Employee_Details employee)
        {
            _context.Employee_Details.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetEmployeeDetails),
                new { id = employee.Emp_ID },
                employee
            );
        }

        // DELETE: api/Employee_Details/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeDetails(int id)
        {
            var employee = await _context.Employee_Details.FindAsync(id);

            if (employee == null)
                return NotFound();

            _context.Employee_Details.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeDetailsExists(int id)
        {
            return _context.Employee_Details.Any(e => e.Emp_ID == id);
        }
    }
}