using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_CRUD.Data;
using School_CRUD.Model;

namespace School_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TeacherController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetAllTeachers()
        {
            var data = await _context.teachers.ToListAsync();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Teacher>>> PostTeachers(Teacher teacher)
        {
            _context.Add(teacher);
            await _context.SaveChangesAsync();
            return Ok(teacher);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeacherById(int id)
        {
            var data = _context.teachers.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Teacher>>> DeleteTeacher(int id)
        {
            var data = _context.teachers.FindAsync(id);
            _context.Remove(data);
            await _context.SaveChangesAsync();
            return Ok(data);
        }
    }
}
