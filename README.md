# SketchyWeather 🌤️

A lightweight weather API service built with .NET 10 that integrates seamlessly with SketchyBar to display real-time weather information in your macOS menu bar.

## Preview

<img width="215" height="36" alt="SketchyBar Weather Integration" src="https://github.com/user-attachments/assets/cbcda572-375b-4de8-a84b-0ab4b3ed5a66" />

*Weather data displayed directly in your macOS menu bar via SketchyBar*

## Features

- 🌍 **Location-based Weather**: Fetches weather data for any city using OpenWeatherMap API
- 🌡️ **Temperature Display**: Shows current temperature in Celsius
- 🔄 **Auto-refresh**: Updates every 5 minutes (configurable)
- 🐳 **Docker Support**: Easy deployment with Docker and Docker Compose
- 🎨 **SketchyBar Integration**: Seamlessly integrates with macOS SketchyBar
- 🚀 **RESTful API**: Simple HTTP endpoint for weather data

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) (for local development)
- [Docker](https://www.docker.com/) (for containerized deployment)
- [SketchyBar](https://github.com/FelixKratz/SketchyBar) (for menu bar integration)
- OpenWeatherMap API Key (free tier available at [openweathermap.org](https://openweathermap.org/api))

## Installation

### Option 1: Docker (Recommended)

1. Clone the repository:
   ```bash
   git clone https://github.com/Askath/sketchyweather.git
   cd sketchyweather
   ```

2. Set your OpenWeatherMap API key as an environment variable:
   ```bash
   export API_KEY_WEATHER="your_api_key_here"
   ```

3. Run with Docker Compose:
   ```bash
   docker-compose up -d
   ```

### Option 2: Local Development

1. Clone the repository:
   ```bash
   git clone https://github.com/Askath/sketchyweather.git
   cd sketchyweather
   ```

2. Set your OpenWeatherMap API key:
   ```bash
   export API_KEY_WEATHER="your_api_key_here"
   ```

3. Build and run the application:
   ```bash
   dotnet restore
   dotnet build
   dotnet run
   ```

## Configuration

### API Key Setup

This application requires an OpenWeatherMap API key. You can get a free API key by:

1. Visiting [OpenWeatherMap](https://openweathermap.org/api)
2. Creating an account
3. Generating an API key from your account dashboard
4. Setting it as an environment variable:
   ```bash
   export API_KEY_WEATHER="your_api_key_here"
   ```

### SketchyBar Integration

The `sketchy/` directory contains SketchyBar configuration files:

1. **items/weather.sh**: Configures the weather item in SketchyBar
   - Updates every 300 seconds (5 minutes)
   - Uses 🌤 as the default icon

2. **plugins/weather.sh**: Plugin script that fetches and displays weather data
   - Connects to the API at `localhost:8080/weatherforecast`
   - Displays location name and temperature

To integrate with SketchyBar:

```bash
# Copy the scripts to your SketchyBar plugin directory
cp -r sketchy/items/* ~/.config/sketchybar/items/
cp -r sketchy/plugins/* ~/.config/sketchybar/plugins/

# Make them executable
chmod +x ~/.config/sketchybar/items/weather.sh
chmod +x ~/.config/sketchybar/plugins/weather.sh

# Reload SketchyBar
sketchybar --reload
```

## Usage

### API Endpoint

Once running, the API is available at:

```
GET http://localhost:8080/weatherforecast
```

**Default Behavior**: Returns weather data for Holzwickede, Germany

**Response Example**:
```json
{
  "name": "Holzwickede",
  "temp": 15.3
}
```

### Customizing the Location

To change the default location, modify the `location` parameter in `Program.cs`:

```csharp
Location location = api.getLocation("holzwickede"); // Change to your city
```

## Project Structure

```
sketchyweather/
├── Program.cs              # Main application entry point
├── WeatherAPI.cs           # Weather API client implementation
├── Dockerfile              # Docker container configuration
├── compose.yaml            # Docker Compose configuration
├── sketchy/                # SketchyBar integration scripts
│   ├── items/              # SketchyBar item configurations
│   └── plugins/            # SketchyBar plugin scripts
├── example.json            # Sample OpenWeatherMap API response
└── README.md               # This file
```

## API Details

### Weather Data Structure

The API returns a simplified weather object containing:
- `name`: Location name
- `temp`: Current temperature in Celsius

The full OpenWeatherMap response is parsed internally. See `example.json` for the complete response structure.

## Docker Deployment

The application includes Docker support for easy deployment:

- **Base Image**: `mcr.microsoft.com/dotnet/aspnet:10.0`
- **Exposed Ports**: 8080 (HTTP), 8081 (HTTPS)
- **Environment Variables**: `API_KEY_WEATHER` (required)

## Development

### Building the Project

```bash
dotnet build
```

### Running Tests

```bash
dotnet test
```

### Running in Development Mode

```bash
dotnet run --environment Development
```

The application will be available with OpenAPI documentation at:
- API: `http://localhost:8080`
- OpenAPI: `http://localhost:8080/openapi/v1.json`

## Technologies Used

- **.NET 10**: Modern C# web framework
- **OpenWeatherMap API**: Weather data provider
- **SketchyBar**: macOS menu bar customization tool
- **Docker**: Containerization platform
- **jq**: JSON processing in shell scripts

## License

This project is open source. Please check with the repository owner for specific license terms.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Acknowledgments

- [OpenWeatherMap](https://openweathermap.org/) for providing the weather API
- [SketchyBar](https://github.com/FelixKratz/SketchyBar) for the amazing macOS menu bar tool
