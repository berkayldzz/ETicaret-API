using ETicaretAPI.Application.ViewModels.Products;
using FluentValidation;

namespace ETicaretAPI.Application.Validators.Products
{
    // Validasyon , ViewModele ya da CQRS patternda request nesnelerine karşı bire bir oluşturulacak nesnelerdir.Her biri için ayrı ayrı oluşturmak en doğrusudur.Create ayrı update ayrı.
    //Bu validatorları uygulamaya bildirmem gerek. ---> Program.cs de bildirdik.
    public class CreateProductValidator:AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
               .NotEmpty()
               .NotNull()
                   .WithMessage("Lütfen ürün adını boş geçmeyiniz.")
               .MaximumLength(150)
               .MinimumLength(5)
                   .WithMessage("Lütfen ürün adını 5 ile 150 karakter arasında giriniz.");

            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen stok bilgisini boş geçmeyiniz.")
                .Must(s => s >= 0)
                    .WithMessage("Stok bilgisi negatif olamaz!");

            RuleFor(p => p.Price)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen fiyat bilgisini boş geçmeyiniz.")
                .Must(s => s >= 0)
                    .WithMessage("Fiyat bilgisi negatif olamaz!");
        }
    }
}
