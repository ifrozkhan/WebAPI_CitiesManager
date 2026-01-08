using Microsoft.AspNetCore.Mvc;
using CitiesManager.WebAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace CitiesManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private static readonly List<City> Cities = new()
        {
            new City { Id = 1, Name = "New York", Country = "USA" },
            new City { Id = 2, Name = "Tokyo", Country = "Japan" },
            new City { Id = 3, Name = "Paris", Country = "France" }
        };

        // GET: api/Cities/GetCities
        [HttpGet("GetCities")]
        public ActionResult<IEnumerable<City>> GetCities()
        {
            return Ok(Cities);
        }

        // GET: api/Cities/GetCitiesBy/1
        [HttpGet("GetCitiesBy/{id}")]
        public ActionResult<City> GetCitiesBy(int id)
        {
            var city = Cities.FirstOrDefault(c => c.Id == id);

            if (city == null)
                return NotFound();

            return Ok(city);
        }

        // GET: api/Cities/GetAllPopularCities
        [HttpGet("GetAllPopularCities")]
        public ActionResult<IEnumerable<City>> GetPopularCities()
        {
            var popularCities = new List<City>
            {
                new City { Id = 1, Name = "New York", Country = "USA" },
                new City { Id = 2, Name = "Bengaluru", Country = "India" }
            };

            return Ok(popularCities);
        }
    }
}