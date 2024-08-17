using App.EnglishBuddy.Application.Features.UserFeatures.AllContactUs;
using App.EnglishBuddy.Application.Features.UserFeatures.GetAllUser;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Common;
using App.EnglishBuddy.Domain.Entities;
using App.EnglishBuddy.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Sentry;
using System.Linq;
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

    public Task<List<T>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return Context.Set<T>().OrderBy(on => on.CreatedDate)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize).ToListAsync(cancellationToken);
    }

    public Task<List<Domain.Entities.Users>> GetAllUser(GetAllUserRequest request, CancellationToken cancellationToken)
    {
        return Context.Set<Users>()
        .Where(x => x.FirstName.Contains(request.FirstName) || x.LastName.Contains(request.LastName) || x.Mobile.Contains(request.Mobile)
        || x.Mobile.Contains(request.Mobile)
        || x.Email.Contains(request.Email)
        )
        .Skip((request.PageNumber - 1) * request.PageSize)
        .Take(request.PageSize).ToListAsync(cancellationToken);
    }

    public Task<List<Domain.Entities.ContactUs>> GetAllContact(AllContactUsRequest request, CancellationToken cancellationToken)
    {
        return Context.Set<ContactUs>()
        .Where(x => x.FirstName.Contains(request.FirstName) || x.LastName.Contains(request.LastName) || x.Mobile.Contains(request.Mobile)
        || x.Mobile.Contains(request.Mobile)
        || x.EmailAdress.Contains(request.Email)
        )
        .Skip((request.PageNumber - 1) * request.PageSize)
        .Take(request.PageSize).ToListAsync(cancellationToken);
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