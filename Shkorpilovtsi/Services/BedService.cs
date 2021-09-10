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
    public class BedService : IDataService<Bed>
    {
        private readonly ApplicationDbContext context;

        public BedService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Bed> CreateAsync(Bed bed)
        {
            var entry = await context.Beds.AddAsync(bed);            
            await context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var bed = await ReadAsync(id);
            if (bed != null)
            {
                context.Beds.Remove(bed);
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }            
        }

        public async Task<List<Bed>> GetListAsync()
        {
            return await context.Beds.ToListAsync();
        }

        public async Task<Bed> ReadAsync(int id)
        {
            return await context.Beds.FindAsync(id);
        }        

        public async Task<Bed> UpdateAsync(int id, Bed bed)
        {
            var entry = await ReadAsync(id);
            if (entry != null)
            {
                entry.Capacity = bed.Capacity;
                entry.Description = bed.Description;
                await context.SaveChangesAsync();
            }
            return entry;
        }
    }
}
