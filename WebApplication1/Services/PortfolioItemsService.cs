using System.Collections;
using System.Text.Json;
using GoodWeb.Models;

namespace GoodWeb;

public class PortfolioItemsService
{
    private readonly ILogger<PortfolioItemsService> _logger;
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
    public PortfolioItemsService(IWebHostEnvironment webHostEnvironment, ILogger<PortfolioItemsService> logger)
    {
        WebHostEnvironment = webHostEnvironment;
        _logger = logger;
    }
    public IWebHostEnvironment WebHostEnvironment { get; }
    private string DataFilePath
    {
        get { return Path.Combine(WebHostEnvironment.ContentRootPath, "Data", "portfolio.json"); }
    }
    public IEnumerable<PortfolioItem>? GetPortfolioItems()
    {
        IEnumerable<PortfolioItem>? data = null;
        try
        {
            using var r = File.OpenText(DataFilePath);
            data = JsonSerializer.Deserialize<PortfolioItem[]>(r.ReadToEnd(), JsonSerializerOptions);
        }
        catch (DirectoryNotFoundException e)
        {
            _logger.LogError("{Description}", e.Message);
            data = [];
        }
        return data;
    }
}
