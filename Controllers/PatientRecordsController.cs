using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientManagement.Data;
using PatientManagement.Models;

namespace PatientManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientRecordsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PatientRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("CreateRecord")]
        public async Task<IActionResult> CreateRecord([FromBody] PatientRecord record)
        {
            if (record == null) 
            {
                return BadRequest("Invalid data");
            }
            _context.PatientRecords.Add(record);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRecord), new { id = record.Id }, record);
        }

        [HttpGet("GetRecords")]
        public async Task<ActionResult<IEnumerable<PatientRecord>>> GetAllRecords()
        {
            return await _context.PatientRecords.ToListAsync();
        }


        [HttpGet("GetRecord/{PatientId}")]
        public async Task<IActionResult> GetRecord(int PatientId)
        {
            var record = await _context.PatientRecords.FindAsync(PatientId);
            if (record == null) return NotFound();
            return Ok(record);
        }

        [HttpPut("UpdateRecord/{PatientId}")]
        public async Task<IActionResult> UpdateRecord(int PatientId, [FromBody] PatientRecord updatedRecord)
        {
            var record = await _context.PatientRecords.FindAsync(PatientId);
            if (record == null) return NotFound();

            record.Description = updatedRecord.Description;
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
