using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shkorpilovtsi.Areas.Admin.Models;
using Shkorpilovtsi.Data;
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
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext context;

        public UserController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {            
            this.userManager = userManager;
            this.context = context;
        }

        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            var categories = await context.Categories.ToListAsync();
            ViewData.Add("categories", categories);
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(UserModel model)
        {
            var user = await userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                user = new(model.Email);
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await context.UserCategories.AddAsync(new() { UserId = user.Id, CategoryId = model.CategoryId });
                    await context.SaveChangesAsync();
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

        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var userCategories = await context.UserCategories.ToListAsync();
            var categories = await context.Categories.ToListAsync();
            var model = new UserModel()
            {
                Id = user.Id,
                Email = user.UserName,
                CategoryId = userCategories.FirstOrDefault((c) => c.UserId.Equals(user.Id))?.CategoryId ?? 0
            };
            ViewData.Add("categories", categories);
            return View(model);
        }

        [HttpGet("ConfirmDelete")]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var userCategories = await context.UserCategories.ToListAsync();
            var categories = await context.Categories.ToListAsync();
            var model = new UserModel()
            {
                Id = user.Id,
                Email = user.UserName,
                CategoryId = userCategories.FirstOrDefault((c) => c.UserId.Equals(user.Id))?.CategoryId ?? 0
            };
            ViewData.Add("categories", categories);
            return View(model);
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(UserModel model)
        {
            var userCategory = await context.UserCategories.FirstOrDefaultAsync((c) => c.UserId.Equals(model.Id));
            if (userCategory != null)
            {
                userCategory.CategoryId = model.CategoryId;
            }
            else
            {
                await context.UserCategories.AddAsync(new()
                {
                    UserId = model.Id,
                    CategoryId = model.CategoryId
                });
            }
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var userCategory = await context.UserCategories.FirstOrDefaultAsync((c) => c.UserId.Equals(user.Id));
            if (userCategory != null)
            {
                context.UserCategories.Remove(userCategory);
                await context.SaveChangesAsync();
            }                        
            return RedirectToAction(nameof(List));
        }

        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            var users = await userManager.Users.ToListAsync();
            var userCategories = await context.UserCategories.ToListAsync();
            var categories = await context.Categories.ToListAsync();
            var model = users.Select((u) => new UserModel() { 
                Id = u.Id,
                Email = u.UserName,
                CategoryId = userCategories.FirstOrDefault((c) => c.UserId.Equals(u.Id))?.CategoryId ?? 0
            }).ToList();
            ViewData.Add("categories", categories);
            return View(model);
        }
    }
}
