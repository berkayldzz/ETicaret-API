
using ETicaretAPI.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices();

// Bu sadece browser tabanlý client uygulamalarýnda geçerlidir.
// Cors politika ayarýný yaparak client uygulamamýzdan gelen isteðin apimizi tüketmesine izin verdik.same origin policy'i hafiflettik.
//policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin() // bu her yerden gelmesini saðlar

// sadece kendi client uygulamamýzdan gelenleri kabul ettik ki  baþka yerlerden gelmesin.

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()
));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
