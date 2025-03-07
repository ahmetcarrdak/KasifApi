using KasifApi.Interfaces;
using KasifApi.DTO;
using KasifApi.Data;
using KasifApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KasifApi.Services
{
    public class SchoolService : ISchool
    {
        private readonly KasifDbContext _context;

        public SchoolService(KasifDbContext context)
        {
            _context = context;
        }

        // Tüm okulları getirme
        public async Task<List<School>> GetAllSchoolsAsync()
        {
            return await _context.Schools.ToListAsync();
        }

        // Logo ve İsimleri ile okulları getirme
        public async Task<IEnumerable<dynamic>> GetTitleAndLogoSchoolsAsync()
        {
            return await _context.Schools
            .OrderBy(s => s.SchoolId) // SchoolId'ye göre sıralama
            .Select(s => new 
            {
                s.SchoolId,
                s.Title,
                s.Logo
            })
            .ToListAsync();
        }

        
        // Belirli bir okul bilgisi getirme
        public async Task<School?> GetSchoolByIdAsync(int id)
        {
            var school = await _context.Schools.FirstOrDefaultAsync(s => s.SchoolId == id);

            if (school == null)
            {
                return null;
            }

            return school;
        }

        public async Task<string> CreateSchoolAsync(School school)
        {
            // Okul nesnesini veritabanına ekliyoruz
            _context.Schools.Add(school);
            await _context.SaveChangesAsync();

            return "success";  // Okul nesnesini döndürüyoruz
        }

    }
}
