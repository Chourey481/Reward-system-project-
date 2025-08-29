using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RewardSystemAPI.Data;
using RewardSystemAPI.DTOs;

namespace RewardSystemAPI.Controllers;

[ApiController]
[Route("api/points")]
public class PointsController : ControllerBase
{
    private readonly AppDbContext _db;
    public PointsController(AppDbContext db) { _db = db; }

    [Authorize]
    [HttpPost("add")]
    public async Task<IActionResult> AddPoints([FromBody] AddPointsDto dto)
    {
        var member = await _db.Members.FindAsync(dto.MemberId);
        if (member == null) return NotFound("Member not found");

        // Points rule: ₹100 => 10 points
        var pointsToAdd = (int)(Math.Floor(dto.PurchaseAmount / 100m) * 10);
        if (pointsToAdd <= 0) return BadRequest("Purchase amount too small for points");

        member.TotalPoints += pointsToAdd;
        await _db.SaveChangesAsync();
        return Ok(new { message = "Points added", pointsAdded = pointsToAdd, totalPoints = member.TotalPoints });
    }

    [Authorize]
    [HttpGet("{memberId}")]
    public async Task<IActionResult> GetPoints(int memberId)
    {
        var member = await _db.Members.FindAsync(memberId);
        if (member == null) return NotFound("Member not found");
        return Ok(new { memberId = member.Id, totalPoints = member.TotalPoints });
    }
}
