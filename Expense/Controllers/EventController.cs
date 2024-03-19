using AutoMapper;
using Expense.Domain.Model;
using Expense.Infrastructure;
using Expense.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Expense.Controllers;

public class EventController: Controller
{
    private readonly IMapper _mapper;
    private readonly ExpenseDbContext _db;

    public EventController(IMapper mapper, ExpenseDbContext db)
    {
        _mapper = mapper;
        _db = db;
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
            _db.Events.Add(@event);
            await _db.SaveChangesAsync();
            return Redirect("/event/" + @event.Id);
        }

        return BadRequest();
    }

    // GET: /event/{id}
    [HttpGet("/event/{id:guid}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var @event = await GetEvent(id, true);
        return View(_mapper.Map<EventViewModel>(@event));
    }

    [HttpPost]
    public async Task<IActionResult> AddAttendee(Guid id, string name)
    {
        var @event = await GetEvent(id);
        @event.AddAttendee(name);
        await _db.SaveChangesAsync();
        return Redirect("/event/" + @event.Id);;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddAttendeeExpense(Guid eventId, Guid attendeeId, string name, decimal amount)
    {
        var @event = await GetEvent(eventId);
        @event.AddAttendeeExpense(attendeeId, name, amount);
        await _db.SaveChangesAsync();
        return Redirect("/event/" + @event.Id);;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAttendeePaymentsViewContent([FromQuery]Guid id, [FromQuery]Guid from, [FromQuery]Guid to)
    {
        var @event = await GetEvent(id, true);
        var result = @event.GetAttendeeSummaryPayment(from, to);
        return PartialView("AttendeePaymentTo", _mapper.Map<AttendeePaymentViewModel>(result));
    }
    
    private async Task<Event> GetEvent(Guid id, bool notracking = false)
    {
        var query = _db.Events.AsQueryable();
        if (notracking)
        {
            query = query.AsNoTracking();
        }
        
        return await query
            .Include(x => x.Attendees)
            .ThenInclude(x => x.Expenses)
            .SingleAsync(x => x.Id == id);
    }
}