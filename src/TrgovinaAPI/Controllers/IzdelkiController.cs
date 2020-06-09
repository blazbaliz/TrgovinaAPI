using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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
    }
}