using System.Diagnostics;
using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;
using Microsoft.AspNetCore.Mvc;
using HelloWorld.Models;

namespace HelloWorld.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAmazonCloudWatch _amazonCloudWatch;


    public HomeController(ILogger<HomeController> logger, IAmazonCloudWatch amazonCloudWatch)
    {
        _logger = logger;
        _amazonCloudWatch = amazonCloudWatch;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    
    public IActionResult Health()
    {
        return View();
    }
    
    public IActionResult Metric()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Metric(int metric)
    {
        try
        {
            await _amazonCloudWatch.PutMetricDataAsync(new PutMetricDataRequest
            {
                Namespace = "Demo",
                MetricData = new List<MetricDatum>
                {
                    new MetricDatum
                    {
                        MetricName = "SprintOneCustomMetric",
                        Value = metric,
                        Unit = StandardUnit.Count,
                        TimestampUtc = DateTime.UtcNow,
                    }
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Failed to send CloudWatch Metric");
            throw;
        } 
        
        ViewBag.Result = "Metric saved!";
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}