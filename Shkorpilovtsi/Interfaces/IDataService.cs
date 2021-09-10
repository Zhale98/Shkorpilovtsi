using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shkorpilovtsi.Interfaces
{
    public interface IDataService<TModel>
    {
        Task<List<TModel>> GetListAsync();
        Task<TModel> CreateAsync(TModel model);
        Task<TModel> ReadAsync(int id);
        Task<TModel> UpdateAsync(int id, TModel model);
        Task<bool> DeleteAsync(int id);        
    }
}
