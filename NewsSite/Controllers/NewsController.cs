using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsSite.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewsSite.Controllers
{
    [Route("api/news")]
    public class NewsController : Controller
    {

        UserManager<User> userManager;
        SignInManager<User> signInManager;
        RoleManager<UserRole> roleManager;
        NewsSiteContext context;

        public NewsController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<UserRole> roleManager, NewsSiteContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.context = context;
        }


        [HttpGet]
        public IActionResult OpenArticle()
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult HiddenArticle()
        {
            return Ok();
        }

    }
}
