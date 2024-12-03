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
            var schools = await _context.Schools.ToListAsync();
            return schools.Select(s => new School
            {
                Id = s.Id,
                Type = s.Type,
                Title = s.Title
            }).ToList();
        }

        // Belirli bir okul bilgisi getirme
        public async Task<School?> GetSchoolByIdAsync(int id)
        {
            var school = await _context.Schools.FirstOrDefaultAsync(s => s.Id == id);

            if (school == null)
            {
                return null;
            }

            return new School
            {
                Id = school.Id,
                Type = school.Type,
                Title = school.Title
            };
        }
    }
}