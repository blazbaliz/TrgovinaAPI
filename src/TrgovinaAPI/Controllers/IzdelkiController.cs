using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrgovinaAPI.Models;

namespace TrgovinaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IzdelkiController : ControllerBase
    {
        private readonly IzdelekContext _context;

        public IzdelkiController(IzdelekContext context) => _context = context;


        //GET:      api/Izdelki
        [HttpGet]
        public ActionResult<IEnumerable<Izdelek>> GetIzdelki()
        {
            return _context.Izdelki;
        }

        //GET:      api/Izdelki/{Id}
        [HttpGet("{id}")]
        public ActionResult<Izdelek> GetIzdelki(int id)
        {
            var Izdelek = _context.Izdelki.Find(id);

            if(Izdelek == null)
            {
                return NotFound();
            }

            return Izdelek;
        }

        //POST:     api/Izdelki
        [HttpPost]

        public ActionResult<Izdelek> PostIzdelki(Izdelek izdelek)
        {
            _context.Izdelki.Add(izdelek);

            try
            {
                _context.SaveChanges();
            }
            catch
            {
               return BadRequest();
            }
            
            return CreatedAtAction("GetIzdelki", new Izdelek{Id = izdelek.Id}, izdelek);
        }

        //PUT:      api/Izdelki/{Id}
        [HttpPut("{id}")]

        public ActionResult PutIzdelki(int id, Izdelek izdelek)
        {
            if(id != izdelek.Id)
            {
                return BadRequest();
            }

            _context.Entry(izdelek).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        //DELETE:       api/Izdelki/{Id}
        [HttpDelete("{id}")]
        public ActionResult<Izdelek> DeleteIzdelki(int id)
        {
            var izdelek = _context.Izdelki.Find(id);

            if (izdelek == null)
            {
                return NotFound();
            }

            _context.Izdelki.Remove(izdelek);
            _context.SaveChanges();

            return izdelek;
        }
    }
}