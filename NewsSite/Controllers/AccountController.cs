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
    [Route("api/account")]
    public class AccountController : Controller
    {

        static readonly string[] roles = { "Administrator" , "Publisher", "Subscriber" };

        UserManager<User> userManager;
        SignInManager<User> signInManager;
        RoleManager<UserRole> roleManager;
        NewsSiteContext context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<UserRole> roleManager, NewsSiteContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.context = context;
        }

        internal async void AddRolesToIdentity()
        {
            foreach (var role in roles)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);

                if (!roleExists)
                {
                    var newRole = new UserRole();
                    newRole.Name = role;
                    await roleManager.CreateAsync(newRole);
                }
            }
        }

        [HttpGet]
        [Route("resetdb")]
        public async Task<IActionResult> ClearDbAndRetrieveDefaultUsers()
        {

            context.RemoveRange(userManager.Users);

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
                }
            }

            return Ok(userManager.Users);
        }

        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> SignIn(string viewModel)
        {

            if (!ModelState.IsValid)
                return View(viewModel);

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

    }
}
