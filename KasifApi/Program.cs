using KasifApi.Data;
using KasifApi.Interfaces;
using KasifApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

// CORS ayarları (isteğe bağlı, frontend ile iletişim için gerekli olabilir)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS middleware'i ekle
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Uygulamayı çalıştır
app.Run();