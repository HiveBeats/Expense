using Expense.Domain.Interface;
using Expense.Domain.Model;
using Expense.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Expense.Repository;

public class EventRepository: IEventRepository
{
    private readonly ExpenseDbContext _db;

    public EventRepository(ExpenseDbContext db)
    {
        _db = db;
    }
    
    public async Task<Event> Get(Guid id, bool notracking = false, CancellationToken ct = default)
    {
        var query = _db.Events.AsQueryable();
        if (notracking)
        {
            query = query.AsNoTracking();
        }

        return await query
            .Include(x => x.Attendees)
                .ThenInclude(x => x.FamilyDependents)
            .Include(x => x.Attendees)
                .ThenInclude(x => x.FamilyOwner)
            .Include(x => x.Attendees)
                .ThenInclude(x => x.Expenses)
            .SingleAsync(x => x.Id == id, ct);
    }

    public async Task SaveChanges(CancellationToken ct = default)
    {
        await _db.SaveChangesAsync(ct);
    }

    public void Insert(Event @event)
    {
        _db.Events.Add(@event);
    }
}