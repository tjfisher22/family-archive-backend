using FamilyArchiveBackend.Data;
using FamilyArchiveBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FamilyArchiveBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberSpousesController : ControllerBase
    {
        private readonly FamilyArchiveContext _context;

        public MemberSpousesController(FamilyArchiveContext context)
        {
            _context = context;
        }

        // GET: api/memberspouses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberSpouse>>> GetSpouses()
        {
            return await _context.MemberSpouses
                .Include(s => s.Spouse1)
                .Include(s => s.Spouse2)
                .ToListAsync();
        }

        // GET: api/memberspouses/spouse1Id/spouse2Id
        [HttpGet("{spouse1Id:int}/{spouse2Id:int}")]
        public async Task<ActionResult<MemberSpouse>> GetSpouse(int spouse1Id, int spouse2Id)
        {
            var spouse = await _context.MemberSpouses
                .Include(s => s.Spouse1)
                .Include(s => s.Spouse2)
                .FirstOrDefaultAsync(s => s.Spouse1Id == spouse1Id && s.Spouse2Id == spouse2Id);

            if (spouse == null)
                return NotFound();

            return spouse;
        }

        // POST: api/memberspouses
        [HttpPost]
        public async Task<ActionResult<MemberSpouse>> CreateSpouse(MemberSpouse spouse)
        {
            _context.MemberSpouses.Add(spouse);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSpouse), new { spouse1Id = spouse.Spouse1Id, spouse2Id = spouse.Spouse2Id }, spouse);
        }

        // DELETE: api/memberspouses/spouse1Id/spouse2Id
        [HttpDelete("{spouse1Id:int}/{spouse2Id:int}")]
        public async Task<IActionResult> DeleteSpouse(int spouse1Id, int spouse2Id)
        {
            var spouse = await _context.MemberSpouses.FindAsync(spouse1Id, spouse2Id);
            if (spouse == null)
                return NotFound();

            _context.MemberSpouses.Remove(spouse);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}