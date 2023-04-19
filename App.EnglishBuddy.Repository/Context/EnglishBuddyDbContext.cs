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
}