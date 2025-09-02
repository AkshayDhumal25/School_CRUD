using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_CRUD.Data;
using School_CRUD.Model;

namespace School_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudents()
        {
            var data = await _context.students.ToListAsync();
            if(data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Student>>> PostStudents(Student student)
        {
            _context.students.Add(student);
            await _context.SaveChangesAsync();
            return Ok(student);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentById(int id)
        {
            var data = _context.students.FindAsync(id);
            if(data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Student>>> DeleteStudent(int id)
        {
            var data = _context.students.FindAsync(id);
            _context.Remove(data);
            await _context.SaveChangesAsync();
            return Ok(data);
        }
    }
}
