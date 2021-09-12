using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shkorpilovtsi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet("[controller]/List")]
        public async Task<IActionResult> List()
        {
            var model = await roleManager.Roles.ToListAsync();
            return View(model);
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet("[controller]/Add")]
        public IActionResult Add()
        {            
            return View();
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost("[controller]/Add")]
        public async Task<IActionResult> Add(IdentityRole model)
        {
            var result = await roleManager.CreateAsync(model);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(List));
            }
            else
            {
                ModelState.AddModelError("Name", result.Errors?.FirstOrDefault()?.Description);
                return View(model);
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet("[controller]/Edit")]
        public async Task<IActionResult> Edit(string id)
        {
            var model = await roleManager.FindByIdAsync(id);
            return View(model);
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet("[controller]/Edit")]
        public async Task<IActionResult> Edit(IdentityRole model)
        {
            var result = await roleManager.UpdateAsync(model);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(List));
            }
            else
            {
                ModelState.AddModelError("Name", result.Errors?.FirstOrDefault()?.Description);
                return View(model);
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet("[controller]/delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                await roleManager.DeleteAsync(role);
            }
            return RedirectToAction(nameof(List));
        }
    }
}
