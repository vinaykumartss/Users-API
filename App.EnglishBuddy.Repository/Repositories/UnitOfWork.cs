using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Infrastructure.Context;

namespace App.EnglishBuddy.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly EnglishBuddyDbContext _context;

    public UnitOfWork(EnglishBuddyDbContext context)
    {
        _context = context;
    }
    public Task Save(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}