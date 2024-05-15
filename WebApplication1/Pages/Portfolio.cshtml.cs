using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GoodWeb;
using GoodWeb.Models;

namespace MyApp.Namespace
{
    public class PortfolioModel : PageModel
    {
        public PortfolioItemsService portfolioItemsService;
        public IEnumerable<PortfolioItem> PortfolioItems { get; private set; }
        public PortfolioModel(PortfolioItemsService itemsService)
        {
            portfolioItemsService = itemsService;
            PortfolioItems = [];
        }
        public void OnGet()
        {
            PortfolioItems = portfolioItemsService.GetPortfolioItems() ?? [];
        }
    }
}
