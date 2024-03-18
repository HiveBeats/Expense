using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        var @event = await _db.Events
            .Include(x => x.Attendees)
            .ThenInclude(x => x.Expenses)
            .SingleAsync(x => x.Id == id);

        return View(_mapper.Map<EventViewModel>(@event));
    }
}