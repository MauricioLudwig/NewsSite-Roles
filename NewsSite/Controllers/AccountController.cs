using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsSite.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewsSite.Controllers
{
    [Route("api/account")]
    public class AccountController : Controller
    {

        UserManager<User> userManager;
        SignInManager<User> signInManager;
        RoleManager<IdentityRole> roleManager;
        NewsSiteContext context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, NewsSiteContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.context = context;
        }

        [HttpGet]
        [Route("addroles")]
        public async Task AddRolesToIdentity()
        {

            string[] roles = { "Administrator", "Publisher", "Subscriber" };

            foreach (var role in roles)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);
                if (!roleExists)
                {
                    var newRole = new IdentityRole(role);
                    await roleManager.CreateAsync(newRole);
                }
            }
        }

        [HttpGet]
        [Route("resetdb")]
        public async Task<IActionResult> ClearDbAndRetrieveDefaultUsers()
        {

            context.RemoveRange(userManager.Users);
            context.SaveChanges();

            var users = new List<DefaultUser>
            {
                new DefaultUser { FirstName = "Adam", Email = "adam@gmail.com", Role = "Administrator", Age = null },
                new DefaultUser { FirstName = "Peter", Email = "peter@gmail.com", Role = "Publisher", Age = null },
                new DefaultUser { FirstName = "Susan", Email = "susan@gmail.com", Role = "Subscriber", Age = 48 },
                new DefaultUser { FirstName = "Viktor", Email = "viktor@gmail.com", Role = "Subscriber", Age = 15 },
                new DefaultUser { FirstName = "Xerxes", Email = "xerxes@gmail.com", Role = null, Age = null },
            };

            foreach (var user in users)
            {
                var newUser = new User
                {
                    UserName = user.Email,
                    Age = user.Age,
                    FirstName = user.FirstName
                };

                var result = await userManager.CreateAsync(newUser);

                if (result.Succeeded)
                {
                    if (user.Role != null)
                        await userManager.AddToRoleAsync(newUser, user.Role);

                    switch (user.Role)
                    {
                        case "Publisher":
                            switch (user.Email)
                            {
                                case "peter@gmail.com":
                                    await userManager.AddClaimAsync(newUser, new Claim("publication", "sports"));
                                    await userManager.AddClaimAsync(newUser, new Claim("publication", "culture"));
                                    break;
                            }
                            break;
                        case "Subscriber":
                            break;
                        case "Administrator":
                            await userManager.AddClaimAsync(newUser, new Claim("publication", "admin"));
                            break;
                    }
                }
            }

            context.SaveChanges();
            return Ok(userManager.Users);
        }

        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> SignIn(string viewModel)
        {

            if (!ModelState.IsValid || String.IsNullOrEmpty(viewModel))
                return BadRequest("Bad request");

            var user = await userManager.FindByNameAsync(viewModel);

            if (user != null)
            {
                await signInManager.SignInAsync(user, false);
                return Ok($"{user.FirstName} was successfully signed in");
            }
            else
            {
                return NotFound($"User {viewModel} was not found");
            }

        }

        [HttpGet]
        [Route("removeusers")]
        public async Task<IActionResult> Remove()
        {
            await signInManager.SignOutAsync();
            context.RemoveRange(userManager.Users);
            context.SaveChanges();
            return Ok();
        }

    }
}
