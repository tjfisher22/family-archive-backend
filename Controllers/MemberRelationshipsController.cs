using FamilyArchiveBackend.Data;
using FamilyArchiveBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FamilyArchiveBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberRelationshipsController : ControllerBase
    {
        private readonly FamilyArchiveContext _context;

        public MemberRelationshipsController(FamilyArchiveContext context)
        {
            _context = context;
        }

        // GET: api/memberrelationships
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberRelationship>>> GetRelationships()
        {
            return await _context.MemberRelationships
                .Include(r => r.Parent)
                .Include(r => r.Child)
                .ToListAsync();
        }

        // GET: api/memberrelationships/parentId/childId
        [HttpGet("{parentId:int}/{childId:int}")]
        public async Task<ActionResult<MemberRelationship>> GetRelationship(int parentId, int childId)
        {
            var relationship = await _context.MemberRelationships
                .Include(r => r.Parent)
                .Include(r => r.Child)
                .FirstOrDefaultAsync(r => r.ParentId == parentId && r.ChildId == childId);

            if (relationship == null)
                return NotFound();

            return relationship;
        }

        // POST: api/memberrelationships
        [HttpPost]
        public async Task<ActionResult<MemberRelationship>> CreateRelationship(MemberRelationship relationship)
        {
            _context.MemberRelationships.Add(relationship);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRelationship), new { parentId = relationship.ParentId, childId = relationship.ChildId }, relationship);
        }

        // DELETE: api/memberrelationships/parentId/childId
        [HttpDelete("{parentId:int}/{childId:int}")]
        public async Task<IActionResult> DeleteRelationship(int parentId, int childId)
        {
            var relationship = await _context.MemberRelationships.FindAsync(parentId, childId);
            if (relationship == null)
                return NotFound();

            _context.MemberRelationships.Remove(relationship);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}