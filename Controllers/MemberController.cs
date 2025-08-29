using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RewardSystemAPI.Data;
using RewardSystemAPI.DTOs;
using RewardSystemAPI.Models;
using RewardSystemAPI.Services;
using RewardSystemAPI.Utils;

namespace RewardSystemAPI.Controllers;

[ApiController]
[Route("api/member")]
public class MemberController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly JwtService _jwt;
    public MemberController(AppDbContext db, JwtService jwt) { _db = db; _jwt = jwt; }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.MobileNumber) || string.IsNullOrWhiteSpace(dto.Password))
            return BadRequest("mobileNumber and password required");

        if (await _db.Members.AnyAsync(m => m.MobileNumber == dto.MobileNumber))
            return Conflict("Mobile already registered");

        var m = new Member
        {
            MobileNumber = dto.MobileNumber,
            Name = dto.Name ?? "",
            PasswordHash = PasswordHasher.Hash(dto.Password),
            Otp = "1234",
            IsVerified = false,
            TotalPoints = 0
        };
        _db.Members.Add(m);
        await _db.SaveChangesAsync();
        return Ok(new { message = "Registered. Dummy OTP saved (1234)." });
    }

    [HttpPost("verify")]
    public async Task<IActionResult> Verify([FromBody] VerifyDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.MobileNumber) || string.IsNullOrWhiteSpace(dto.Otp))
            return BadRequest("mobileNumber and otp required");

        var member = await _db.Members.FirstOrDefaultAsync(m => m.MobileNumber == dto.MobileNumber);
        if (member == null) return NotFound("Member not found");
        if (member.IsVerified) return BadRequest("Already verified");
        if (dto.Otp != member.Otp) return BadRequest("Invalid OTP");

        member.IsVerified = true;
        await _db.SaveChangesAsync();
        return Ok(new { message = "Verified successfully." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.MobileNumber) || string.IsNullOrWhiteSpace(dto.Password))
            return BadRequest("mobileNumber and password required");

        var member = await _db.Members.FirstOrDefaultAsync(m => m.MobileNumber == dto.MobileNumber);
        if (member == null) return Unauthorized("Invalid credentials");
        if (!member.IsVerified) return Unauthorized("Member not verified");
        if (!PasswordHasher.Verify(dto.Password, member.PasswordHash)) return Unauthorized("Invalid credentials");

        var token = _jwt.GenerateToken(member.Id, member.MobileNumber);
        return Ok(new { token, memberId = member.Id, name = member.Name });
    }
}
