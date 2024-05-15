using Microsoft.EntityFrameworkCore;
using GoodWeb.Models;

namespace GoodWeb;

public class TestimonialsContext : DbContext
{
    public TestimonialsContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Testimonial> Testimonials { get; set; }
    public DbSet<Author> Authors {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>().ToTable("Author");
        modelBuilder.Entity<Testimonial>().ToTable("Testimony");
    }
}
