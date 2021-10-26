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
    public class ShiftService : IDataService<Shift>
    {
        private readonly ApplicationDbContext context;

        public ShiftService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Shift> CreateAsync(Shift shift)
        {
            var entry = await context.Shifts.AddAsync(shift);
            await context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var shift = await ReadAsync(id);
            if (shift != null)
            {
                context.Shifts.Remove(shift);
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Shift>> GetListAsync()
        {
            return await context.Shifts.ToListAsync();
        }

        public async Task<Shift> ReadAsync(int id)
        {
            return await context.Shifts.FindAsync(id);
        }

        public async Task<Shift> UpdateAsync(int id, Shift shift)
        {
            var entry = await ReadAsync(id);
            if (entry != null)
            {
                entry.Number = shift.Number;                
                entry.StartDate = shift.StartDate;
                entry.EndDate = shift.EndDate;
                await context.SaveChangesAsync();
            }
            return entry;
        }
    }
}
