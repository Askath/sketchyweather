using sketchyWeatherApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapGet("/weatherforecast", () =>
    {
        WeatherApi api = new WeatherApi(new HttpClient());
        Location location = api.getLocation("holzwickede");
        
        api = new WeatherApi(new HttpClient());
        var forecast = api.getWeather(location.lat, location.lon);
        return forecast;
    })
    .WithName("GetWeatherForecast");

app.Run();