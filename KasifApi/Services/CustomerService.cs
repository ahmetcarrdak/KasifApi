using KasifApi.Interfaces;
using KasifApi.DTO;
using KasifApi.Models;
using KasifApi.Data;
using Microsoft.EntityFrameworkCore;

namespace KasifApi.Services;

public class CustomerService : ICustomer
{
    private readonly KasifDbContext _context;

    public CustomerService(KasifDbContext context)
    {
        _context = context;
    }

    // Şifre Hashleme Metodu
    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    // Şifre Doğrulama Metodu (isteğe bağlı)
    private bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }

    public async Task<Customer?> LoginAsync(CustomerLoginDto customerLoginDto)
    {
        // Gelen info değerine göre kullanıcıyı bulma işlemi
        var customer = await _context.Customers
            .FirstOrDefaultAsync(c =>
                c.Email == customerLoginDto.info ||
                c.PhoneNumber == customerLoginDto.info ||
                c.Username == customerLoginDto.info);

        // Eğer kullanıcı bulunamazsa, null döndür
        if (customer == null)
        {
            return null;
        }

        // Şifre doğrulama
        if (!VerifyPassword(customerLoginDto.Password, customer.Password))
        {
            // Şifre yanlışsa null döndür
            return null;
        }

        // Kullanıcı ve şifre doğruysa kullanıcıyı döndür
        return customer;
    }


    public async Task<Customer?> RegisterAsync(CustomerRegisterDto customerRegisterDto)
    {
        // Kullanıcı adının var olup olmadığını kontrol et
        var existingCustomer = await _context.Customers
            .FirstOrDefaultAsync(c => c.Username == customerRegisterDto.Username);

        if (existingCustomer != null)
        {
            // Eğer kullanıcı adı zaten varsa, null döndür veya hata mesajı fırlat
            return null;
        }

        // Yeni müşteri oluştur
        var newCustomer = new Customer
        {
            Name = customerRegisterDto.Name,
            Email = customerRegisterDto.Email,
            PhoneNumber = customerRegisterDto.PhoneNumber,
            Username = customerRegisterDto.Username,
            Password = HashPassword(customerRegisterDto.Password), // Şifreyi hashlemenizi öneririm
            SchoolId = customerRegisterDto.SchoolId,
            IsActive = true,
            IsDeleted = false
        };

        // Yeni müşteriyi veritabanına ekle
        await _context.Customers.AddAsync(newCustomer);

        // Değişiklikleri kaydet
        await _context.SaveChangesAsync();

        // Kayıt edilen kullanıcıyı döndür
        return newCustomer;
    }

    // Müşteriyi ID'ye göre getirme
    public async Task<Customer?> GetByIdAsync(int customerId)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId);
    }

    // Kullancı adına göre kontrol
    public async Task<bool> IsUsernameExistsAsync(string username)
    {
        // Kullanıcı adı kontrolü
        return await _context.Customers.AnyAsync(c => c.Username == username);
    }


    // Müşteri güncelleme
    public async Task<string> UpdateAsync(CustomerUpdateDto customerUpdateDto)
    {
        // Güncellenecek müşteri var mı kontrol et
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerUpdateDto.ImageId);

        if (customer == null)
        {
            return "Kullanıcı bulunamadı.";
        }

        // Yeni bilgileri güncelle
        customer.Name = customerUpdateDto.Name;
        customer.Email = customerUpdateDto.Email;
        customer.PhoneNumber = customerUpdateDto.PhoneNumber;
        customer.Username = customerUpdateDto.Username;
        customer.SchoolId = customerUpdateDto.SchoolId;
        customer.Bio = customerUpdateDto.Bio;

        // Eğer şifre gönderilmişse hashle ve güncelle
        if (!string.IsNullOrEmpty(customerUpdateDto.Password))
        {
            customer.Password = HashPassword(customerUpdateDto.Password);
        }

        // Değişiklikleri kaydet
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();

        return "Kullanıcı başarıyla güncellendi.";
    }

    // IsDeleted özelliğini toggle eden metot
    public async Task<string> ToggleDeleteAsync(int customerId)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId);

        if (customer == null)
        {
            return "Kullanıcı bulunamadı.";
        }

        // IsDeleted değerini tersine çevir
        customer.IsDeleted = !customer.IsDeleted;

        // Değişiklikleri kaydet
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();

        var status = customer.IsDeleted ? "silindi" : "silinmesi kaldırıldı";
        return $"Kullanıcı {status}.";
    }

    // IsActive özelliğini toggle eden metot
    public async Task<string> ToggleActivateAsync(int customerId)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId);

        if (customer == null)
        {
            return "Kullanıcı bulunamadı.";
        }

        // IsActive değerini tersine çevir
        customer.IsActive = !customer.IsActive;

        // Değişiklikleri kaydet
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();

        var status = customer.IsActive ? "aktifleştirildi" : "pasifleştirildi";
        return $"Kullanıcı {status}.";
    }
}