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
    public string Name { get; private set; }
    
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

    public AttendeePayment GetAttendeeSummaryPayment(Guid fromId, Guid toId)
    {
        //todo: функционал сджоинить двух attendee в одного при вычислениях (наприм. парочка рассчитывается из одного кошелька)
        
        var from = Attendees.Single(x => x.Id == fromId);
        var to = Attendees.Single(x => x.Id == toId);

        var equalPaymentFrom = from.Expenses.Sum(x => x.Amount) / Attendees.Count();
        var equalPaymentTo = to.Expenses.Sum(x => x.Amount) / Attendees.Count();

        return new AttendeePayment(from, to, equalPaymentTo - equalPaymentFrom);
    }
}