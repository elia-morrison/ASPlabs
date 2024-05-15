using Microsoft.EntityFrameworkCore;

namespace GoodWeb;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorPages();
        builder.Services.AddTransient<PortfolioItemsService>();
        builder.Services.AddDbContext<TestimonialsContext>(options =>
            options.UseSqlite(
                builder.Configuration.GetConnectionString(
                    "TestimonialsContextSQLite"
                )
            )
        );

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }
        else
        {
            app.UseDeveloperExceptionPage();
            app.UseMigrationsEndPoint();
        }

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<TestimonialsContext>();
            context.Database.EnsureCreated();
            DummyTestimonialCreator.Initialize(context);
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}
