using App.EnglishBuddy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.EnglishBuddy.Infrastructure.Context;

public class EnglishBuddyDbContext : DbContext
{
    public EnglishBuddyDbContext(DbContextOptions<EnglishBuddyDbContext> options) : base(options)
    {
    }

    public DbSet<Users> Users { get; set; }
    public DbSet<Calls> Calls { get; set; }
    public DbSet<Otp> Otp { get; set; }
    public DbSet<CallInfo> CallInfo { get; set; }
    public DbSet<Ratings> Ratings { get; set; }
    public DbSet<TotalRatings> TotalRatings { get; set; }
    public DbSet<Meetings> Meetings { get; set; }
    public DbSet<RandomUsers> RandomUsers { get; set; }
    public DbSet<MeetingIds> MeetingIds { get; set; }
    public DbSet<ContactUs> ContactUs { get; set; }
    public DbSet<UsersImages> UsersImages { get; set; }
}