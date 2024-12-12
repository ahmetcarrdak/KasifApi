using KasifApi.Services;
using KasifApi.DTO;
using KasifApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace KasifApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly CustomerService _customerService;

    public CustomerController(CustomerService customerService)
    {
        _customerService = customerService;
    }

    // Kullanıcı Giriş
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] CustomerLoginDto customerLoginDto)
    {
        var customer = await _customerService.LoginAsync(customerLoginDto);

        if (customer == null)
        {
            return Unauthorized("Giriş bilgileri yanlış veya kullanıcı bulunamadı.");
        }

        return Ok(customer);
    }

    // Kullanıcı Kayıt
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CustomerRegisterDto customerRegisterDto)
    {
        var customer = await _customerService.RegisterAsync(customerRegisterDto);

        if (customer == null)
        {
            return Conflict("Bu kullanıcı adı zaten kullanılıyor.");
        }

        return CreatedAtAction(nameof(GetById), new { id = customer.CustomerId }, customer);
    }

    // ID ile Kullanıcı Getirme
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customer = await _customerService.GetByIdAsync(id);

        if (customer == null)
        {
            return NotFound("Kullanıcı bulunamadı.");
        }

        return Ok(customer);
    }

    // Kullanıcı Adı Kontrolü
    [HttpGet("check-username")]
    public async Task<IActionResult> CheckUsername([FromQuery] string username)
    {
        // Gelen kullanıcı adını kontrol et
        var exists = await _customerService.IsUsernameExistsAsync(username);

        if (exists)
        {
            return Ok("Bu kullanıcı adı zaten mevcut.");
        }

        return Ok("Bu kullanıcı adı kullanılabilir.");
    }
    
    // Kullanıcı Güncelleme
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] CustomerUpdateDto customerUpdateDto)
    {
        var result = await _customerService.UpdateAsync(customerUpdateDto);

        if (result == "Kullanıcı bulunamadı.")
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    // Kullanıcı Silme Durumunu Değiştirme (Toggle)
    [HttpPatch("toggle-delete/{id}")]
    public async Task<IActionResult> ToggleDelete(int id)
    {
        var result = await _customerService.ToggleDeleteAsync(id);

        if (result.Contains("bulunamadı"))
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    // Kullanıcı Aktiflik Durumunu Değiştirme (Toggle)
    [HttpPatch("toggle-activate/{id}")]
    public async Task<IActionResult> ToggleActivate(int id)
    {
        var result = await _customerService.ToggleActivateAsync(id);

        if (result.Contains("bulunamadı"))
        {
            return NotFound(result);
        }

        return Ok(result);
    }
}