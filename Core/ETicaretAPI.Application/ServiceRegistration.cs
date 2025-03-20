using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaretAPI.Application
{
    // Application katmanındaki gerekli handler sınıflarını presentation katmanında program.cs üzerinden IOC containera eklememizi sağlayacak registration sınıfımızdır.
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection collection)
        {
            // tek tek AddScoped ile yapmış olmadık.

            // typeof(ServiceRegistration) : ServiceRegistration bu sınıfın bulunduğu assembly hangisiyse bütün IHandler,IRequest vs. sınıflarını al alayını bul ve ona göre mediatr yapılanmasında IOC'ye ekle.

            collection.AddMediatR(typeof(ServiceRegistration));
        }
    }
}
