namespace Expense.Domain.Model;

public record AttendeePayment(Attendee From, Attendee To, decimal Amount);