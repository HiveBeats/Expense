using Expense.Domain.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Expense.Repository;

public static class DependencyInjection
{
    public static IServiceCollection AddEventRepository(this IServiceCollection services)
    {
        return services.AddScoped<IEventRepository, EventRepository>();
    }
}