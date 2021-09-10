using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shkorpilovtsi.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace Shkorpilovtsi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[Area]")]
    public class HomeController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public HomeController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Init")]
        public IActionResult Initialize()
        {
            return View();
        }

        [HttpPost("Init")]
        public async Task<IActionResult> Initialize(InitializationModel model)
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new() { Name = "Administrator", NormalizedName = "administrator" });                
            }
            if (!userManager.Users.Any())
            {
                var user = new IdentityUser(model.Email);
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "administrator");
                }
                else
                {
                    ModelState.AddModelError("Password", result.Errors?.FirstOrDefault()?.Description);
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("Email", "Вече има въведен администратор в системата");
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
