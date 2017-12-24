using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        RoleManager<IdentityRole> roleManager;
        NewsSiteContext context;

        public NewsController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, NewsSiteContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("open")]
        public IActionResult AllowAccessToOpenNews()
        {
            return Ok("Access to open news allowed");
        }

        [Authorize(Policy = "AccessToHiddenNews")]
        [HttpGet]
        [Route("hidden")]
        public IActionResult AllowAccessToHiddenNews()
        {
            return Ok("Access to hidden news allowed");
        }

        [HttpGet]
        [Route("hiddenandadult")]
        [Authorize(Policy = "AccessToHiddenNews")]
        [Authorize(Policy = "Adult")]
        public IActionResult HiddenAndAdult()
        {
            return Ok("You can view this article and");
        }

        [HttpGet]
        [Route("sports")]
        [Authorize(Policy = "PublishSports")]
        public IActionResult PublishSportsArticle()
        {
            return Ok("You may submit sports article");
        }

        [HttpGet]
        [Route("culture")]
        [Authorize(Policy = "PublishCulture")]
        public IActionResult PublishCultureArticle()
        {
            return Ok("You may submit culture article");
        }

    }
}
