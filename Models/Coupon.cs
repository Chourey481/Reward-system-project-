using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RewardSystemAPI.Models;

public class Coupon
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int MemberId { get; set; }

    [ForeignKey("MemberId")]
    public Member? Member { get; set; }

    // coupon rupee amount: 50 or 100
    public int Amount { get; set; }

    public string Code { get; set; } = "";

    public DateTime RedeemedAt { get; set; } = DateTime.UtcNow;
}
