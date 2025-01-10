# WeatherAPI .NET Service

This is a wrapper service for the VisualCrossing Weather API, providing weather data with enhanced performance through a caching mechanism using Redis. The service fetches weather information and stores it in Redis for quick retrieval.

## Features
- Weather data wrapper for VisualCrossing API.
- Caching with Redis to optimize repeated requests.
- Easy integration for .NET applications.

## Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/Deepanc7/WeatherAPI.git
2. Install dependencies:
   dotnet add package StackExchange.Redis
   dotnet add package Microsoft.Extensions.Caching.StackExchangeRedis
3. Start Docker container
   docker compose up
4. Run the service
   dotnet run
