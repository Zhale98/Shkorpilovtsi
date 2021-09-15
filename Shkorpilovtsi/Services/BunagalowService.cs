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
    public class BunagalowService : IDataService<Bungalow>
    {
        private readonly ApplicationDbContext context;

        public BunagalowService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Bungalow> CreateAsync(Bungalow bungalow)
        {
            var entry = await context.Bungalows.AddAsync(bungalow);
            await context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var bungalow = await ReadAsync(id);
            if (bungalow != null)
            {
                var roomsInBungalow = await context.RoomsInBungalows.Where((r) => r.BungalowId == id).ToListAsync();
                if (roomsInBungalow?.Count > 0)
                {
                    context.RoomsInBungalows.RemoveRange(roomsInBungalow);
                }
                context.Bungalows.Remove(bungalow);
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Bungalow>> GetListAsync()
        {
            return await context.Bungalows.ToListAsync();
        }

        public async Task<Bungalow> ReadAsync(int id)
        {
            return await context.Bungalows.FindAsync(id);
        }

        public async Task<Bungalow> UpdateAsync(int id, Bungalow bungalow)
        {
            var entry = await ReadAsync(id);
            if (entry != null)
            {
                entry.Number = bungalow.Number;
                entry.Description = bungalow.Description;
                entry.IsActive = bungalow.IsActive;                
                entry.MapCoords = bungalow.MapCoords;                
                await context.SaveChangesAsync();
            }
            return entry;
        }
    }
}
