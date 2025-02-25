using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Repositories
{
    public interface IWriteRepository<T>:IRepository<T> where T : BaseEntity
    {
        // Neden bool: Eklediysem/sildiysem/güncellediysem sonucunda true ya da false döneceğim. Kendimiz böyle istedik.
       
        Task<bool> AddAsync(T model);  // 1 tane ekleme için
        Task<bool> AddRangeAsync(List<T> datas); // Birden fazla ekleme
        bool Remove(T model);
        bool RemoveRange(List<T> datas);
        Task<bool> RemoveAsync(string id); // id sini verdiğim veriyi de silsin.
        bool Update(T model);
        Task<int> SaveAsync();
    }
}
