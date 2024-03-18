namespace Expense.Models;

public class AttendeeViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<ExpenseViewModel> Expenses { get; set; }
}