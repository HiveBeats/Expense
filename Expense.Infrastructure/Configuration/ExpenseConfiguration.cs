using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expense.Infrastructure.Configuration;

public class ExpenseConfiguration:IEntityTypeConfiguration<Domain.Model.Expense>
{
    public void Configure(EntityTypeBuilder<Domain.Model.Expense> builder)
    {
        builder.Property(m => m.Id)
            .HasConversion((id) => id.ToString(), (str) => Guid.Parse(str))
            .HasMaxLength(36)
            .ValueGeneratedNever();

        builder.Property(m => m.Name)
            .HasMaxLength(128);
    }
}