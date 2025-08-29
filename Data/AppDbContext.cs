using Microsoft.EntityFrameworkCore;
using RewardSystemAPI.Models;

namespace RewardSystemAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }
    public DbSet<Member> Members { get; set; } = null!;
    public DbSet<Coupon> Coupons { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>()
            .HasIndex(m => m.MobileNumber)
            .IsUnique();
    }
}
