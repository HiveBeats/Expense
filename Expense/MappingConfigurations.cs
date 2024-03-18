using AutoMapper;
using Expense.Domain.Model;
using Expense.Models;

namespace Expense;

public class MappingConfigurations: Profile
{
    public MappingConfigurations()
    {
        CreateMap<Event, EventViewModel>();
        CreateMap<Attendee, AttendeeViewModel>();
        CreateMap<Domain.Model.Expense, ExpenseViewModel>();
    }
}