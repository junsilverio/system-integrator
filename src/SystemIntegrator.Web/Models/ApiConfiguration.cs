using System.ComponentModel.DataAnnotations;

namespace SystemIntegrator.Web.Models;

public class ApiConfiguration
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Url]
    [StringLength(500)]
    public string Url { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    public bool IsEnabled { get; set; } = true;

    public ApiStatus Status { get; set; } = ApiStatus.Unknown;

    public int? ResponseTime { get; set; }

    public DateTime? LastChecked { get; set; }

    [StringLength(1000)]
    public string? LastError { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public List<IntegrationConfig> Integrations { get; set; } = new();
}

public enum ApiStatus
{
    Unknown,
    Healthy,
    Degraded,
    Down
}
