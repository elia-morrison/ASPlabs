using GoodWeb.Models;

namespace GoodWeb;

public class DummyTestimonialCreator
{
    public static void Initialize(TestimonialsContext context)
    {
        if (context.Authors.Any())
            return;

        var authors = new List<Author>
        {
            new Author {Name = "James Fernando", Position = "Manager of Racer", Photo="uploads/testi_01.png"},
            new Author {Name = "Jacques Philips", Position= "Designer", Photo="uploads/testi_02.png"},
            new Author {Name = "Venanda Mercy", Position="Newyork City", Photo="uploads/testi_03.png"},
        };

        context.Authors.AddRange(authors);
        context.SaveChanges();

        var testimonials = new List<Testimonial>
        {
            new Testimonial {
                Quote = "Wonderful Support!",
                Text="They have got my project on time with the competition with a sed highly skilled, and experienced & professional team.",
                Author=authors[0]
            },
            new Testimonial {
                Quote="Awesome Services!",
                Text="Explain to you how all this mistaken idea of denouncing pleasure and praising pain was born and I will give you completed.",
                Author=authors[1]
            },
            new Testimonial {
                Quote="Great & Talented Team!",
                Text="The master-builder of human happiness no one rejects, dislikes avoids pleasure itself, because it is very pursue pleasure.",
                Author=authors[2]
            }
        };

        context.Testimonials.AddRange(testimonials);
        context.SaveChanges();
    }
}
