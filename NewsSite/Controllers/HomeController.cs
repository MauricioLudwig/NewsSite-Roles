using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsSite.Models;

namespace NewsSite.Controllers
{
    [Route("api/home")]
    public class HomeController : Controller
    {

        UserManager<User> userManager;
        SignInManager<User> signInManager;
        NewsSiteContext context;

        public HomeController(UserManager<User> userManager, SignInManager<User> signInManager, NewsSiteContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }

        [HttpGet]
        [Route("getuseremails")]
        public IActionResult GetUserEmails()
        {
            return Ok(userManager.Users
                .Select(o => new IndexVM
                {
                    Id = o.Id,
                    Email = o.UserName
                })
                .OrderBy(o => o.Email)
                .ToArray());
        }

    }
}
