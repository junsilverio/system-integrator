using Microsoft.EntityFrameworkCore;
using SystemIntegrator.Web.Models;

namespace SystemIntegrator.Web.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ApiConfiguration> ApiConfigurations { get; set; }
    public DbSet<IntegrationConfig> IntegrationConfigs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApiConfiguration>()
            .HasMany(a => a.Integrations)
            .WithOne(i => i.ApiConfiguration)
            .HasForeignKey(i => i.ApiConfigurationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
