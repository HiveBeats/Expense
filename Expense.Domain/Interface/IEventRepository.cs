using Expense.Domain.Model;

namespace Expense.Domain.Interface;

public interface IEventRepository
{
    Task<Event> GetEvent(Guid id);
    Task<bool> InsertEvent(Event @event);
}