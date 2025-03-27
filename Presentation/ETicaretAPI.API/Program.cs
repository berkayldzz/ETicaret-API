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

// Bu sadece browser tabanl� client uygulamalar�nda ge�erlidir.
// Cors politika ayar�n� yaparak client uygulamam�zdan gelen iste�in apimizi t�ketmesine izin verdik.same origin policy'i hafiflettik.
//policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin() // bu her yerden gelmesini sa�lar

// sadece kendi client uygulamam�zdan gelenleri kabul ettik ki  ba�ka yerlerden gelmesin.

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
             ValidateAudience = true, //Olusturulacak token degerini kimlerin/hangi originlerin/sitelerin kullanaca��n� belirledigimiz de�erdir. -> www.bilmemne.com
             ValidateIssuer = true, //Olusturulacak token degerini kimin da��tt���n� ifade edece�imiz aland�r. -> www.myapi.com
             ValidateLifetime = true, //Olusturulan token degerinin s�resini kontrol edecek olan dogrulamad�r.
             ValidateIssuerSigningKey = true, //�retilecek token degerinin uygulamam�za ait bir deger oldugunu ifade eden security key  verisinin dogrulanmas�d�r

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
