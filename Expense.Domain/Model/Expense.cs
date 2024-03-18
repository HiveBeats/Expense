namespace Expense.Domain.Model;

public class Expense
{
    private Expense()
    {
        
    }
    
    public Expense(Attendee attendee, string name, decimal amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Can't be less than zero");
        
        Id = Guid.NewGuid();
        Attendee = attendee;
        Name = name;
        Amount = amount;
    }
    
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Amount { get; private set; }
    //navigation property
    public Attendee Attendee { get; private set; }
}