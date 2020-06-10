using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TrgovinaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace TrgovinaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UporabnikiController : ControllerBase
    {
        private readonly UporabnikiContext _context;

        public UporabnikiController(UporabnikiContext context) => _context = context;

        //GET:      api/Uporabniki
        [HttpGet]
        public ActionResult<IEnumerable<Uporabnik>> GetUporabniki()
        {
            return _context.Uporabniki;
        }

        //GET:      api/Uporabniki/{id}
        [HttpGet("{id}")]
        public ActionResult<Uporabnik> GetUporabniki(int id)
        {
            var uporabnik = _context.Uporabniki.Find(id);

            if(uporabnik == null)
            {
                return NotFound();
            }

            return uporabnik;
        }

        //POST:     api/Uporabniki/
        [HttpPost]
        public ActionResult<Uporabnik> PostUporabniki(Uporabnik uporabnik)
        {
            _context.Uporabniki.Add(uporabnik);

            try
            {
                _context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return CreatedAtAction("GetUporabniki", new Uporabnik{Id = uporabnik.Id}, uporabnik);
        }

        //PUT:      api/Uporabniki/{id}
        [HttpPut("{id}")]
        public ActionResult PutUporabniki(int id, Uporabnik uporabnik)
        {
            if (id != uporabnik.Id)
            {
                return BadRequest();
            }

            _context.Entry(uporabnik).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }


        //DELETE:       api/Uporabniki/{id}
        [HttpDelete("{id}")]
        public ActionResult<Uporabnik> DeleteUporabniki(int id)
        {
            var uporabnik = _context.Uporabniki.Find(id);

            if (uporabnik == null)
            {
                return NotFound();
            }

            _context.Uporabniki.Remove(uporabnik);
            _context.SaveChanges();

            return uporabnik;
        }
    }
}