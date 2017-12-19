using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NewsSite.Controllers
{
    [Route("api/home")]
    public class HomeController : Controller
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
