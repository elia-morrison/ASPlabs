using System.Collections;
using System.Text.Json;
using GoodWeb.Models;

namespace GoodWeb;

public class PortfolioService
{
    private readonly ILogger<PortfolioService> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public PortfolioService(IWebHostEnvironment webHostEnvironment, ILogger<PortfolioService> logger)
    {
        _webHostEnvironment = webHostEnvironment;
        _logger = logger;
    }

    private string DataFilePath => Path.Combine(_webHostEnvironment.ContentRootPath, "Data", "portfolio.json");

    public IEnumerable<PortfolioItem>? GetPortfolioItems()
    {
        try
        {
            using var r = File.OpenText(DataFilePath);
            return JsonSerializer.Deserialize<PortfolioItem[]>(r.ReadToEnd(), JsonSerializerOptions);
        }
        catch (DirectoryNotFoundException e)
        {
            _logger.LogError("{Description}", e.Message);
            return Array.Empty<PortfolioItem>();
        }
    }
}

