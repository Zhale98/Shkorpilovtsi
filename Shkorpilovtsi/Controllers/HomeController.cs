﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shkorpilovtsi.Data;
using Shkorpilovtsi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Shkorpilovtsi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<PartialViewResult> GetMap()
        {
            var model = await GetBungaloViewModels();
            return PartialView("components/map", model);
        }

        public async Task<PartialViewResult> GetBungalowModal(int id)
        {
            var bungalow = await context.Bungalows.FindAsync(id);
            var roomsInBungalow = await context.RoomsInBungalows
                .Include((o) => o.Room)
                .Where((r) => r.BungalowId == id).ToListAsync();
            var bedsInRooms = await context.BedsInRooms.Include((o) => o.Bed).ToListAsync();
            var model = new BungalowViewModel()
            {
                Id = bungalow.Id,
                Number = bungalow.Number,
                Description = bungalow.Description,
                Rooms = roomsInBungalow.Select((o) => o.Room).ToList(),
                BedsInRooms = bedsInRooms
            };            
            return PartialView("components/bungalowmodal", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }        

        private async Task<List<BungalowViewModel>> GetBungaloViewModels()
        {
            var bungalows = await context.Bungalows.ToListAsync();
            var roomsInBungalow = await context.RoomsInBungalows.Include((o) => o.Room).ToListAsync();
            var model = new List<BungalowViewModel>(bungalows.Select((bungalow) => new BungalowViewModel()
            {
                Id = bungalow.Id,
                Number = bungalow.Number,
                Coords = bungalow.MapCoords,
                Description = bungalow.Description,
                HasFridge = roomsInBungalow.Where((r) => r.BungalowId == bungalow.Id).Any((r) => r.Room.HasFridge),
                HasWc = roomsInBungalow.Where((r) => r.BungalowId == bungalow.Id).Any((r) => r.Room.IsWc),
                HasSofa = roomsInBungalow.Where((r) => r.BungalowId == bungalow.Id).Any((r) => r.Room.HasSofa)
            }));
            return model;
        }
    }
}
