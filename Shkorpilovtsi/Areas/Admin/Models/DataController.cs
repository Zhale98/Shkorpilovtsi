using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shkorpilovtsi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shkorpilovtsi.Areas.Admin.Models
{
    public abstract class DataController<TModel> : Controller
    {
        protected readonly IDataService<TModel> service;

        public DataController(IDataService<TModel> service)
        {
            this.service = service;
        }
        #region GET
        [Authorize(Roles = "Administrator")]
        [HttpGet("[controller]/list")]
        public virtual async Task<IActionResult> List()
        {
            var model = await service.GetListAsync();
            return View(model);
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet("[controller]/Create")]
        public virtual IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet("[controller]/Edit")]
        public virtual async Task<IActionResult> Edit(int id)
        {
            var model = await service.ReadAsync(id);
            return View(model);
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet("[controller]/ConfirmDelete")]
        public virtual async Task<IActionResult> ConfirmDelete(int id)
        {
            var model = await service.ReadAsync(id);
            return View(model);
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet("[controller]/Delete")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            await service.DeleteAsync(id);
            return RedirectToAction(nameof(List));
        }
        #endregion
        #region POST
        [Authorize(Roles = "Administrator")]
        [HttpPost("[controller]/Create")]
        public virtual async Task<IActionResult> Create(TModel model)
        {
            await service.CreateAsync(model);
            return RedirectToAction(nameof(List));
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost("[controller]/Edit")]
        public virtual async Task<IActionResult> Edit(int id, TModel model)
        {
            await service.UpdateAsync(id, model);
            return RedirectToAction(nameof(List));
        }
        #endregion
    }
}
