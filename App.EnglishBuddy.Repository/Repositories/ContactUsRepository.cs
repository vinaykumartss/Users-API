using App.EnglishBuddy.Application.Features.UserFeatures.CallList;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using App.EnglishBuddy.Infrastructure.Context;

namespace App.EnglishBuddy.Infrastructure.Repositories;

public class ContactUsRepository : BaseRepository<ContactUs>, IContactUsRepository
{
    public EnglishBuddyDbContext _context;
    public ContactUsRepository(EnglishBuddyDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<List<CallsDetailsResponse>> CallDetails(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}