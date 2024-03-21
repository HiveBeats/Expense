using AutoMapper;
using Expense.Domain.Interface;
using Expense.Domain.Model;
using Expense.Models;
using Microsoft.AspNetCore.Mvc;

namespace Expense.Controllers;

public class EventController: Controller
{
    private readonly IMapper _mapper;
    private readonly IEventRepository _repository;

    public EventController(IMapper mapper, IEventRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    // GET: /event/{id}
    [HttpGet("/event/")]
    public IActionResult Index()
    {
        return View();
    }
    
    // POST: Process the form submission
    [HttpPost]
    public async Task<IActionResult> AddExpense(string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            var @event = new Event(name);
            _repository.Insert(@event);
            await _repository.SaveChanges();
            return Redirect("/event/" + @event.Id);
        }

        return BadRequest();
    }

    // GET: /event/{id}
    [HttpGet("/event/{id:guid}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var @event = await _repository.Get(id, true);
        return View(_mapper.Map<EventViewModel>(@event));
    }

    [HttpPost]
    public async Task<IActionResult> AddAttendee(Guid id, string name)
    {
        var @event = await _repository.Get(id);
        @event.AddAttendee(name);
        await _repository.SaveChanges();
        return Redirect("/event/" + @event.Id);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddAttendeeExpense(Guid eventId, Guid attendeeId, string name, decimal amount)
    {
        var @event = await _repository.Get(eventId);
        @event.AddAttendeeExpense(attendeeId, name, amount);
        await _repository.SaveChanges();
        return Redirect("/event/" + @event.Id);
    }
    
    [HttpPost]
    public async Task<IActionResult> JoinFamilyAttendees(Guid eventId, Guid attendeeId, IEnumerable<Guid> dependantIds)
    {
        var @event = await _repository.Get(eventId);
        @event.JoinFamilyAttendees(attendeeId, dependantIds);
        await _repository.SaveChanges();
        return Redirect("/event/" + @event.Id);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAttendeePaymentsViewContent([FromQuery]Guid id, [FromQuery]Guid from, [FromQuery]Guid to)
    {
        var @event = await _repository.Get(id, true);
        var result = new List<AttendeePayment>();
        if (to != Guid.Empty)
        {
            result.Add(@event.GetAttendeeSummaryPayment(from, to));
        }
        else
        {
            result.AddRange(@event.GetAttendeeSummaryPayments(from));
        }
        
        return PartialView("AttendeePaymentTo", result.Select(x => _mapper.Map<AttendeePaymentViewModel>(x)).ToList());
    }
}