namespace GoodWeb.Models;

public class Author
{
    public int Id {get; set;}
    public required string Name {get; set;}
    public string? Position {get; set;}
    public string Photo {get; set;} = "uploads/default.png";
}
