using CitiesManager.WebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CitiesManager.WebAPI.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Employee_Details>> GetAllAsync();

        Task<Employee_Details?> GetByIdAsync(int id);

        Task<string> CreateAsync(Employee_Details customer);

        Task<string> UpdateAsync(Employee_Details customer, int id);

        Task<string> DeleteAsync(int id);
    }
}