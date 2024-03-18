using Expense.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Expense.Infrastructure;

public class ExpenseDbContext: DbContext
{
    public ExpenseDbContext()
    {
        
    }
    
    public ExpenseDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExpenseDbContext).Assembly);
    }

    public virtual DbSet<Event> Events { get; set; } = null!;
    public virtual DbSet<Attendee> Attendees { get; set; } = null!;
    public virtual DbSet<Domain.Model.Expense> Expenses { get; set; } = null!;
}