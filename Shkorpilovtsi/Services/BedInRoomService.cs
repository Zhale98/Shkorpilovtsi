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
    public class BedInRoomService : IDataService<BedsInRoom>
    {
        private readonly ApplicationDbContext context;

        public BedInRoomService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<BedsInRoom> CreateAsync(BedsInRoom bedInRoom)
        {
            var entry = await context.BedsInRooms.AddAsync(bedInRoom);
            await context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var bedInRoom = await ReadAsync(id);
            if (bedInRoom != null)
            {
                context.BedsInRooms.Remove(bedInRoom);
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<BedsInRoom>> GetListAsync()
        {
            return await context.BedsInRooms.Include((o) => o.Bed).Include((o) => o.Room).ToListAsync();
        }

        public async Task<BedsInRoom> ReadAsync(int id)
        {
            return await context.BedsInRooms.Include((o) => o.Bed).Include((o) => o.Room).FirstOrDefaultAsync((o) => o.Id == id);
        }

        public async Task<BedsInRoom> UpdateAsync(int id, BedsInRoom bedInRoom)
        {
            var entry = await ReadAsync(id);
            if (entry != null)
            {
                entry.BedId = bedInRoom.BedId;                
                await context.SaveChangesAsync();
            }
            return entry;
        }
    }
}
