using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ETicaretAPI.Infrastructure.Filters
{
    public class ValidationFilter : IAsyncActionFilter 
    {
        // IAsyncActionFilter , actiona gelen isteklerde devreye giren bir filterdır.
        // IAsyncActionFilter, ASP.NET Core'da yer alan bir Action Filter (Aksiyon Filtresi) arayüzüdür ve bir controller metodu çalışmadan önce veya sonra devreye girerek işlem yapmaya olanak tanır.
        // Action Filter:Aksiyon başlamadan önce ve sonra çalışır.Loglama, validasyon, veri manipülasyonu gibi işlemler için kullanılır.
        // OnActionExecuting → Aksiyon çalışmadan önce çağrılır.
        // OnActionExecuted → Aksiyon tamamlandıktan sonra çalışır.
        // OnActionExecutionAsync metodu hem aksiyon (action) çağrılmadan önce hem de çağrıldıktan sonra tetiklenebilir.

        // Her gelen istekte ister girilen veriler doğru ister yanlış olsun bu filter tetikleniyor.

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(x => x.Value.Errors.Any())
                    .ToDictionary(e => e.Key, e => e.Value.Errors.Select(e => e.ErrorMessage)).ToArray();

                context.Result = new BadRequestObjectResult(errors);
                return;
            }
            await next();
        }
    }
}
