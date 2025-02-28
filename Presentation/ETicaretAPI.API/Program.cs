
using ETicaretAPI.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices();

// Bu sadece browser tabanl� client uygulamalar�nda ge�erlidir.
// Cors politika ayar�n� yaparak client uygulamam�zdan gelen iste�in apimizi t�ketmesine izin verdik.same origin policy'i hafiflettik.
//policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin() // bu her yerden gelmesini sa�lar

// sadece kendi client uygulamam�zdan gelenleri kabul ettik ki  ba�ka yerlerden gelmesin.

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
