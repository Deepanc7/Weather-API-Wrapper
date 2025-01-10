using WeatherAPI.Services.Caching;

namespace WeatherAPI.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly IRedisCacheService _cache;

        public WeatherService(HttpClient httpClient, string apiKey, IRedisCacheService cache)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<dynamic> GetWeatherAsync(string location)
        {
            var cachedData = _cache.GetData<dynamic>(location);
            if (cachedData != null)
            {
                return cachedData;
            }

            var url = $"forecast?location={location}&key={_apiKey}";
            try
            {
                var httpResponse = await _httpClient.GetAsync(url);
                httpResponse.EnsureSuccessStatusCode();

                var responseData = await httpResponse.Content.ReadFromJsonAsync<dynamic>();
                _cache.SetData(location, responseData);

                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching weather data: {ex.Message}", ex);
            }
        }
    }
}