using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Common;
using App.EnglishBuddy.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.EnglishBuddy.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly EnglishBuddyDbContext Context;

    public BaseRepository(EnglishBuddyDbContext context)
    {
        Context = context;
    }
    
    public void Create(T entity)
    {
        Context.Add(entity);
    }

    public void Update(T entity)
    {
        Context.Update(entity);
    }

    public void Delete(T entity)
    {

        Context.Remove(entity);
        
    }

    public Task<T> GetById(Guid id, CancellationToken cancellationToken)
    {
        return Context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<List<T>> GetAll(CancellationToken cancellationToken)
    {
        return Context.Set<T>().ToListAsync(cancellationToken);
    }
    public async Task<List<T>> FindByCondition(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
    {
        return await Context.Set<T>()
             .Where(expression).ToListAsync(cancellationToken);
    }


    public async Task<T> FindByUserId(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
    {
        return await Context.Set<T>()
             .Where(expression).FirstOrDefaultAsync(cancellationToken);
    }
}