using BlueSkyApp.Data;
using BlueSkyApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlueSkyApp.Controllers
{
    [Route("organization-members")]
    [ApiController]
    public class OrganizationMembersController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public OrganizationMembersController(ApplicationDbContext db) => _db = db;

      
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var members = await _db.OrganizationMembers
                .Select(m => new OrganizationMemberDTO
                {
                    OrganizationMemberId = m.OrganizationMemberId,
                    OrganizationId = m.OrganizationId,
                    UserId = m.UserId,
                    RoleId = m.RoleId,
                    CreatedAt = m.CreatedAt
                }).ToListAsync();

            return Ok(members);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var member = await _db.OrganizationMembers
                .Where(m => m.OrganizationMemberId == id)
                .Select(m => new OrganizationMemberDTO
                {
                    OrganizationMemberId = m.OrganizationMemberId,
                    OrganizationId = m.OrganizationId,
                    UserId = m.UserId,
                    RoleId = m.RoleId,
                    CreatedAt = m.CreatedAt
                }).FirstOrDefaultAsync();

            if (member == null) return NotFound();
            return Ok(member);
        }

        
        [HttpPost]
        public async Task<IActionResult> Post(CreateOrganizationMemberDTO dto)
        {
            var member = new OrganizationMember
            {
                OrganizationId = dto.OrganizationId,
                UserId = dto.UserId,
                RoleId = dto.RoleId,
                CreatedAt = DateTime.UtcNow
            };

            _db.OrganizationMembers.Add(member);
            await _db.SaveChangesAsync();

            var resultDto = new OrganizationMemberDTO
            {
                OrganizationMemberId = member.OrganizationMemberId,
                OrganizationId = member.OrganizationId,
                UserId = member.UserId,
                RoleId = member.RoleId,
                CreatedAt = member.CreatedAt
            };

            return CreatedAtAction(nameof(Get), new { id = member.OrganizationMemberId }, resultDto);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var member = await _db.OrganizationMembers.FindAsync(id);
            if (member == null) return NotFound();

            _db.OrganizationMembers.Remove(member);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
