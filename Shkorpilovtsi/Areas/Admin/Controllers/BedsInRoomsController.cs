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
    [Area("admin")]
    [Route("area")]
    public class BedsInRoomsController : DataController<BedsInRoom>
    {
        private readonly ApplicationDbContext context;

        public BedsInRoomsController(IDataService<BedsInRoom> service, ApplicationDbContext context) : base(service)
        {
            this.context = context;
        }
        [Authorize(Roles = "Administrator")]
        public override IActionResult Create()
        {            
            ViewData.Add("rooms", context.Rooms.ToList());
            ViewData.Add("beds", context.Beds.ToList());
            return base.Create();
        }
        [Authorize(Roles = "Administrator")]
        public override Task<IActionResult> Edit(int id)
        {
            ViewData.Add("beds", context.Beds.ToList());                       
            return base.Edit(id);
        }        
    }
}
