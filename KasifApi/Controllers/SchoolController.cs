using KasifApi.Interfaces;
using KasifApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace KasifApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly ISchool _schoolService;

        public SchoolController(ISchool schoolService)
        {
            _schoolService = schoolService;
        }

        // Tüm okulları getirme
        [HttpGet("all")]
        public async Task<ActionResult<List<School>>> GetAllSchools()
        {
            var schools = await _schoolService.GetAllSchoolsAsync();
            return Ok(schools);
        }

        // Belirli bir okul bilgisi getirme
        [HttpGet("{id}")]
        public async Task<ActionResult<School>> GetSchoolById(int id)
        {
            var school = await _schoolService.GetSchoolByIdAsync(id);

            if (school == null)
            {
                return NotFound("Okul bulunamadı.");
            }

            return Ok(school);
        }
    }
}