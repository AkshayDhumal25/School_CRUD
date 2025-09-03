using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_CRUD.Data;
using School_CRUD.Model;

namespace School_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AttendanceController(AppDbContext context)
        {
            _context = context;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAttendance()
        {
            return await _context.Attendance
                .Include(a => a.Student)
                .ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Attendance>> GetAttendance(int id)
        {
            var attendance = await _context.Attendance
                .Include(a => a.Student)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (attendance == null)
            {
                return NotFound();
            }

            return attendance;
        }

        
        [HttpGet("ByStudent/{studentId}")]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAttendanceByStudent(int studentId)
        {
            var attendance = await _context.Attendance
                .Where(a => a.StudentId == studentId)
                .Include(a => a.Student)
                .ToListAsync();

            if (!attendance.Any())
            {
                return NotFound("No attendance records found for this student.");
            }

            return attendance;
        }

        
        [HttpGet("ByDate/{date}")]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAttendanceByDate(DateTime date)
        {
            var attendance = await _context.Attendance
                .Where(a => a.Date.Date == date.Date)
                .Include(a => a.Student)
                .ToListAsync();

            return attendance;
        }


        [HttpPost]
        public async Task<ActionResult<Attendance>> PostAttendance(Attendance attendance)
        {
            _context.Attendance.Add(attendance);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAttendance), new { id = attendance.Id }, attendance);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendance(int id, Attendance attendance)
        {
            if (id != attendance.Id)
            {
                return BadRequest();
            }

            _context.Entry(attendance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            var attendance = await _context.Attendance.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }

            _context.Attendance.Remove(attendance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AttendanceExists(int id)
        {
            return _context.Attendance.Any(e => e.Id == id);
        }
    }
}
