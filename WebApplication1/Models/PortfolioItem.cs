using System.Text.Json;

namespace GoodWeb.Models
{
    public class PortfolioItem
    {
        public string? Section { get; set; }
        public string? Image { get; set; }
        public string? Title { get; set; }
        public string? Category { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
