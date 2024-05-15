namespace GoodWeb.Models;

public class Testimonial
{
    public int Id { get; set; }
    public required string Quote { get; set; }
    public required string Text { get; set; }
    public int AuthorId {get; set;}
    public Author? Author { get; set; }
}
