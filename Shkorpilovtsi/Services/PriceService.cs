using Microsoft.EntityFrameworkCore;
using Shkorpilovtsi.Data;
using Shkorpilovtsi.Interfaces;
using Shkorpilovtsi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shkorpilovtsi.Services
{
    public class PriceService : IDataService<Price>
    {
        private readonly ApplicationDbContext context;

        public PriceService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Price> CreateAsync(Price price)
        {
            var entry = await context.Prices.AddAsync(price);
            await context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var price = await ReadAsync(id);
            if (price != null)
            {
                context.Prices.Remove(price);
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Price>> GetListAsync()
        {
            return await context.Prices.Include((p) => p.Category).Include((p) => p.Shift).ToListAsync();
        }

        public async Task<Price> ReadAsync(int id)
        {
            return await context.Prices.Include((o) => o.Category).Include((o) => o.Shift).FirstOrDefaultAsync((p) => p.Id == id);
        }

        public async Task<Price> UpdateAsync(int id, Price price)
        {
            var entry = await ReadAsync(id);
            if (entry != null)
            {
                entry.ShiftId = price.ShiftId;
                entry.CategoryId = price.CategoryId;
                entry.SingleDayPrice = price.SingleDayPrice;
                entry.FullPrice = price.FullPrice;
                await context.SaveChangesAsync();
            }
            return entry;
        }
    }
}
