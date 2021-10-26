using Microsoft.EntityFrameworkCore;
using Shkorpilovtsi.Data;
using Shkorpilovtsi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shkorpilovtsi.Services
{
    public class ReservationService
    {
        private readonly ApplicationDbContext context;

        public ReservationService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Reservation> GetReservationAsync(int id)
        {
            return await context.Reservations.FindAsync(id);
        }

        public async Task<List<ReservationDetail>> GetReservationDetailsAsync(int id)
        {
            return await context.ReservationDetails.Where((o) => o.ReservationId == id).ToListAsync();
        }

        public async Task<bool> IsBungalowAvailable(int bungalowId, DateTime startDate, DateTime endDate)
        {
            var bungalow = await context.Bungalows.FindAsync(bungalowId);
            if (bungalow?.IsActive == true)
            {
                var reservations = await context.Reservations.Where((r) => r.BungalowId == bungalowId).ToListAsync();
                return !reservations.Any((r) => (r.StartDate > startDate && r.StartDate < endDate) || (r.EndDate > startDate && r.EndDate < endDate));
            }
            return false;                
        }

        public async Task<Reservation> CreateAsync(Reservation reservation)
        {
            var entry = await context.Reservations.AddAsync(reservation);
            await context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<ReservationDetail> CreateReservatonDetailAsync(Reservation reservation, string description, int? categoryId = null, int? specialFeeId = null)
        {
            var detail = new ReservationDetail()
            {
                ReservationId = reservation.Id,
                Description = description,
                CategoryId = categoryId,
                SpecialFeeId = specialFeeId,
                Price = await CalculateReservationDetailPrice(reservation, categoryId, specialFeeId)
            };
            var entry = await context.ReservationDetails.AddAsync(detail);
            await context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task DeleteReservationDetailAsync(int id)
        {
            var detail = await context.ReservationDetails.FindAsync(id);
            if (detail != null)
            {
                context.ReservationDetails.Remove(detail);
                await context.SaveChangesAsync();
            }            
        }        

        public async Task<decimal> CalculateReservationDetailPrice(Reservation reservation, int? categoryId, int? specialFeeId)
        {
            decimal singlePrice = 0;
            decimal fullPrice = 0;            
            if (categoryId.HasValue)
            {
                var price = await context.Prices.FirstOrDefaultAsync((p) => p.ShiftId == reservation.ShiftId && p.CategoryId == categoryId);
                if (price != null)
                {
                    singlePrice = price.SingleDayPrice;
                    fullPrice = price.FullPrice;
                }
                var specialFeeForCategory = await context.SpecialFees.FirstOrDefaultAsync((f) => f.ShiftId == reservation.ShiftId && f.CategoryId == categoryId);
                if (specialFeeForCategory != null)
                {
                    singlePrice += specialFeeForCategory.SingleDayPrice;
                    fullPrice += specialFeeForCategory.FullPrice;
                }
            }
            if (specialFeeId.HasValue)
            {
                var specialFee = await context.SpecialFees.FindAsync(specialFeeId);
                if (specialFee != null)
                {
                    singlePrice += specialFee.SingleDayPrice;
                    fullPrice += specialFee.FullPrice;
                }
            }
            int? daysCount = (int?)(reservation.EndDate - reservation.StartDate)?.TotalDays;
            return daysCount.HasValue ? daysCount.Value * singlePrice : fullPrice;
        }        
        
    }
}
