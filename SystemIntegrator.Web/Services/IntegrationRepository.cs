namespace SystemIntegrator.Web.Services;

public sealed class IntegrationRepository
{
    private readonly object _gate = new();
    private readonly List<ApiConfiguration> _apiConfigurations =
    [
        new(Guid.NewGuid(), "Orders API", "https://orders.example.internal/api", "Production", true, DateTime.UtcNow),
        new(Guid.NewGuid(), "Inventory API", "https://inventory.example.internal/api", "Production", true, DateTime.UtcNow),
        new(Guid.NewGuid(), "Billing API", "https://billing.example.internal/api", "Staging", false, DateTime.UtcNow)
    ];

    private readonly List<SystemIntegrationConfiguration> _integrationConfigurations =
    [
        new(Guid.NewGuid(), "ERP", "OAuth2", "https://erp.example.internal/sync", 30, true, DateTime.UtcNow),
        new(Guid.NewGuid(), "CRM", "API Key", "https://crm.example.internal/hooks", 45, true, DateTime.UtcNow),
        new(Guid.NewGuid(), "HRIS", "Basic Auth", "https://hris.example.internal/int", 60, false, DateTime.UtcNow)
    ];

    public IReadOnlyList<ApiConfiguration> GetApiConfigurations()
    {
        lock (_gate)
        {
            return _apiConfigurations
                .OrderBy(x => x.Name, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }
    }

    public ApiConfigurationReport GetApiConfigurationReport()
    {
        lock (_gate)
        {
            var enabled = _apiConfigurations.Count(x => x.Enabled);
            return new ApiConfigurationReport(
                _apiConfigurations.Count,
                enabled,
                _apiConfigurations.Count - enabled);
        }
    }

    public IReadOnlyList<ApiHealthStatus> GetApiHealthStatuses()
    {
        lock (_gate)
        {
            var now = DateTime.UtcNow;
            return _apiConfigurations
                .Select(x =>
                {
                    if (!x.Enabled)
                    {
                        return new ApiHealthStatus(x.Id, x.Name, "Disabled", 0, now);
                    }

                    var draw = Random.Shared.Next(100);
                    var status = draw < 78 ? "Healthy" : draw < 93 ? "Degraded" : "Down";
                    var latencyMs = status switch
                    {
                        "Healthy" => Random.Shared.Next(20, 120),
                        "Degraded" => Random.Shared.Next(200, 700),
                        _ => Random.Shared.Next(800, 2500)
                    };
                    return new ApiHealthStatus(x.Id, x.Name, status, latencyMs, now);
                })
                .ToList();
        }
    }

    public void UpsertApiConfiguration(ApiConfigurationInput input)
    {
        lock (_gate)
        {
            var normalized = input with
            {
                Name = input.Name.Trim(),
                BaseUrl = input.BaseUrl.Trim(),
                Environment = input.Environment.Trim()
            };

            if (normalized.Id is null)
            {
                _apiConfigurations.Add(new ApiConfiguration(
                    Guid.NewGuid(),
                    normalized.Name,
                    normalized.BaseUrl,
                    normalized.Environment,
                    normalized.Enabled,
                    DateTime.UtcNow));
                return;
            }

            var index = _apiConfigurations.FindIndex(x => x.Id == normalized.Id.Value);
            if (index >= 0)
            {
                _apiConfigurations[index] = _apiConfigurations[index] with
                {
                    Name = normalized.Name,
                    BaseUrl = normalized.BaseUrl,
                    Environment = normalized.Environment,
                    Enabled = normalized.Enabled,
                    UpdatedAtUtc = DateTime.UtcNow
                };
            }
        }
    }

    public void RemoveApiConfiguration(Guid id)
    {
        lock (_gate)
        {
            _apiConfigurations.RemoveAll(x => x.Id == id);
        }
    }

    public void SetApiConfigurationEnabled(Guid id, bool enabled)
    {
        lock (_gate)
        {
            var index = _apiConfigurations.FindIndex(x => x.Id == id);
            if (index >= 0)
            {
                _apiConfigurations[index] = _apiConfigurations[index] with
                {
                    Enabled = enabled,
                    UpdatedAtUtc = DateTime.UtcNow
                };
            }
        }
    }

    public IReadOnlyList<SystemIntegrationConfiguration> GetSystemIntegrations()
    {
        lock (_gate)
        {
            return _integrationConfigurations
                .OrderBy(x => x.SystemName, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }
    }

    public IntegrationConfigurationReport GetIntegrationConfigurationReport()
    {
        lock (_gate)
        {
            var enabled = _integrationConfigurations.Count(x => x.Enabled);
            return new IntegrationConfigurationReport(
                _integrationConfigurations.Count,
                enabled,
                _integrationConfigurations.Count - enabled);
        }
    }

    public void UpsertSystemIntegration(SystemIntegrationInput input)
    {
        lock (_gate)
        {
            var normalized = input with
            {
                SystemName = input.SystemName.Trim(),
                AuthenticationType = input.AuthenticationType.Trim(),
                EndpointUrl = input.EndpointUrl.Trim()
            };

            if (normalized.Id is null)
            {
                _integrationConfigurations.Add(new SystemIntegrationConfiguration(
                    Guid.NewGuid(),
                    normalized.SystemName,
                    normalized.AuthenticationType,
                    normalized.EndpointUrl,
                    normalized.TimeoutSeconds,
                    normalized.Enabled,
                    DateTime.UtcNow));
                return;
            }

            var index = _integrationConfigurations.FindIndex(x => x.Id == normalized.Id.Value);
            if (index >= 0)
            {
                _integrationConfigurations[index] = _integrationConfigurations[index] with
                {
                    SystemName = normalized.SystemName,
                    AuthenticationType = normalized.AuthenticationType,
                    EndpointUrl = normalized.EndpointUrl,
                    TimeoutSeconds = normalized.TimeoutSeconds,
                    Enabled = normalized.Enabled,
                    UpdatedAtUtc = DateTime.UtcNow
                };
            }
        }
    }

    public void SetSystemIntegrationEnabled(Guid id, bool enabled)
    {
        lock (_gate)
        {
            var index = _integrationConfigurations.FindIndex(x => x.Id == id);
            if (index >= 0)
            {
                _integrationConfigurations[index] = _integrationConfigurations[index] with
                {
                    Enabled = enabled,
                    UpdatedAtUtc = DateTime.UtcNow
                };
            }
        }
    }

    public void RemoveSystemIntegration(Guid id)
    {
        lock (_gate)
        {
            _integrationConfigurations.RemoveAll(x => x.Id == id);
        }
    }
}

public sealed record ApiConfiguration(
    Guid Id,
    string Name,
    string BaseUrl,
    string Environment,
    bool Enabled,
    DateTime UpdatedAtUtc);

public sealed record ApiConfigurationInput(
    Guid? Id,
    string Name,
    string BaseUrl,
    string Environment,
    bool Enabled);

public sealed record ApiConfigurationReport(
    int Total,
    int Enabled,
    int Disabled);

public sealed record ApiHealthStatus(
    Guid ApiId,
    string ApiName,
    string Status,
    int ResponseTimeMs,
    DateTime CheckedAtUtc);

public sealed record SystemIntegrationConfiguration(
    Guid Id,
    string SystemName,
    string AuthenticationType,
    string EndpointUrl,
    int TimeoutSeconds,
    bool Enabled,
    DateTime UpdatedAtUtc);

public sealed record SystemIntegrationInput(
    Guid? Id,
    string SystemName,
    string AuthenticationType,
    string EndpointUrl,
    int TimeoutSeconds,
    bool Enabled);

public sealed record IntegrationConfigurationReport(
    int Total,
    int Enabled,
    int Disabled);
