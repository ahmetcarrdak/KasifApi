using KasifApi.DTO;
using KasifApi.Models;

namespace KasifApi.Interfaces
{
    public interface ISchool
    {
        Task<List<School>> GetAllSchoolsAsync();
        Task<IEnumerable<dynamic>> GetTitleAndLogoSchoolsAsync();
        Task<School?> GetSchoolByIdAsync(int id);
        Task<string> CreateSchoolAsync(School school);
    }
}