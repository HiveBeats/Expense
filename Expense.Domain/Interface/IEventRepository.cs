using Expense.Domain.Model;

namespace Expense.Domain.Interface;

public interface IEventRepository
{
    Task<Event> Get(Guid id, bool notracking = false, CancellationToken ct = default);
    Task SaveChanges(CancellationToken ct = default);
    void Insert(Event @event);
}