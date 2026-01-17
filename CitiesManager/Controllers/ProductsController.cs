using CitiesManager.WebAPI.DatabaseContext;
using CitiesManager.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CitiesManager.WebAPI.Controllers
{
    /// Using SqlConnection Querys Controller

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly string _connectionString;

        public ProductsController(ApplicationDbContext context)
        {
            _connectionString = context.Database.GetConnectionString();
        }

        private SqlConnection CreateConnection() => new SqlConnection(_connectionString);

        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = new List<Employee_Details>();

            using var conn = CreateConnection();
            await conn.OpenAsync();

            var cmd = new SqlCommand("SELECT * FROM Employee_Details", conn);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                employees.Add(new Employee_Details
                {
                    Emp_ID = reader.GetInt32(reader.GetOrdinal("Emp_ID")),
                    Emp_Name = reader["Emp_Name"]?.ToString(),
                    Type = reader["Type"]?.ToString(),
                    Gender = reader["Gender"]?.ToString(),
                    DOB = reader.GetDateTime(reader.GetOrdinal("DOB")),
                    Address = reader["Address"]?.ToString()
                });
            }

            return Ok(employees);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            using var conn = CreateConnection();
            await conn.OpenAsync();

            var cmd = new SqlCommand(
                "SELECT * FROM Employee_Details WHERE Emp_ID = @Emp_ID",
                conn
            );
            cmd.Parameters.Add("@Emp_ID", System.Data.SqlDbType.Int).Value = id;

            using var reader = await cmd.ExecuteReaderAsync();

            if (!await reader.ReadAsync())
                return NotFound();

            var employee = new Employee_Details
            {
                Emp_ID = reader.GetInt32(reader.GetOrdinal("Emp_ID")),
                Emp_Name = reader["Emp_Name"]?.ToString(),
                Type = reader["Type"]?.ToString(),
                Gender = reader["Gender"]?.ToString(),
                DOB = reader.GetDateTime(reader.GetOrdinal("DOB")),
                Address = reader["Address"]?.ToString()
            };

            return Ok(employee);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Employee_Details employee)
        {
            using var conn = CreateConnection();
            await conn.OpenAsync();

            var cmd = new SqlCommand(
                @"INSERT INTO Employee_Details
                  (Emp_ID, Emp_Name, Type, Gender, DOB, Address)
                  VALUES
                  (@Emp_ID, @Emp_Name, @Type, @Gender, @DOB, @Address)",
                conn
            );

            cmd.Parameters.Add("@Emp_ID", System.Data.SqlDbType.Int).Value = employee.Emp_ID;
            cmd.Parameters.Add("@Emp_Name", System.Data.SqlDbType.NVarChar).Value = employee.Emp_Name ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Type", System.Data.SqlDbType.NVarChar).Value = employee.Type ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Gender", System.Data.SqlDbType.NVarChar).Value = employee.Gender ?? (object)DBNull.Value;
            cmd.Parameters.Add("@DOB", System.Data.SqlDbType.DateTime).Value = employee.DOB;
            cmd.Parameters.Add("@Address", System.Data.SqlDbType.NVarChar).Value = employee.Address ?? (object)DBNull.Value;

            await cmd.ExecuteNonQueryAsync();

            return CreatedAtAction(nameof(Get), new { id = employee.Emp_ID }, employee);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Employee_Details employee)
        {
            using var conn = CreateConnection();
            await conn.OpenAsync();

            var cmd = new SqlCommand(
                @"UPDATE Employee_Details
                  SET Emp_Name = @Emp_Name,
                      Type = @Type,
                      Gender = @Gender,
                      DOB = @DOB,
                      Address = @Address
                  WHERE Emp_ID = @Emp_ID",
                conn
            );

            cmd.Parameters.Add("@Emp_ID", System.Data.SqlDbType.Int).Value = id;
            cmd.Parameters.Add("@Emp_Name", System.Data.SqlDbType.NVarChar).Value = employee.Emp_Name ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Type", System.Data.SqlDbType.NVarChar).Value = employee.Type ?? (object)DBNull.Value;
            cmd.Parameters.Add("@Gender", System.Data.SqlDbType.NVarChar).Value = employee.Gender ?? (object)DBNull.Value;
            cmd.Parameters.Add("@DOB", System.Data.SqlDbType.DateTime).Value = employee.DOB;
            cmd.Parameters.Add("@Address", System.Data.SqlDbType.NVarChar).Value = employee.Address ?? (object)DBNull.Value;

            var rows = await cmd.ExecuteNonQueryAsync();

            if (rows == 0)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            using var conn = CreateConnection();
            await conn.OpenAsync();

            var cmd = new SqlCommand(
                "DELETE FROM Employee_Details WHERE Emp_ID = @Emp_ID",
                conn
            );
            cmd.Parameters.Add("@Emp_ID", System.Data.SqlDbType.Int).Value = id;

            var rows = await cmd.ExecuteNonQueryAsync();

            if (rows == 0)
                return NotFound();

            return NoContent();
        }
    }
}