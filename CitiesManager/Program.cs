using CitiesManager.Repositories;
using CitiesManager.WebAPI.DatabaseContext;
using CitiesManager.WebAPI.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// EF Core DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Default")
    );
});

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repository (Dapper)
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IEmployeeFullDetailsRepository, EmployeeFullDetailsRepository>();

//  Register CORS policy
builder.Services.AddCors(options => { options.AddPolicy("AllowAngularClient", policy => policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod()); });

var app = builder.Build();

//  Use the named policy
app.UseCors("AllowAngularClient");

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();