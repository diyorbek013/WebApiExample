using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;
using SuperHeroAPI.Models;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = _context.SuperHeroes.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero does not excist ");
            }
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> Addhero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }


        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> Update(SuperHero request)
        {
            var db = await _context.SuperHeroes.FindAsync(request.Id);
            if (db == null)
            {
                return BadRequest("Not Found");
            }

            db.Name = request.Name;
            db.FirstName = request.FirstName;
            db.LastName = request.LastName;
            db.Place = request.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var db = await _context.SuperHeroes.FindAsync(id);
            if (db == null)
                return BadRequest("Hero not found.");

            _context.SuperHeroes.Remove(db);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
