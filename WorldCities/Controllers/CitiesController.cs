using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldCities.Data;
using WorldCities.Models;

namespace WorldCities.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CitiesController(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<ApiResult<City>>> GetCities(int pageIndex = 0, int pageSize = 10) {
            return await ApiResult<City>.CreateAsync(
                    _context.Cities,
                    pageIndex,
                    pageSize
                );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(int id) {
            
            City city = await _context.Cities.FindAsync(id);
            
            if (city == null) {

                return NotFound("The city with id " + id + " don't exist.");

            }

            return Ok(city);

        }

        [HttpPost]
        public async Task<ActionResult<City>> PostCity([FromBody] City city) {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Add(city);

            await _context.SaveChangesAsync();

            return Ok(city);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<City>> PutCity(int id, [FromBody] City city) {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != city.CityId)
                return BadRequest("Primary keys don't match");

            City oldCity = await _context.Cities.FindAsync(id);

            if (oldCity == null) {

                return NotFound("The city with id " + id + " don't exist.");

            }

            oldCity.Name = city.Name;
            oldCity.Code = city.Code;
            oldCity.Lat = city.Lat;
            oldCity.Lng = city.Lng;

            _context.Cities.Update(oldCity);

            await _context.SaveChangesAsync();

            return Ok(oldCity);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCity(int id) {

            City city = await _context.Cities.FindAsync(id);

            if (city == null)
            {

                return NotFound("The city with id " + id + " don't exist.");

            } 

            _context.Remove(city);

            await _context.SaveChangesAsync();

            return Ok();

        }

    }
}
