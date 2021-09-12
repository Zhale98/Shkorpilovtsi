using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shkorpilovtsi.Areas.Admin.Models;
using Shkorpilovtsi.Data;
using Shkorpilovtsi.Interfaces;
using Shkorpilovtsi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shkorpilovtsi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("area")]
    public class PriceController : DataController<Price>
    {
        private readonly ApplicationDbContext context;

        public PriceController(IDataService<Price> service, ApplicationDbContext context) : base(service)
        {
            this.context = context;
        }
        [Authorize(Roles = "Administrator")]
        public override IActionResult Create()
        {
            var shifts = context.Shifts.ToList();
            var categories = context.Categories.ToList();
            ViewData.Add("shifts", shifts);
            ViewData.Add("categories", categories);
            return base.Create();
        }
        [Authorize(Roles = "Administrator")]
        public override async Task<IActionResult> Edit(int id)
        {
            var shifts = await context.Shifts.ToListAsync();
            var categories = await context.Categories.ToListAsync();
            ViewData.Add("shifts", shifts);
            ViewData.Add("categories", categories);
            return await base.Edit(id);
        }
    }
}

