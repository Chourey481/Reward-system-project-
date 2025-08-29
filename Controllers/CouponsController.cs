using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RewardSystemAPI.Data;
using RewardSystemAPI.DTOs;
using RewardSystemAPI.Models;

namespace RewardSystemAPI.Controllers;

[ApiController]
[Route("api/coupons")]
public class CouponsController : ControllerBase
{
    private readonly AppDbContext _db;
    public CouponsController(AppDbContext db) { _db = db; }

    [Authorize]
    [HttpPost("redeem")]
    public async Task<IActionResult> Redeem([FromBody] RedeemDto dto)
    {
        var member = await _db.Members.FindAsync(dto.MemberId);
        if (member == null) return NotFound("Member not found");

        if (dto.PointsToRedeem != 500 && dto.PointsToRedeem != 1000)
            return BadRequest("Can only redeem 500 or 1000 points");

        if (member.TotalPoints < dto.PointsToRedeem) return BadRequest("Insufficient points");

        var amount = dto.PointsToRedeem == 500 ? 50 : 100;
        member.TotalPoints -= dto.PointsToRedeem;

        var coupon = new Coupon
        {
            MemberId = dto.MemberId,
            Amount = amount,
            Code = $"CPN-{Guid.NewGuid().ToString().Substring(0,8).ToUpper()}",
            RedeemedAt = DateTime.UtcNow
        };
        _db.Coupons.Add(coupon);
        await _db.SaveChangesAsync();
        return Ok(new { message = "Coupon redeemed", couponId = coupon.Id, amount = coupon.Amount, code = coupon.Code, remainingPoints = member.TotalPoints });
    }
}
