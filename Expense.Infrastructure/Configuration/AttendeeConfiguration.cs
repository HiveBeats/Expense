using Expense.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expense.Infrastructure.Configuration;

public class AttendeeConfiguration:IEntityTypeConfiguration<Attendee>
{
    public void Configure(EntityTypeBuilder<Attendee> builder)
    {
        builder.Property(m => m.Id)
            .HasConversion((id) => id.ToString(), (str) => Guid.Parse(str))
            .HasMaxLength(36)
            .ValueGeneratedNever();

        builder.Property(m => m.Name)
            .HasMaxLength(128);

        builder.HasMany(x => x.Expenses)
            .WithOne(x => x.Attendee);
    }
}