using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence
{
    static class Configuration
    {

        // ConfigurationManager, .NET uygulamalarında uygulama yapılandırmalarını okumak için kullanılan bir sınıftır. Özellikle appsettings.json, web.config, app.config gibi dosyalardaki bağlantı dizesi (connection string), API anahtarları veya diğer yapılandırma verilerini almak için kullanılır.

        // ConfigurationManager nesnesi üzerinden biz başka bir katmandaki json dosyasına erişmeye çalışıyoruz.
        // Bunu da AddJsonFile adlı fonksiyon yardımıyla yapıyoruz. Başka bir kütüphane ekledik bunun için.
        // Neredeki appsettings.json olduğunu da SetBasePath fonksiyonu yardımıyla bildiriyoruz.

        // Bu kodları 2 farklı yerde kullandığımız için tek başına burada topladık ve ServiceRegistration ve DesignTimeDbContextFactory içinde kullandık.
        static public string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/ETicaretAPI.API"));
                configurationManager.AddJsonFile("appsettings.json");

                return configurationManager.GetConnectionString("PostgreSQL");
            }
        }
    }
}
