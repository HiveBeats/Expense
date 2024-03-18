namespace Expense.Models;

public class EventViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<AttendeeViewModel> Attendees { get; set; }
}