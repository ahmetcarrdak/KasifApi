using KasifApi.DTO;

namespace KasifApi.Interfaces;

public interface IEventRegistion
{
    // Yeni kayıt oluştur
    Task<EventRegistionDto> CreateAsync(EventRegistionCreateDto eventRegistionCreateDto);

    // Kayıtları listele
    Task<List<EventRegistionDto>> GetAllAsync();

    // ID ile kayıt getir
    Task<EventRegistionDto?> GetByIdAsync(int id);

    // ID ile kayıt sil
    Task<bool> DeleteAsync(int id);
}