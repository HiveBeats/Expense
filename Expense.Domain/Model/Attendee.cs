namespace Expense.Domain.Model;

public class Attendee
{
    private List<Expense> _expenses = new List<Expense>();

    private Attendee()
    {
        
    }
    
    public Attendee(Event @event, string name)
    {
        Id = Guid.NewGuid();
        Event = @event;
        Name = name;
    }
    
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    //navigation property
    public Event Event { get; private set; }
    public IEnumerable<Expense> Expenses
    {
        get { return _expenses; } 
        private set { _expenses = value.ToList(); }
    }

    public void AddExpense(string name, decimal amount)
    {
        _expenses.Add(new Expense(this, name, amount));
    }

    public void RemoveExpense(Guid id)
    {
        var expense = Expenses.Single(x => x.Id == id);
        _expenses.Remove(expense);
    }
}