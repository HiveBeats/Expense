using AutoMapper;
using Expense.Domain.Model;
using Expense.Models;

namespace Expense;

public class MappingConfigurations: Profile
{
    public MappingConfigurations()
    {
        CreateMap<Event, EventViewModel>();
        CreateMap<Attendee, AttendeeViewModel>()
            .ForMember(d => d.InFamily, opt => opt.MapFrom(src => src.FamilyOwner != null || src.FamilyDependents.Any()));
        CreateMap<Domain.Model.Expense, ExpenseViewModel>();
        CreateMap<AttendeePayment, AttendeePaymentViewModel>()
            .ForMember(d => d.From, opt => opt.MapFrom(src => src.From.Name))
            .ForMember(d => d.To, opt => opt.MapFrom(src => src.To.Name));
    }
}