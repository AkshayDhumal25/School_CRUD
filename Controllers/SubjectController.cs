using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_CRUD.Data;
using School_CRUD.Model;

namespace School_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SubjectController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjects()
        {
            return await _context.Subjects
                                 .Include(s => s.Teacher)
                                 .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Subject>> GetSubject(int id)
        {
            var subject = await _context.Subjects
                                        .Include(s => s.Teacher)
                                        .FirstOrDefaultAsync(s => s.Id == id);

            if (subject == null)
                return NotFound();

            return subject;
        }

        [HttpPost]
        public async Task<ActionResult<Subject>> CreateSubject(Subject subject)
        {
            var teacherExists = await _context.teachers.AnyAsync(t => t.Id == subject.TeacherId);
            if (!teacherExists)
                return BadRequest("Teacher does not exist.");

            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSubject), new { id = subject.Id }, subject);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(int id, Subject subject)
        {
            if (id != subject.Id)
                return BadRequest("Id mismatch");

            var existingSubject = await _context.Subjects.FindAsync(id);
            if (existingSubject == null)
                return NotFound();

            existingSubject.Name = subject.Name;
            existingSubject.TeacherId = subject.TeacherId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
                return NotFound();

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
