
using App.EventManagement.Application.Repositories;
using App.EventManagement.Domain.Common;
using App.EventManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.EventManagement.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly EventManagementDbContext Context;

    public BaseRepository(EventManagementDbContext context)
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

    public Task<List<T>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return Context.Set<T>().OrderBy(on => on.CreatedDate)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize).ToListAsync(cancellationToken);
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

    public List<T> FindByListSync(Expression<Func<T, bool>> expression)
    {
        return  Context.Set<T>()
             .Where(expression).ToList();
    }

    public T FindByUserIdSync(Expression<Func<T, bool>> expression)
    {
        return  Context.Set<T>()
             .Where(expression).FirstOrDefault();
    }
}