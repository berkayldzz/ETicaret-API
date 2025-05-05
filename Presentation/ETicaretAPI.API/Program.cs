using ETicaretAPI.API.Configurations.ColumnWriters;
using ETicaretAPI.API.Extensions;
using ETicaretAPI.Application;
using ETicaretAPI.Application.Validators.Products;
using ETicaretAPI.Infrastructure;
using ETicaretAPI.Infrastructure.Filters;
using ETicaretAPI.Infrastructure.Services.Storage.Local;
using ETicaretAPI.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using NpgsqlTypes;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();



builder.Services.AddStorage<LocalStorage>();

//builder.Services.AddStorage(StorageType.Local);

// Bu sadece browser tabanlı client uygulamalarında geçerlidir.
// Cors politika ayarını yaparak client uygulamamızdan gelen isteğin apimizi tüketmesine izin verdik.same origin policy'i hafiflettik.
//policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin() // bu her yerden gelmesini sağlar

// sadece kendi client uygulamamızdan gelenleri kabul ettik ki  başka yerlerden gelmesin.

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()
));

Logger log = new LoggerConfiguration()
     .WriteTo.Console()
     .WriteTo.File("logs/log.txt")
     .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL"), "logs",
         needAutoCreateTable: true,
         columnOptions: new Dictionary<string, ColumnWriterBase>
         {
             {"message", new RenderedMessageColumnWriter(NpgsqlDbType.Text)},
             {"message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text)},
             {"level", new LevelColumnWriter(true , NpgsqlDbType.Varchar)},
             {"time_stamp", new TimestampColumnWriter(NpgsqlDbType.Timestamp)},
             {"exception", new ExceptionColumnWriter(NpgsqlDbType.Text)},
             {"log_event", new LogEventSerializedColumnWriter(NpgsqlDbType.Json)},
             {"user_name", new UsernameColumnWriter()}
         })
     .WriteTo.Seq(builder.Configuration["Seq:ServerURL"])
     .Enrich.FromLogContext()
     .MinimumLevel.Information()
     .CreateLogger();

builder.Host.UseSerilog(log);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>()).ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
     .AddJwtBearer("Admin", options =>
     {
         options.TokenValidationParameters = new()
         {
             ValidateAudience = true, //Olusturulacak token degerini kimlerin/hangi originlerin/sitelerin kullanacağını belirledigimiz değerdir. -> www.bilmemne.com
             ValidateIssuer = true, //Olusturulacak token degerini kimin dağıttığını ifade edeceğimiz alandır. -> www.myapi.com
             ValidateLifetime = true, //Olusturulan token degerinin süresini kontrol edecek olan dogrulamadır.
             ValidateIssuerSigningKey = true, //Üretilecek token degerinin uygulamamıza ait bir deger oldugunu ifade eden security key  verisinin dogrulanmasıdır

             ValidAudience = builder.Configuration["Token:Audience"],
             ValidIssuer = builder.Configuration["Token:Issuer"],
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),

              NameClaimType = ClaimTypes.Name //JWT üzerinde Name claimne karþýlýk gelen deðeri User.Identity.Name propertysinden elde edebiliriz.
         };
     });

builder.Services.AddHttpClient();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());


app.UseStaticFiles();

app.UseSerilogRequestLogging();

app.UseHttpLogging();

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
 {
     var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
     LogContext.PushProperty("user_name", username);
     await next();
 });

app.MapControllers();

app.Run();
