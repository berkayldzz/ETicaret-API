using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        // GetByIdAsync metodunda parametreden gelen id ile entitynin id sini karşılaştırmak istiyoruz bunun için where T : class ifadesindeki class yerine BaseEntity verdik.O da bir class zaten sadece çemberi daraltmış olduk.Böylece entity içindeki id ye erişebildik.
        // Veya bu yöntem yerine direkt FindAsync metoduyla da id ye erişebiliriz.


        // Tracking mekanizması default olarak takip edilecek şekildedir.Eğer bu false gelirse sorgu takip edilmesin şeklinde ayarladık.
        private readonly ETicaretAPIDbContext _context;

        public ReadRepository(ETicaretAPIDbContext context)
        {
            _context = context;
        }

        // Orm'ler bunun gibi generic yapılanmalara uygun bir şekilde generic parametredeki türe (T) ait olabilecek DbContexti bize döndürecek metotlar(Set) barındırmaktadır.
        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool tracking = true)    // Table DbSet'tir.DbSet de IQueryable<TEntity> olduğundan dolayı direkt geriye döndürebiliyor.
             // =>Table;
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        // => Table.Where(method);
        {
            var query = Table.Where(method);
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        //=> await Table.FirstOrDefaultAsync(method);
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = Table.AsNoTracking();
            return await query.FirstOrDefaultAsync(method);
        }
        public async Task<T> GetByIdAsync(string id, bool tracking = true)
        //  => await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id)); // stringi guide parse ettik.
        //=> await Table.FindAsync(Guid.Parse(id));
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = Table.AsNoTracking();
            return await query.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));  // Eğer IQueryable ile çalışıyorsak FindAsync fonksiyonu yoktur.
        }


    }
}
