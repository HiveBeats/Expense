using Expense.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expense.Infrastructure.Configuration;

public class EventConfiguration:IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.Property(m => m.Id)
            .HasConversion((id) => id.ToString(), (str) => Guid.Parse(str))
            .HasMaxLength(36)
            .ValueGeneratedNever();

        builder.Property(m => m.Name)
            .HasMaxLength(128);

        builder.HasMany(x => x.Attendees)
            .WithOne(x => x.Event);
    }
}