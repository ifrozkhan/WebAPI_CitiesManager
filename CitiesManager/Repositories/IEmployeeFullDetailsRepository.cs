using CitiesManager.Models;

namespace CitiesManager.Repositories
{
    public interface IEmployeeFullDetailsRepository
    {
        Task<List<Employee_Full_Details>> GetAllAsync();
        Task<Employee_Full_Details> GetByIdAsync(int id);
        Task AddAsync(Employee_Full_Details employee_full_details);
        Task UpdateAsync(Employee_Full_Details employee_full_details);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}
