using Expense.Infrastructure;
using Expense.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<ExpenseDbContext>(options => options
    .UseNpgsql(connectionString: builder.Configuration.GetConnectionString("Expense"))
    .UseLoggerFactory(LoggerFactory.Create(logBuilder => logBuilder.AddConsole())));
builder.Services.AddEventRepository();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Migrate database
using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope())
{
    await serviceScope.ServiceProvider.GetRequiredService<ExpenseDbContext>().Database.MigrateAsync();
}



app.Run();