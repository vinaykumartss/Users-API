

using App.EventManagement.Application.Repositories;
using App.EventManagement.Infrastructure.Context;

namespace App.EventManagement.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly EventManagementDbContext _context;

    public UnitOfWork(EventManagementDbContext context)
    {
        _context = context;
    }
    public Task Save(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}