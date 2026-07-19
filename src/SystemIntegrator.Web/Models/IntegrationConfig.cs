using System.ComponentModel.DataAnnotations;

namespace SystemIntegrator.Web.Models;

public class IntegrationConfig
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string SystemName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string IntegrationType { get; set; } = string.Empty;

    public int ApiConfigurationId { get; set; }

    public ApiConfiguration? ApiConfiguration { get; set; }

    [StringLength(500)]
    public string? AuthenticationMethod { get; set; }

    [StringLength(2000)]
    public string? ConfigurationJson { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
