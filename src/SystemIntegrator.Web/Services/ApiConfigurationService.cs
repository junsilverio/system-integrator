using SystemIntegrator.Web.Data;
using SystemIntegrator.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace SystemIntegrator.Web.Services;

public class ApiConfigurationService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ApiConfigurationService> _logger;

    public ApiConfigurationService(ApplicationDbContext context, ILogger<ApiConfigurationService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<ApiConfiguration>> GetAllAsync()
    {
        return await _context.ApiConfigurations
            .Include(a => a.Integrations)
            .OrderBy(a => a.Name)
            .ToListAsync();
    }

    public async Task<ApiConfiguration?> GetByIdAsync(int id)
    {
        return await _context.ApiConfigurations
            .Include(a => a.Integrations)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<ApiConfiguration> CreateAsync(ApiConfiguration config)
    {
        config.CreatedAt = DateTime.UtcNow;
        _context.ApiConfigurations.Add(config);
        await _context.SaveChangesAsync();
        return config;
    }

    public async Task<ApiConfiguration> UpdateAsync(ApiConfiguration config)
    {
        config.UpdatedAt = DateTime.UtcNow;
        _context.ApiConfigurations.Update(config);
        await _context.SaveChangesAsync();
        return config;
    }

    public async Task DeleteAsync(int id)
    {
        var config = await _context.ApiConfigurations.FindAsync(id);
        if (config != null)
        {
            _context.ApiConfigurations.Remove(config);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ToggleStatusAsync(int id)
    {
        var config = await _context.ApiConfigurations.FindAsync(id);
        if (config != null)
        {
            config.IsEnabled = !config.IsEnabled;
            config.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return config.IsEnabled;
        }
        return false;
    }
}
