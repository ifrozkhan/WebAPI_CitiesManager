using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using CitiesManager.WebAPI.Models;
using CitiesManager.WebAPI.Repositories;
using CitiesManager.WebAPI.DatabaseContext;
using Microsoft.EntityFrameworkCore;

public class CustomerRepository : ICustomerRepository
{
    private readonly string _connectionString;

    public CustomerRepository(ApplicationDbContext repo)
    {
        _connectionString = repo.Database.GetConnectionString()!;
    }

    private IDbConnection Connection => new SqlConnection(_connectionString);

    public async Task<IEnumerable<Employee_Details>> GetAllAsync()
    {
        using var db = Connection;
        return await db.QueryAsync<Employee_Details>(
            "SELECT * FROM Employee_Details"
        );
    }

    public async Task<Employee_Details?> GetByIdAsync(int id)
    {
        using var db = Connection;

        var query = @"SELECT * 
                      FROM Employee_Details 
                      WHERE Employee_ID = @Employee_ID";

        return await db.QueryFirstOrDefaultAsync<Employee_Details>(
            query,
            new { Employee_ID = id }
        );
    }

    public async Task<string> CreateAsync(Employee_Details customer)
    {
        using var db = Connection;

        var sql = @"INSERT INTO Employee_Details
                    (Employee_ID, Name, Type, Gender, DOB, Address)
                    VALUES
                    (@Employee_ID, @Name, @Type, @Gender, @DOB, @Address)";

        var parameters = new DynamicParameters();
        parameters.Add("Employee_ID", customer.Employee_ID, DbType.Int64);
        parameters.Add("Name", customer.Name, DbType.String);
        parameters.Add("Type", customer.Type, DbType.String);
        parameters.Add("Gender", customer.Gender, DbType.String);
        parameters.Add("DOB", customer.DOB, DbType.DateTime);
        parameters.Add("Address", customer.Address, DbType.String);

        await db.ExecuteAsync(sql, parameters);
        return "Pass";
    }

    public async Task<string> UpdateAsync(Employee_Details customer, int id)
    {
        using var db = Connection;

        var sql = @"UPDATE Employee_Details
                    SET Name = @Name,
                        Type = @Type,
                        Gender = @Gender,
                        DOB = @DOB,
                        Address = @Address
                    WHERE Employee_ID = @Employee_ID";

        var parameters = new DynamicParameters();
        parameters.Add("Employee_ID", id, DbType.Int64);
        parameters.Add("Name", customer.Name, DbType.String);
        parameters.Add("Type", customer.Type, DbType.String);
        parameters.Add("Gender", customer.Gender, DbType.String);
        parameters.Add("DOB", customer.DOB, DbType.DateTime);
        parameters.Add("Address", customer.Address, DbType.String);

        await db.ExecuteAsync(sql, parameters);
        return "Pass";
    }

    public async Task<string> DeleteAsync(int id)
    {
        using var db = Connection;

        var sql = @"DELETE FROM Employee_Details 
                    WHERE Employee_ID = @Employee_ID";

        await db.ExecuteAsync(sql, new { Employee_ID = id });
        return "Pass";
    }
}