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
    public class RoomService : IDataService<Room>
    {
        private readonly ApplicationDbContext context;

        public RoomService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Room> CreateAsync(Room room)
        {
            var entry = await context.Rooms.AddAsync(room);
            await context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var room = await ReadAsync(id);
            if (room != null)
            {
                context.Rooms.Remove(room);
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Room>> GetListAsync()
        {
            return await context.Rooms.ToListAsync();
        }

        public async Task<Room> ReadAsync(int id)
        {
            return await context.Rooms.FindAsync(id);
        }

        public async Task<Room> UpdateAsync(int id, Room room)
        {
            var entry = await ReadAsync(id);
            if (entry != null)
            {
                entry.Name = room.Name;
                entry.Description = room.Description;
                entry.HasFridge = room.HasFridge;
                entry.IsWc = room.IsWc;
                entry.HasSofa = room.HasSofa;
                await context.SaveChangesAsync();
            }
            return entry;
        }
    }
}
