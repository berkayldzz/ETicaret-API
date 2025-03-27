using ETicaretAPI.Infrastructure;
using ETicaretAPI.Application.Validators.Products;
using ETicaretAPI.Infrastructure.Filters;
using ETicaretAPI.Persistence;
using FluentValidation.AspNetCore;
using ETicaretAPI.Infrastructure.Enums;
using ETicaretAPI.Infrastructure.Services.Storage.Local;
using ETicaretAPI.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();



builder.Services.AddStorage<LocalStorage>();

//builder.Services.AddStorage(StorageType.Local);

// Bu sadece browser tabanlý client uygulamalarýnda geçerlidir.
// Cors politika ayarýný yaparak client uygulamamýzdan gelen isteðin apimizi tüketmesine izin verdik.same origin policy'i hafiflettik.
//policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin() // bu her yerden gelmesini saðlar

// sadece kendi client uygulamamýzdan gelenleri kabul ettik ki  baþka yerlerden gelmesin.

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()
));

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>()).ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
     .AddJwtBearer("Admin", options =>
     {
         options.TokenValidationParameters = new()
         {
             ValidateAudience = true, //Olusturulacak token degerini kimlerin/hangi originlerin/sitelerin kullanacaðýný belirledigimiz deðerdir. -> www.bilmemne.com
             ValidateIssuer = true, //Olusturulacak token degerini kimin daðýttýðýný ifade edeceðimiz alandýr. -> www.myapi.com
             ValidateLifetime = true, //Olusturulan token degerinin süresini kontrol edecek olan dogrulamadýr.
             ValidateIssuerSigningKey = true, //Üretilecek token degerinin uygulamamýza ait bir deger oldugunu ifade eden security key  verisinin dogrulanmasýdýr

             ValidAudience = builder.Configuration["Token:Audience"],
             ValidIssuer = builder.Configuration["Token:Issuer"],
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))
         };
     });


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
