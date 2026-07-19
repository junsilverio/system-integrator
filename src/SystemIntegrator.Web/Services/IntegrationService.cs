using SystemIntegrator.Web.Data;
using SystemIntegrator.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace SystemIntegrator.Web.Services;

public class IntegrationService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<IntegrationService> _logger;

    public IntegrationService(ApplicationDbContext context, ILogger<IntegrationService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<IntegrationConfig>> GetAllAsync()
    {
        return await _context.IntegrationConfigs
            .Include(i => i.ApiConfiguration)
            .OrderBy(i => i.SystemName)
            .ToListAsync();
    }

    public async Task<IntegrationConfig?> GetByIdAsync(int id)
    {
        return await _context.IntegrationConfigs
            .Include(i => i.ApiConfiguration)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<List<IntegrationConfig>> GetByApiConfigIdAsync(int apiConfigId)
    {
        return await _context.IntegrationConfigs
            .Where(i => i.ApiConfigurationId == apiConfigId)
            .OrderBy(i => i.SystemName)
            .ToListAsync();
    }

    public async Task<IntegrationConfig> CreateAsync(IntegrationConfig config)
    {
        config.CreatedAt = DateTime.UtcNow;
        _context.IntegrationConfigs.Add(config);
        await _context.SaveChangesAsync();
        return config;
    }

    public async Task<IntegrationConfig> UpdateAsync(IntegrationConfig config)
    {
        config.UpdatedAt = DateTime.UtcNow;
        _context.IntegrationConfigs.Update(config);
        await _context.SaveChangesAsync();
        return config;
    }

    public async Task DeleteAsync(int id)
    {
        var config = await _context.IntegrationConfigs.FindAsync(id);
        if (config != null)
        {
            _context.IntegrationConfigs.Remove(config);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ToggleActiveAsync(int id)
    {
        var config = await _context.IntegrationConfigs.FindAsync(id);
        if (config != null)
        {
            config.IsActive = !config.IsActive;
            config.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return config.IsActive;
        }
        return false;
    }
}
