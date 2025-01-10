using WeatherAPI.Services;
using WeatherAPI.Services.Caching;

var builder = WebApplication.CreateBuilder(args);

// Add Redis Cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "_Weather";
});

// Register Redis Cache Service as Singleton
builder.Services.AddSingleton<IRedisCacheService, RedisCacheService>();

builder.Services.AddHttpClient<WeatherService>();

// Register WeatherService separately to resolve dependencies like apiKey and cache
builder.Services.AddTransient<WeatherService>(sp =>
{
    var httpClient = sp.GetRequiredService<HttpClient>();
    httpClient.BaseAddress = new Uri(builder.Configuration["ApiSettings:VisualCrossingBaseUrl"]);

    var apiKey = builder.Configuration["ApiKeys:VisualCrossing"]
                 ?? Environment.GetEnvironmentVariable("VISUAL_CROSSING_API_KEY");

    if (string.IsNullOrEmpty(apiKey))
    {
        throw new Exception("Visual Crossing API key is not configured.");
    }

    var cache = sp.GetRequiredService<IRedisCacheService>();

    return new WeatherService(httpClient, apiKey, cache);
});

builder.Services.AddScoped<IRedisCacheService, RedisCacheService>();

// Add controllers and Newtonsoft.Json support
builder.Services.AddControllers()
    .AddNewtonsoftJson();

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
