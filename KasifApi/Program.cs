using KasifApi.Data;
using KasifApi.Interfaces;
using KasifApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Veritabanı bağlantısı (PostgreSQL)
builder.Services.AddDbContext<KasifDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Servisleri bağımlılık olarak ekle
builder.Services.AddScoped<ICustomer, CustomerService>();
builder.Services.AddScoped<IEventRegistion, EventRegistionService>();
builder.Services.AddScoped<IFollowing, FollowingService>();
builder.Services.AddScoped<IMessage, MessageService>();
builder.Services.AddScoped<IPost, PostService>();
builder.Services.AddScoped<IPostSaved, PostSavedService>();
builder.Services.AddScoped<ISchool, SchoolService>();

// CORS yapılandırması
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// JSON döndürme seçeneklerini yapılandır (isteğe bağlı)
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // PascalCase için
        options.JsonSerializerOptions.WriteIndented = true;        // JSON formatı için okunabilir hale getirir
    });

// Swagger/OpenAPI desteği
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// HTTP istek boru hattını yapılandır

app.UseSwagger();
app.UseSwaggerUI();


app.MapGet("/test", () => "API is working!");

app.MapGet("/api/test", () => new { message = "Hello from KasifAPI!", timestamp = DateTime.UtcNow });

// Bu satır HTTPS yönlendirmesini devre dışı bırakır
app.UseHttpsRedirection();
// CORS kullanımı
app.UseCors("AllowAll");

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

// Uygulamayı çalıştır
app.Run();
