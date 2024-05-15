using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GoodWeb;
using GoodWeb.Models;

namespace MyApp.Namespace
{
    public class TestimonialForm
    {
        public string? Status { get; set; } = default;
        public required string Name { get; set; }
        public string? Position { get; set; }
        public required string Title { get; set; }
        public required string Text { get; set; }
    }
    public class TestimonialsModel : PageModel
    {
        [BindProperty]
        public required TestimonialForm Form { get; set; }
        private readonly TestimonialsContext _context;
        public List<Testimonial> Testimonials { get; private set; } = [];
        public TestimonialsModel(TestimonialsContext context)
        {
            _context = context;
            LoadTestimonials();
        }

        public IActionResult? OnPost()
        {
            if (Form.Name == null)
            {
                Form.Status = "Attention! You must enter your name.";
                return null;
            }

            if (Form.Title == null)
            {
                Form.Status = "Attention! Please, enter a title.";
                return null;
            }

            if (Form.Text == null)
            {
                Form.Status = "Attention! Please, enter your message.";
                return null;
            }

            TrimFormFields();

            Author author = _context.Authors
                .Where(a => a.Name == Form.Name && a.Position == Form.Position)
                .FirstOrDefault()
                ?? new() { Name = Form.Name, Position = Form.Position };

            Testimonial testimonial = new()
            {
                Quote = Form.Title,
                Text = Form.Text,
                Author = author
            };

            _context.Testimonials.Add(testimonial);
            _context.SaveChanges();

            return Redirect("/Testimonials");
        }

        void TrimFormFields()
        {
            Form.Name = Form.Name.Trim();
            Form.Position = Form.Position?.Trim() ?? "";
            Form.Title = Form.Title.Trim();
            Form.Text = Form.Text.Trim();
        }

        async void LoadTestimonials()
        {
            Testimonials = await _context.Testimonials
                .Include(t => t.Author)
                .ToListAsync();
        }
    }
}
