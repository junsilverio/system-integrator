# System Integrator

A comprehensive Web Application for monitoring and configuring API Integrations built with Blazor and .NET 10.

## Features

### 1. Dashboard - Real-Time API Health Monitoring
- **Live Status Monitoring**: Real-time health checks of all configured APIs
- **Status Indicators**: Visual representation (Healthy, Degraded, Down)
- **Response Time Tracking**: Monitor API performance with response time metrics
- **Auto-Refresh**: Automatic health checks every 30 seconds
- **Statistics Overview**: Quick stats showing total, healthy, degraded, and down APIs

### 2. Maintenance Page - API Configuration Management
- **CRUD Operations**: Create, Read, Update, and Delete API configurations
- **Enable/Disable**: Toggle API status without deleting configurations
- **Configuration Details**: Manage API name, URL, description, and status
- **Validation**: Built-in form validation for data integrity
- **Reports & Statistics**: View total APIs, enabled APIs, and integration counts

### 3. Integrations Page - System Integration Configuration
- **Integration Management**: Configure integrations for different systems
- **Multiple Integration Types**: Support for REST API, SOAP, GraphQL, Webhook, Database, Message Queue, and more
- **Authentication Support**: Configure various authentication methods (API Key, OAuth 2.0, JWT, Basic Auth, Certificate)
- **Flexible Configuration**: JSON-based configuration for custom integration settings
- **Filtering**: Filter integrations by API or active status
- **Active/Inactive Toggle**: Enable or disable integrations as needed

## Technology Stack

- **Framework**: Blazor Server (.NET 10)
- **Database**: SQLite with Entity Framework Core
- **UI**: Bootstrap 5 with custom styling
- **Real-time Updates**: Blazor Interactive Server Components

## Getting Started

### Prerequisites
- .NET 10 SDK or later
- Any modern web browser

### Installation

1. Clone the repository:
```bash
git clone https://github.com/junsilverio/system-integrator.git
cd system-integrator
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Run the application:
```bash
cd src/SystemIntegrator.Web
dotnet run
```

4. Open your browser and navigate to:
```
https://localhost:5001
```

### Database

The application uses SQLite for data storage. The database is automatically created on first run at:
```
src/SystemIntegrator.Web/systemintegrator.db
```

## Project Structure

```
system-integrator/
├── src/
│   └── SystemIntegrator.Web/
│       ├── Components/
│       │   ├── Layout/        # Layout components (NavMenu, MainLayout)
│       │   └── Pages/         # Blazor pages (Dashboard, Maintenance, Integrations)
│       ├── Data/              # Database context
│       ├── Models/            # Data models (ApiConfiguration, IntegrationConfig)
│       ├── Services/          # Business logic services
│       └── wwwroot/           # Static files (CSS, JS)
└── SystemIntegrator.sln
```

## Usage

### Adding a New API

1. Navigate to the **Maintenance** page
2. Click **Add New API**
3. Fill in the required details:
   - Name: Descriptive name for the API
   - URL: Full API endpoint URL
   - Description: Optional description
   - Status: Enable or disable the API
4. Click **Save**

### Creating an Integration

1. Navigate to the **Integrations** page
2. Click **Add New Integration**
3. Configure the integration:
   - System Name: Name of the target system
   - Integration Type: Select from available types
   - API Configuration: Link to an existing API
   - Authentication Method: Select authentication type
   - Configuration: Add custom JSON configuration
4. Click **Save**

### Monitoring API Health

1. Navigate to the **Dashboard** page
2. View real-time health status of all APIs
3. Click refresh button on individual APIs for manual health checks
4. Use **Refresh All** to check all enabled APIs at once

## Features in Detail

### Auto Health Monitoring
- Automatic health checks every 30 seconds
- HTTP timeout of 10 seconds per request
- Status determination based on HTTP response codes
- Error tracking and display

### Data Management
- Complete CRUD operations for APIs and integrations
- Soft enable/disable without data deletion
- Cascading delete for related integrations
- Validation at both client and server side

### User Interface
- Responsive design for mobile and desktop
- Bootstrap 5 components
- Modal dialogs for forms
- Confirmation dialogs for destructive actions
- Real-time updates without page refresh

## Future Enhancements

- Authentication and authorization
- API health history and trending
- Alerting and notifications
- Export reports to PDF/Excel
- Advanced filtering and search
- API request/response logging
- Webhook endpoints for external notifications

## License

MIT License - see LICENSE file for details

## Author

Created for API Integration monitoring and management.
