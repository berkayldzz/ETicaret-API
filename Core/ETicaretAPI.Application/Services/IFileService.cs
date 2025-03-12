//  Bütün servislerimle ilgili interfacelerimi service klasörü içinde oluşturuyorum.

using Microsoft.AspNetCore.Http;

namespace ETicaretAPI.Application.Services
{
    public interface IFileService
    {
        // IFormFileCollection files : Dosya yükleme sürecinde dosyanın kendisine ihtiyacımız olacak.
        Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files);

        // Dosyamız fiziksel olarak bildirilen dizine eklenmesini sağlayacak.
        Task<bool> CopyFileAsync(string path, IFormFile file);




    }
}
