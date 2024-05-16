using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GoodWeb;
using GoodWeb.Models;

namespace MyApp.Namespace
{
    public class PortfolioModel : PageModel
    {
        public PortfolioService portfolioItemsService;
        public IEnumerable<PortfolioItem> PortfolioItems { get; private set; }
        public PortfolioModel(PortfolioService itemsService)
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
