using CitiesManager.Models;
using CitiesManager.WebAPI.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CitiesManager.Repositories
{
    public class EmployeeFullDetailsRepository : IEmployeeFullDetailsRepository
    {
        private readonly ApplicationDbContext _context;
        public EmployeeFullDetailsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee_Full_Details>> GetAllAsync()
        {
            return await _context.Employee_Full_Details.ToListAsync();
        }

        public async Task<Employee_Full_Details?> GetByIdAsync(int id)
        {
            return await _context.Employee_Full_Details.FindAsync(id);
        }

        public async Task AddAsync(Employee_Full_Details employee_full_details)
        {
            await _context.Employee_Full_Details.AddAsync(employee_full_details);
        }

        public Task UpdateAsync(Employee_Full_Details employee_full_details)
        {
            _context.Employee_Full_Details.Update(employee_full_details);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var employee_full_details = await _context.Employee_Full_Details.FindAsync(id);
            if (employee_full_details != null)
            {
                _context.Employee_Full_Details.Remove(employee_full_details);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
