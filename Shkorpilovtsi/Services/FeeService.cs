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
    public class FeeService : IDataService<SpecialFee>
    {
        private readonly ApplicationDbContext context;

        public FeeService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<SpecialFee> CreateAsync(SpecialFee fee)
        {
            await context.SpecialFees.AddAsync(fee);
            await context.SaveChangesAsync();
            return fee;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var fee = await ReadAsync(id);
            if (fee != null)
            {
                context.SpecialFees.Remove(fee);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<SpecialFee>> GetListAsync()
        {
            return await context.SpecialFees.ToListAsync();
        }

        public async Task<SpecialFee> ReadAsync(int id)
        {
            return await context.SpecialFees.FindAsync(id);
        }

        public async Task<SpecialFee> UpdateAsync(int id, SpecialFee fee)
        {
            var entry = await ReadAsync(id);
            if (entry != null)
            {
                entry.Name = fee.Name;
                entry.SingleDayPrice = fee.SingleDayPrice;
                entry.FullPrice = fee.FullPrice;
                entry.CategoryId = fee.CategoryId;
                entry.ShiftId = fee.ShiftId;
                await context.SaveChangesAsync();
            }
            return entry;
        }
    }
}
