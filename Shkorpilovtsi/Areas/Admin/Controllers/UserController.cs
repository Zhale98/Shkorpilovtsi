using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shkorpilovtsi.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shkorpilovtsi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]")]
    public class UserController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public UserController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpGet("Add")]
        public async Task<IActionResult> Add()
        {
            var roles = await roleManager.Roles.ToListAsync();
            ViewData.Add("roles", roles);
            return View();
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(UserModel model)
        {
            var user = await userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                user = new(model.Email);
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (!String.IsNullOrEmpty(model.RoleId))
                    {
                        var role = await roleManager.FindByIdAsync(model.RoleId);
                        if (role != null)
                        {
                            await userManager.AddToRoleAsync(user, role.Name);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", result.Errors?.FirstOrDefault()?.Description);
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("EMail", "Има въведен потребител с това име");
                return View(model);
            }
            return RedirectToAction(nameof(List));
        }

        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            var model = await userManager.Users.ToListAsync();
            var roles = await roleManager.Roles.ToListAsync();
            ViewData.Add("roles", roles);
            return View(model);
        }
    }
}
