using Member.Data;
using Member.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MemberEntity = Member.Models.Member;

namespace Member.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController(MemberDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberEntity>>> GetMembers()
    {
        return Ok(await dbContext.Members.AsNoTracking().ToListAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MemberEntity>> GetMember(int id)
    {
        var member = await dbContext.Members.AsNoTracking().FirstOrDefaultAsync(item => item.MemberId == id);
        return member is null ? NotFound() : Ok(member);
    }

    [HttpPost]
    public async Task<ActionResult<MemberEntity>> CreateMember(MemberRequest request)
    {
        var member = new MemberEntity
        {
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            Email = request.Email.Trim(),
            PhoneNumber = request.PhoneNumber?.Trim(),
            Address = request.Address?.Trim(),
            City = request.City?.Trim(),
            State = request.State?.Trim(),
            Country = request.Country?.Trim(),
            MembershipType = request.MembershipType,
            MembershipStartDate = request.MembershipStartDate!.Value,
            MembershipEndDate = request.MembershipEndDate,
            IsActive = request.IsActive
        };
        member.CreatedDate = DateTime.UtcNow;

        dbContext.Members.Add(member);
        await dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMember), new { id = member.MemberId }, member);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateMember(int id, MemberRequest request)
    {
        var existingMember = await dbContext.Members.FindAsync(id);
        if (existingMember is null)
        {
            return NotFound();
        }

        existingMember.FirstName = request.FirstName.Trim();
        existingMember.LastName = request.LastName.Trim();
        existingMember.Email = request.Email.Trim();
        existingMember.PhoneNumber = request.PhoneNumber?.Trim();
        existingMember.Address = request.Address?.Trim();
        existingMember.City = request.City?.Trim();
        existingMember.State = request.State?.Trim();
        existingMember.Country = request.Country?.Trim();
        existingMember.MembershipType = request.MembershipType;
        existingMember.MembershipStartDate = request.MembershipStartDate!.Value;
        existingMember.MembershipEndDate = request.MembershipEndDate;
        existingMember.IsActive = request.IsActive;
        existingMember.UpdatedDate = DateTime.UtcNow;
        await dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteMember(int id)
    {
        var member = await dbContext.Members.FindAsync(id);
        if (member is null)
        {
            return NotFound();
        }

        dbContext.Members.Remove(member);
        await dbContext.SaveChangesAsync();
        return NoContent();
    }
}
