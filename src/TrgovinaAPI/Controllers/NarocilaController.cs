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

        
    }
}