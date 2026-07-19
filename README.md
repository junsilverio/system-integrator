# system-integrator

Blazor (.NET 10) web app for monitoring and configuring API integrations.

## Features

1. **Dashboard** (`/`) for near real-time API health monitoring with auto-refresh.
2. **Maintenance** (`/maintenance`) for adding, updating, enabling/disabling, removing, and reporting API configurations and URLs.
3. **Integrations** (`/integrations`) for configuring integrations for different systems.
4. SmartAdmin-inspired, customizable theme behavior (light/dark switch in layout).

## Run

```bash
dotnet run --project /home/runner/work/system-integrator/system-integrator/SystemIntegrator.Web/SystemIntegrator.Web.csproj
```