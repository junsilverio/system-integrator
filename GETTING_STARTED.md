# Getting Started Guide - System Integrator

## Quick Start

### 1. Prerequisites
- .NET 10 SDK installed
- A modern web browser (Chrome, Edge, Firefox, Safari)

### 2. Running the Application

Navigate to the project directory and run:

```bash
cd src/SystemIntegrator.Web
dotnet run
```

The application will start and display output similar to:
```
Now listening on: http://localhost:5279
Application started. Press Ctrl+C to shut down.
```

### 3. Access the Application

Open your browser and navigate to the displayed URL (e.g., `http://localhost:5279`)

## First-Time Setup

### Step 1: Add Your First API Configuration

1. Navigate to the **Maintenance** page from the sidebar
2. Click **Add New API** button
3. Fill in the details:
   - **Name**: e.g., "GitHub API"
   - **URL**: e.g., "https://api.github.com"
   - **Description**: e.g., "GitHub REST API"
   - **Enabled**: Check this box
4. Click **Save**

### Step 2: Monitor API Health

1. Navigate to the **Dashboard** page
2. Your newly added API will appear in the list
3. Click the refresh button (🔄) to check the API health
4. The status will update showing:
   - **Green (Healthy)**: API responded successfully (200-299 status)
   - **Yellow (Degraded)**: API returned a client error (400-499 status)
   - **Red (Down)**: API returned a server error (500+) or connection failed
   - Response time in milliseconds

### Step 3: Create an Integration

1. Navigate to the **Integrations** page
2. Click **Add New Integration**
3. Fill in the details:
   - **System Name**: e.g., "Payment Gateway"
   - **Integration Type**: Select from dropdown (e.g., "REST API")
   - **API Configuration**: Select the API you created earlier
   - **Authentication Method**: e.g., "API Key"
   - **Configuration (JSON)**: Add custom configuration in JSON format:
     ```json
     {
       "apiKey": "YOUR_API_KEY",
       "baseUrl": "https://api.example.com",
       "timeout": 30000
     }
     ```
   - **Active**: Check this box
4. Click **Save**

## Feature Overview

### Dashboard Features

- **Real-time Health Monitoring**: Auto-refreshes every 30 seconds
- **Manual Refresh**: Click individual API refresh buttons or "Refresh All"
- **Status Cards**: Quick overview of total, healthy, degraded, and down APIs
- **Detailed Table**: Shows each API with:
  - Name and status badge
  - URL
  - Current health status
  - Response time
  - Last check timestamp
  - Error messages (if any)

### Maintenance Features

- **Create API**: Add new API configurations
- **Edit API**: Update existing configurations
- **Enable/Disable**: Toggle without deleting data
- **Delete API**: Remove configurations (with warning if integrations exist)
- **Statistics**: View counts of total APIs, enabled APIs, and integrations

### Integrations Features

- **Create Integration**: Link systems to APIs
- **Edit Integration**: Update integration settings
- **Filter by API**: View integrations for specific APIs
- **Filter by Status**: Show only active or inactive integrations
- **Toggle Active**: Enable/disable integrations
- **Delete Integration**: Remove integration configurations
- **View Configuration**: Expand to see JSON configuration details

## Sample Data for Testing

### Sample API Endpoints for Testing:

1. **JSONPlaceholder** (Always Available)
   - Name: JSONPlaceholder API
   - URL: https://jsonplaceholder.typicode.com/posts
   - Great for testing as it always responds with 200 OK

2. **GitHub API**
   - Name: GitHub API
   - URL: https://api.github.com
   - No authentication required for basic access

3. **REST Countries**
   - Name: REST Countries API
   - URL: https://restcountries.com/v3.1/all
   - Provides country data

### Sample Integration Configuration:

```json
{
  "environment": "production",
  "apiKey": "your-api-key-here",
  "baseUrl": "https://api.example.com/v1",
  "timeout": 30000,
  "retryAttempts": 3,
  "headers": {
    "Content-Type": "application/json",
    "Accept": "application/json"
  }
}
```

## Tips and Best Practices

1. **Regular Monitoring**: Keep the Dashboard open to monitor API health in real-time
2. **Descriptive Names**: Use clear, descriptive names for APIs and integrations
3. **Disable vs Delete**: Use disable/inactive instead of delete to preserve history
4. **Test URLs**: Verify API URLs are accessible before saving
5. **Configuration Format**: Always use valid JSON in the Configuration field
6. **Health Check Timeout**: APIs that don't respond within 10 seconds are marked as Down

## Troubleshooting

### API Shows as "Down" but it's working

- Check if the URL is correct and accessible from your machine
- Verify there's no firewall blocking the connection
- Ensure the API responds within 10 seconds

### Database Error

- The database file is created automatically at: `src/SystemIntegrator.Web/systemintegrator.db`
- If you encounter issues, delete the database file and restart the application

### Application Won't Start

- Ensure .NET 10 SDK is installed: `dotnet --version`
- Check if port 5279 is available (or use the port shown in console output)
- Verify all NuGet packages are restored: `dotnet restore`

## Database Location

The SQLite database is stored at:
```
src/SystemIntegrator.Web/systemintegrator.db
```

You can use SQLite tools to view/edit the database directly if needed.

## Development Mode

The application runs in Development mode by default, which provides:
- Detailed error messages
- Hot reload support (when using `dotnet watch run`)
- Browser refresh on code changes

## Stopping the Application

Press `Ctrl+C` in the terminal where the application is running.

## Next Steps

- Add more APIs to monitor
- Create integrations for your systems
- Customize authentication methods
- Explore filtering and search capabilities
- Monitor API performance trends

For more information, see the main README.md file.
