using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Repositories
{
    // IReadRepository'nin bir T parametresi olsun onu IRepository'nin T parametresine göndersin.


    // Func<T, bool> → T tipinde bir nesne alır ve bool (true/false) döndürür. Yani, bir filtreleme fonksiyonu oluşturur.
    // Expression<> → LINQ ifadelerini SQL’e çevirebilmek için kullanılır.
    // Özetle, bu parametre veritabanında WHERE şartı olarak kullanılacak bir fonksiyon bekler.

    public interface IReadRepository<T>:IRepository<T> where T : BaseEntity
    {
        // IQueryable olan sorgularda yazmış olduğumuz where şartları ya da select sorguları ilgili db sorgusuna eklenecektir.
        // List IEnumarable'dır.InMemory'e çeker bütün verileri ve onun üzerinde işlem yapmanı sağlar.

        IQueryable<T> GetAll(bool tracking = true);
        IQueryable<T> GetWhere(Expression<Func<T,bool>> method, bool tracking = true); // şarta uygun olan birden fazla veri elde edilsin.
        
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);    // şarta uygun olan bir tane tekil bir veri elde edilsin

        Task<T> GetByIdAsync(string id, bool tracking = true);                      // id ye uygun olan hangisiyse onu getirecek.


        // Bu son 2 metodumuz ormnin arka planda senkron değil asenkron fonksiyonlarını kullanacağından asyn ve Task ile onları işaretledik.
        // Örneğin GetSingle FirsOrDefault'un asenkron metodunu kulllanacak.
    }
}
