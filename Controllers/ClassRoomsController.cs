using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_CRUD.Data;
using School_CRUD.Model;

namespace School_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassRoomsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClassRoomsController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassRoom>>> GetClassRooms()
        {
            return await _context.ClassRooms
                .Include(c => c.Teacher)
                .Include(c => c.Students)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClassRoom>> GetClassRoom(int id)
        {
            var classRoom = await _context.ClassRooms
                .Include(c => c.Teacher)
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (classRoom == null)
            {
                return NotFound();
            }

            return classRoom;
        }

        [HttpPost]
        public async Task<ActionResult<ClassRoom>> PostClassRoom(ClassRoom classRoom)
        {
            _context.ClassRooms.Add(classRoom);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClassRoom), new { id = classRoom.Id }, classRoom);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutClassRoom(int id, ClassRoom classRoom)
        {
            if (id != classRoom.Id)
            {
                return BadRequest();
            }

            _context.Entry(classRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassRoomExists(id))
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
        public async Task<IActionResult> DeleteClassRoom(int id)
        {
            var classRoom = await _context.ClassRooms.FindAsync(id);
            if (classRoom == null)
            {
                return NotFound();
            }

            _context.ClassRooms.Remove(classRoom);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClassRoomExists(int id)
        {
            return _context.ClassRooms.Any(e => e.Id == id);
        }
    }
}
