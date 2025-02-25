using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entities
{
    public class Order : BaseEntity
    {
        // order ile product arasında çoka çok ilişki kuruyoruz.
        // bir siparişin bir tane müşterisi olabilir
        public string Description { get; set; }
        public string Address { get; set; }
        public ICollection<Product> Products { get; set; }
        public Guid CustomerId { get; set; } // order tablosunda customera karşlılık bir id de koy.Koymazsak kendi koyuyor zaten ama biz düzenledik.
        public Customer Customer { get; set; }
    }
}
