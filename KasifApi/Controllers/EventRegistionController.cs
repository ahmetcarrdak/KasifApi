using KasifApi.DTO;
using KasifApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KasifApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventRegistionController : ControllerBase
{
    private readonly IEventRegistion _eventRegistionService;

    public EventRegistionController(IEventRegistion eventRegistionService)
    {
        _eventRegistionService = eventRegistionService;
    }

    // Yeni kayıt oluştur
    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] EventRegistionCreateDto eventRegistionCreateDto)
    {
        var result = await _eventRegistionService.CreateAsync(eventRegistionCreateDto);
        return Ok(result);
    }

    // Tüm kayıtları listele
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _eventRegistionService.GetAllAsync();
        return Ok(result);
    }

    // ID ile kayıt getir
    [HttpGet("ById/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _eventRegistionService.GetByIdAsync(id);

        if (result == null)
        {
            return NotFound("Kayıt bulunamadı.");
        }

        return Ok(result);
    }

    // ID ile kayıt sil
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _eventRegistionService.DeleteAsync(id);

        if (!result)
        {
            return NotFound("Kayıt bulunamadı.");
        }

        return Ok("Kayıt başarıyla silindi.");
    }
}