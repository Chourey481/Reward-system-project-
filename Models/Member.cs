using System.ComponentModel.DataAnnotations;

namespace RewardSystemAPI.Models;

public class Member
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(15)]
    public string MobileNumber { get; set; } = "";

    public string Name { get; set; } = "";

    // store dummy OTP like "1234"
    public string Otp { get; set; } = "1234";

    public bool IsVerified { get; set; } = false;

    // hashed password (simple SHA256 used for assignment)
    public string PasswordHash { get; set; } = "";

    public int TotalPoints { get; set; } = 0;
}
