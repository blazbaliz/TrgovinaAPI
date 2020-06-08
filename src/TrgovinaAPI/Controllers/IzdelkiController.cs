using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace TrgovinaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IzdelkiController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "to", "je","zakodirano"};
        }
    }
}