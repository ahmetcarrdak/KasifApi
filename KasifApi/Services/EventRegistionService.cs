using KasifApi.DTO;
using KasifApi.Interfaces;
using KasifApi.Models;
using KasifApi.Data;
using Microsoft.EntityFrameworkCore;

namespace KasifApi.Services;

public class EventRegistionService : IEventRegistion
{
    private readonly KasifDbContext _context;

    public EventRegistionService(KasifDbContext context)
    {
        _context = context;
    }

    // Yeni kayıt oluştur
    public async Task<EventRegistionDto> CreateAsync(EventRegistionCreateDto eventRegistionCreateDto)
    {
        var eventRegistion = new EventRegistion
        {
            PostId = eventRegistionCreateDto.PostId,
            CustomerId = eventRegistionCreateDto.CustomerId,
            Created = DateTime.UtcNow
        };

        await _context.EventRegistions.AddAsync(eventRegistion);
        await _context.SaveChangesAsync();

        return new EventRegistionDto
        {
            Id = eventRegistion.EventRegistionId,
            PostId = eventRegistion.PostId,
            CustomerId = eventRegistion.CustomerId,
            Created = eventRegistion.Created
        };
    }

    // Tüm kayıtları listele
    public async Task<List<EventRegistionDto>> GetAllAsync()
    {
        return await _context.EventRegistions
            .Select(e => new EventRegistionDto
            {
                Id = e.EventRegistionId,
                PostId = e.PostId,
                CustomerId = e.CustomerId,
                Created = e.Created
            })
            .ToListAsync();
    }

    // ID ile kayıt getir
    public async Task<EventRegistionDto?> GetByIdAsync(int id)
    {
        var eventRegistion = await _context.EventRegistions.FirstOrDefaultAsync(e => e.EventRegistionId == id);

        if (eventRegistion == null) return null;

        return new EventRegistionDto
        {
            Id = eventRegistion.EventRegistionId,
            PostId = eventRegistion.PostId,
            CustomerId = eventRegistion.CustomerId,
            Created = eventRegistion.Created
        };
    }

    // ID ile kayıt sil
    public async Task<bool> DeleteAsync(int id)
    {
        var eventRegistion = await _context.EventRegistions.FirstOrDefaultAsync(e => e.EventRegistionId == id);

        if (eventRegistion == null) return false;

        _context.EventRegistions.Remove(eventRegistion);
        await _context.SaveChangesAsync();

        return true;
    }
}
