using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBookApp.Server.Data;

namespace PhoneBookApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
       private readonly PhoneBookDbContext _context;

        public PersonController(PhoneBookDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Person>>> GetPeople()
        {
            var persons = await _context.People.ToListAsync();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var result = _context.People.FirstOrDefault(p => p.Id == id);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]

        public async Task<ActionResult<List<Person>>> AddPerson(Person person)
        {
            //person.EmailAddress = null;
            _context.People.Add(person);
            await _context.SaveChangesAsync();

            return Ok(await GetDbPeople());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Person>>> UpdatePerson(Person person, int id)
        {
            var result = await _context.People.FirstOrDefaultAsync(p => p.Id == id);

            if( result == null)
                return NotFound();

            result.Name = person.Name;
            result.Surname = person.Surname;
            result.PhoneNumber = person.PhoneNumber;
            result.EmailAddress = person.EmailAddress;

            await _context.SaveChangesAsync();

            return Ok(await GetDbPeople());

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Person>>> DeletePerson(int id)
        {
            var person = await _context.People.FirstOrDefaultAsync(p => p.Id == id);

            if (person == null)
                return NotFound();

            _context.People.Remove(person);
            await _context.SaveChangesAsync();

            return Ok(await GetDbPeople());

        }
        public async Task<List<Person>> GetDbPeople()
        {
            return await _context.People.ToListAsync();
        }
    }
}
