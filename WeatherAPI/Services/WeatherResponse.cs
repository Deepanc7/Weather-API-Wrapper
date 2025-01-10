// WeatherResponse.cs
using Newtonsoft.Json;
using WeatherAPI.Services.Caching;

public class WeatherResponse
{
    [JsonProperty("location")]
    public Location Location { get; set; }

    [JsonProperty("current")]
    public Current Current { get; set; }

    [JsonProperty("forecast")]
    public Forecast Forecast { get; set; }
}

public class Location
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("region")]
    public string Region { get; set; }

    [JsonProperty("country")]
    public string Country { get; set; }

    [JsonProperty("lat")]
    public double Latitude { get; set; }

    [JsonProperty("lon")]
    public double Longitude { get; set; }
}

public class Current
{
    [JsonProperty("temp_c")]
    public double TemperatureCelsius { get; set; }

    [JsonProperty("condition")]
    public Condition Condition { get; set; }
}

public class Condition
{
    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("icon")]
    public string Icon { get; set; }
}

public class Forecast
{
    [JsonProperty("forecastday")]
    public List<ForecastDay> ForecastDays { get; set; }
}

public class ForecastDay
{
    [JsonProperty("date")]
    public string Date { get; set; }

    [JsonProperty("day")]
    public Day Day { get; set; }
}

public class Day
{
    [JsonProperty("maxtemp_c")]
    public double MaxTempC { get; set; }

    [JsonProperty("mintemp_c")]
    public double MinTempC { get; set; }

    [JsonProperty("condition")]
    public Condition Condition { get; set; }
}