using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shkorpilovtsi.Data;
using Shkorpilovtsi.Models;
using Shkorpilovtsi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shkorpilovtsi.Controllers
{
    [Route("[controller]")]
    public class ReservationController : Controller
    {
        private readonly ReservationService service;
        private readonly ApplicationDbContext context;

        public ReservationController(ReservationService service, ApplicationDbContext context)
        {
            this.service = service;
            this.context = context;
        }
        [Authorize]
        [HttpGet("Index")]
        public async Task<IActionResult> Index(int? shiftId = null, int? bungalowId = null)
        {
            var shifts = await context.Shifts.Where((s) => s.StartDate.Year == DateTime.Now.Year).ToListAsync();            
            var bungalows = await context.Bungalows.ToListAsync();
            ViewData.Add("shifts", shifts);            
            ViewData.Add("bungalows", bungalows);
            Reservation model = new();
            if (shiftId.HasValue) model.ShiftId = shiftId.Value;
            if (bungalowId.HasValue) model.BungalowId = bungalowId.Value;
            return View("Index", model);
        }
        [Authorize]
        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            return await Index();
        }
        [Authorize]
        [HttpGet("CreateByShift")]
        public async Task<IActionResult> CreateByShift(int id)
        {            
            return await Index(shiftId: id);
        }
        [Authorize]
        [HttpGet("CreateByBungalow")]
        public async Task<IActionResult> CreateByBungalow(int id)
        {
            return await Index(bungalowId: id);
        }      
        [Authorize]
        [HttpGet("DeleteReservationDetail")]
        public async Task<IActionResult> DeleteReservationDetail(int reservationId, int detailId)
        {
            await service.DeleteReservationDetailAsync(detailId);
            return RedirectToAction(nameof(CreateReservation), new { id = reservationId });
        }
        [Authorize]
        [HttpGet("CreateReservation")]
        public async Task<IActionResult> CreateReservation(int id)
        {
            var model = await context.Reservations.FindAsync(id);
            var shift = await context.Shifts.FindAsync(model.ShiftId);
            var bungalow = await context.Bungalows.FindAsync(model.BungalowId);
            var categories = await context.Categories.ToListAsync();
            var roomsInBungalow = await context.RoomsInBungalows.Include((o) => o.Room).Where((o) => o.BungalowId == bungalow.Id).ToListAsync();
            var bedsInRooms = await context.BedsInRooms.Include((o) => o.Bed).ToListAsync();
            var reservationDetails = await context.ReservationDetails.Where((o) => o.ReservationId == id).ToListAsync();
            ViewData.Add("shift", shift);
            ViewData.Add("bungalow", bungalow);
            ViewData.Add("categories", categories);
            ViewData.Add("roomsInBungalow", roomsInBungalow);
            ViewData.Add("bedsInRooms", bedsInRooms);
            ViewData.Add("reservationDetails", reservationDetails);
            return View("CreateReservation", model);
        }
        [HttpPost("CreateReservation")]
        public async Task<IActionResult> CreateReservation(int reservationId, int categoryId, string description)
        {            
            var model = await context.Reservations.FindAsync(reservationId);
            await service.CreateReservatonDetailAsync(model, description, categoryId);
            var shift = await context.Shifts.FindAsync(model.ShiftId);
            var bungalow = await context.Bungalows.FindAsync(model.BungalowId);
            var categories = await context.Categories.ToListAsync();
            var roomsInBungalow = await context.RoomsInBungalows.Include((o) => o.Room).Where((o) => o.BungalowId == bungalow.Id).ToListAsync();
            var bedsInRooms = await context.BedsInRooms.Include((o) => o.Bed).ToListAsync();
            var reservationDetails = await context.ReservationDetails.Where((o) => o.ReservationId == reservationId).ToListAsync();
            ViewData.Add("shift", shift);
            ViewData.Add("bungalow", bungalow);
            ViewData.Add("categories", categories);
            ViewData.Add("roomsInBungalow", roomsInBungalow);
            ViewData.Add("bedsInRooms", bedsInRooms);
            ViewData.Add("reservationDetails", reservationDetails);
            return View("CreateReservation", model);
        }
        [HttpPost("Index")]
        public async Task<IActionResult> Index(Reservation model)
        {
            var shift = await context.Shifts.FindAsync(model.ShiftId);
            if (!await service.IsBungalowAvailable(model.BungalowId, model.StartDate ?? shift.StartDate, model.EndDate ?? shift.EndDate))
            {
                ModelState.AddModelError("BungalowId", "Бунгалото е заето в този период");
                var shifts = await context.Shifts.Where((s) => s.StartDate.Year == DateTime.Now.Year).ToListAsync();
                var bungalows = await context.Bungalows.ToListAsync();
                ViewData.Add("shifts", shifts);
                ViewData.Add("bungalows", bungalows);
                return View("Index", model);
            }
            model.Approved = true;
            model.Cancelled = false;
            model.DateCreated = DateTime.Now;
            var result = await service.CreateAsync(model);
            return RedirectToAction(nameof(CreateReservation), new { id = result.Id });
        }
    }
}
