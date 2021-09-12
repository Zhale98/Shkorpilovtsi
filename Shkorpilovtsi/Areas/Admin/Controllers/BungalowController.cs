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
    public class BungalowController : DataController<Bungalow>
    {
        private readonly ApplicationDbContext context;

        public BungalowController(IDataService<Bungalow> service, ApplicationDbContext context) : base(service)
        {
            this.context = context;
        }
        [Authorize(Roles = "Administrator")]
        public override async Task<IActionResult> Edit(int id)
        {            
            var rooms = await context.RoomsInBungalows.Include((o) => o.Room).Where((o) => o.BungalowId == id).Select((o) => o.Room).ToListAsync();
            ViewData.Add("rooms", rooms);            
            return await base.Edit(id);
        }
        [Authorize(Roles = "Administrator")]
        public override async Task<IActionResult> List()
        {
            var roomsInBungalows = await context.RoomsInBungalows.Include((o) => o.Room).ToListAsync();
            var bedsInRooms = await context.BedsInRooms.Include((o) => o.Bed).ToListAsync();
            ViewData.Add("rooms", roomsInBungalows);
            ViewData.Add("bedsInRooms", bedsInRooms);
            return await base .List();
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet("[controller]/DeleteRoom")]
        public async Task<IActionResult> DeleteRoom(int id, int bungalowId)
        {
            var roomInBungalow = await context.RoomsInBungalows.FirstOrDefaultAsync((o) => o.BungalowId == bungalowId && o.RoomId == id);
            if (roomInBungalow != null)
            {
                context.RoomsInBungalows.Remove(roomInBungalow);
                await context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Edit), new { id = bungalowId });
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet("[controller]/AddRoomToBungalow")]
        public async Task<IActionResult> AddRoomToBungalow()
        {
            ViewData.Add("rooms", await context.Rooms.ToListAsync());
            ViewData.Add("bungalows", await context.Bungalows.ToListAsync());
            return View();
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost("[controller]/AddRoomToBungalow")]
        public async Task<IActionResult> AddRoomToBungalow(RoomsInBungalow model)
        {
            await context.RoomsInBungalows.AddAsync(model);
            await context.SaveChangesAsync();
            ViewData.Add("rooms", await context.Rooms.ToListAsync());
            ViewData.Add("bungalows", await context.Bungalows.ToListAsync());
            ViewData.Add("info", "СТАЯТА Е ДОБАВЕНА УСПЕШНО");
            return View();
        }
    }
}
