using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace TrgovinaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UporabnikiController : ControllerBase
    {
        //GET:      api/Uporabniki
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] {"sdf", "ssd"};
        }
    }
}