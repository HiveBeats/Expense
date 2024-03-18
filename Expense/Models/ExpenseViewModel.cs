namespace Expense.Models;

public class ExpenseViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
}