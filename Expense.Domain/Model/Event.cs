using System.Data;

namespace Expense.Domain.Model;

public class Event
{
    private List<Attendee> _attendees = new List<Attendee>();

    private Event()
    {
        
    }
    
    public Event(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
    
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    
    public IEnumerable<Attendee> Attendees
    {
        get { return _attendees;}
        private set { _attendees = value.ToList(); }
    }

    public void AddAttendee(string name)
    {
        _attendees.Add(new Attendee(this, name));
    }

    public void AddAttendeeExpense(Guid id, string name, decimal amount)
    {
        var attendee = Attendees.Single(a => a.Id == id);
        attendee.AddExpense(name, amount);
    }

    public void RemoveAttendeeExpense(Guid attendeeId, Guid expenseId)
    {
        var attendee = Attendees.Single(a => a.Id == attendeeId);
        attendee.RemoveExpense(expenseId);
    }

    public void JoinFamilyAttendees(Guid ownerId, IEnumerable<Guid> dependantIds)
    {
        var owner = Attendees.Single(x => x.Id == ownerId);
        var dependants = Attendees.Where(x => dependantIds.Contains(x.Id));

        if (owner.FamilyOwner is not null)
        {
            throw new ConstraintException("Family owner can't be inside another family");
        }

        if (dependants.Any(x => x.FamilyOwner is not null))
        {
            throw new ConstraintException(
                $"Those guys already inside other family: {string.Join(", ", dependants.Where(x => x.FamilyOwner != null).Select(x => x.Name))}");
        }
        
        owner.FamilyDependents = dependants;
        foreach (var dependant in dependants)
        {
            dependant.FamilyOwner = owner;
        }
    }

    public AttendeePayment GetAttendeeSummaryPayment(Guid fromId, Guid toId)
    {
        var from = Attendees.Single(x => x.Id == fromId);
        var to = Attendees.Single(x => x.Id == toId);

        var equalPaymentFrom = from.GetFamilyExpenses().Sum(x => x.Amount) / Attendees.Count();
        var equalPaymentTo = to.GetFamilyExpenses().Sum(x => x.Amount) / Attendees.Count();

        return new AttendeePayment(from, to, equalPaymentTo - equalPaymentFrom);
    }
}