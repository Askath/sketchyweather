# SketchyWeather 🌤️

A simple .NET weather API for SketchyBar that displays weather info in your macOS menu bar.

<img width="215" height="36" alt="SketchyBar Weather Integration" src="https://github.com/user-attachments/assets/cbcda572-375b-4de8-a84b-0ab4b3ed5a66" />

## Setup

1. Get a free API key from [OpenWeatherMap](https://openweathermap.org/api)
2. Set the environment variable:
   ```bash
   export API_KEY_WEATHER="your_api_key_here"
   ```
3. Run with Docker:
   ```bash
   docker-compose up -d
   ```

Or run locally:
```bash
dotnet run
```

## SketchyBar Integration

Copy the scripts to your SketchyBar config:
```bash
cp -r sketchy/items/* ~/.config/sketchybar/items/
cp -r sketchy/plugins/* ~/.config/sketchybar/plugins/
chmod +x ~/.config/sketchybar/items/weather.sh
chmod +x ~/.config/sketchybar/plugins/weather.sh
sketchybar --reload
```

## Usage

The API runs on `localhost:8080/weatherforecast` and returns:
```json
{
  "name": "Holzwickede",
  "temp": 15.3
}
```

To change the location, edit `Program.cs` and modify the city name in `getLocation()`.
