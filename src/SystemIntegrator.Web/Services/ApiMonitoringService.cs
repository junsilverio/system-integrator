using SystemIntegrator.Web.Models;
using System.Net.Http;
using System.Diagnostics;

namespace SystemIntegrator.Web.Services;

public class ApiMonitoringService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<ApiMonitoringService> _logger;

    public ApiMonitoringService(IHttpClientFactory httpClientFactory, ILogger<ApiMonitoringService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<(ApiStatus status, int responseTime, string? error)> CheckApiHealthAsync(string url)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.Timeout = TimeSpan.FromSeconds(10);

            var stopwatch = Stopwatch.StartNew();
            var response = await httpClient.GetAsync(url);
            stopwatch.Stop();

            var responseTime = (int)stopwatch.ElapsedMilliseconds;

            if (response.IsSuccessStatusCode)
            {
                return (ApiStatus.Healthy, responseTime, null);
            }
            else if ((int)response.StatusCode >= 500)
            {
                return (ApiStatus.Down, responseTime, $"Server error: {response.StatusCode}");
            }
            else
            {
                return (ApiStatus.Degraded, responseTime, $"Client error: {response.StatusCode}");
            }
        }
        catch (TaskCanceledException)
        {
            return (ApiStatus.Down, 0, "Request timeout");
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error checking API health for {Url}", url);
            return (ApiStatus.Down, 0, $"Connection error: {ex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error checking API health for {Url}", url);
            return (ApiStatus.Down, 0, $"Error: {ex.Message}");
        }
    }

    public async Task<Dictionary<string, object>> GetDashboardStatsAsync(List<ApiConfiguration> apis)
    {
        var stats = new Dictionary<string, object>
        {
            ["totalApis"] = apis.Count,
            ["enabledApis"] = apis.Count(a => a.IsEnabled),
            ["healthyApis"] = apis.Count(a => a.Status == ApiStatus.Healthy),
            ["degradedApis"] = apis.Count(a => a.Status == ApiStatus.Degraded),
            ["downApis"] = apis.Count(a => a.Status == ApiStatus.Down),
            ["avgResponseTime"] = apis.Where(a => a.ResponseTime.HasValue)
                .Select(a => a.ResponseTime!.Value)
                .DefaultIfEmpty(0)
                .Average()
        };

        return await Task.FromResult(stats);
    }
}
