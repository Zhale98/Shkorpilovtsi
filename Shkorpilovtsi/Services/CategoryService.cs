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
    public class CategoryService : IDataService<Category>
    {
        private readonly ApplicationDbContext context;

        public CategoryService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entry = await ReadAsync(id);
            if (entry != null)
            {
                context.Categories.Remove(entry);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Category>> GetListAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<Category> ReadAsync(int id)
        {
            return await context.Categories.FindAsync(id);
        }

        public async Task<Category> UpdateAsync(int id, Category model)
        {
            var entry = await ReadAsync(id);
            if (entry != null)
            {
                entry.Name = model.Name;
            }
            await context.SaveChangesAsync();
            return entry;
        }
    }
}
