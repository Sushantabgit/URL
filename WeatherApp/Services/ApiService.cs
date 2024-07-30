using Newtonsoft.Json;
using WeatherApp.Models;

namespace WeatherApp.Services;

public class ApiService
{
    public static async Task<Root> getWeather(double latitude, double longitude){
        var httpClient = new HttpClient();
        var response = await httpClient.GetStringAsync(string.Format("http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&appid=f68cd597dccdc52bef388b4a9428087c",latitude,longitude));
        return JsonConvert.DeserializeObject<Root>(response);
    }
    public static async Task<Root> getWeatherCity(string cityName){
        var httpClient = new HttpClient();
        var response = await httpClient.GetStringAsync(string.Format("http://api.openweathermap.org/data/2.5/weather?q={0},in&APPID=f68cd597dccdc52bef388b4a9428087c",cityName));
        return JsonConvert.DeserializeObject<Root>(response);
    }
}
