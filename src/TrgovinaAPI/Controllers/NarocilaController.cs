using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrgovinaAPI.Models;

namespace TrgovinaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NarocilaController : ControllerBase
    {
        private readonly NarocilaContext _context;

        public NarocilaController(NarocilaContext context) => _context = context;

        //GET:      api/Narocila
        [HttpGet]
        public ActionResult<IEnumerable<Narocilo>> GetNarocila()
        {
            return _context.Narocila;
        }

        //GET:      api/Narocila/{id}
        [HttpGet("{id}")]
        public ActionResult<Narocilo> GetNarocila(int id)
        {
            var narocilo = _context.Narocila.Find(id);

            if(narocilo == null)
            {
                return NotFound();
            }

            return narocilo;
        }

        //POST:     api/Narocila
        [HttpPost]
        public ActionResult<Narocilo> PostNarocila(Narocilo narocilo)
        {
            _context.Narocila.Add(narocilo);
            try
            {
            _context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return CreatedAtAction("GetNarocila", new Narocilo{Id = narocilo.Id}, narocilo);
        }

        //PUT:      api/Narocila/{id}
        [HttpPut("{id}")]
        public ActionResult PutNarocila(int id, Narocilo narocilo)
        {
            if(id != narocilo.Id)
            {
                return BadRequest();
            }

            _context.Entry(narocilo).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        //DELETE:       api/Narocila/{Id}
        [HttpDelete("{id}")]
        public ActionResult<Narocilo> DeleteNarocila(int id)
        {
            var narocilo = _context.Narocila.Find(id);

            if(narocilo == null)
            {
                return NotFound();
            }

            _context.Narocila.Remove(narocilo);
            _context.SaveChanges();

            return narocilo;
        }
    }
}