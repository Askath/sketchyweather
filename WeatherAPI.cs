namespace sketchyWeatherApi;

public struct Location
{
    public float lon { get; set; }
    public float lat { get; set; }
    public string name { get; set; }
    public string country { get; set; }
    public string state { get; set; }
    public LocalNames local_names { get; set; }
}

public struct LocalNames
{
    public string de { get; set; }
    public string ja { get; set; }
}

public class WeatherApi(HttpClient client, float lat = 0, float lon = 0)
{
    private readonly string _apiKey =
        Environment.GetEnvironmentVariable("API_KEY_WEATHER") ?? throw new Exception("API_KEY not set");
    private readonly string _baseUrl = "https://api.openweathermap.org/data/2.5/weather";
    private float _lat = lat;
    private float _lon = lon;

    private readonly HttpClient _client = client;

    public Location getLocation(string location)
    {
        _client.BaseAddress = new Uri("http://api.openweathermap.org/geo/1.0/direct");
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Add("Accept", "application/json");
        
        string query = $"?q={location}&appid={_apiKey}&limit=1";
        
        HttpResponseMessage response = _client.GetAsync(query).Result;
        response.EnsureSuccessStatusCode();
        var result = System.Text.Json.JsonSerializer.Deserialize<List<Location>>(response.Content.ReadAsStringAsync().Result);
        return result[0];
    }

    public string getWeather(float clat, float clon)
    {
        if (clat != 0) _lat = clat;
        if (clon != 0) _lon = clon;

        _client.BaseAddress = new Uri(_baseUrl);
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Add("Accept", "application/json");

        string query = $"?lat={_lat}&lon={_lon}&appid={_apiKey}&units=metric";

        HttpResponseMessage response = _client.GetAsync(query).Result;
        response.EnsureSuccessStatusCode();

        using var doc = System.Text.Json.JsonDocument.Parse(response.Content.ReadAsStringAsync().Result);

        string name = "";
        if (doc.RootElement.TryGetProperty("name", out var nameElement) &&
            nameElement.ValueKind == System.Text.Json.JsonValueKind.String)
        {
            name = nameElement.GetString() ?? "";
        }

        double temp = 0;
        if (doc.RootElement.TryGetProperty("main", out var mainElement) &&
            mainElement.TryGetProperty("temp", out var tempElement) &&
            tempElement.ValueKind == System.Text.Json.JsonValueKind.Number)
        {
            temp = tempElement.GetDouble();
        }


        var result = new { name,  temp };
        
        return System.Text.Json.JsonSerializer.Serialize(result);
    }
}