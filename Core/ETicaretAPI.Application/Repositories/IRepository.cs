using ETicaretAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Repositories
{
    // Bu interface temel base şeyleri yani bütün repositorylerde olmasını istediğim şeyleri tutacak.
    // Örneğin DbSet propertyleri
    public interface IRepository<T> where T : BaseEntity
    {
        // DbSet tablolara karşılık geliyor.
        // Biz table alırız ama herhangi bir set işlemi yapmayız.

        DbSet<T> Table { get; }
    }
}
