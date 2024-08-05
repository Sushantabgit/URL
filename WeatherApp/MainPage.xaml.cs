using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.ViewModels;

namespace WeatherApp;

public partial class MainPage : ContentPage
{
    private WeatherViewModel viewModel;

    public double latitude;
    public double longitude;

    public MainPage()
    {
        InitializeComponent();
        viewModel = (WeatherViewModel)BindingContext;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await getLocation();
        await GetWeatherDataByLocation(latitude, longitude);
    }

    public async Task getLocation()
    {
        var location = await Geolocation.GetLocationAsync();
        latitude = location.Latitude;
        longitude = location.Longitude;
    }

    private async void TapLocation_Tapped(object sender, EventArgs e)
    {
        await getLocation();
        await GetWeatherDataByLocation(latitude, longitude);
    }

    public async Task GetWeatherDataByLocation(double latitude, double longitude)
    {
        var result = await ApiService.getWeather(latitude, longitude);

        viewModel.CityName = result.name.ToString();
        var temperature = (int)(result.main.temp - 273.15);
        viewModel.CurrTemp = temperature.ToString() + "°C";
        viewModel.CurrFeel = result.weather.FirstOrDefault()?.main.ToString().ToUpper();

        viewModel.LblHumidity = result.main.humidity.ToString() + "%";
        viewModel.LblVisi = (result.visibility / 1000).ToString() + " Km";
        var wSpeed = (int)(result.wind.speed * 3600 / 1000);
        viewModel.LblWind = wSpeed.ToString() + " km/hr";

        viewModel.WeatherImage = "https://openweathermap.org/img/wn/" + result.weather.FirstOrDefault()?.icon.ToString() + "@2x.png";
    }

    public async Task GetWeatherDataByCity(string city)
    {
        var result = await ApiService.getWeatherCity(city);

        viewModel.CityName = result.name.ToString();
        var temperature = (int)(result.main.temp - 273.15);
        viewModel.CurrTemp = temperature.ToString() + "°C";
        viewModel.CurrFeel = result.weather.FirstOrDefault()?.main.ToString().ToUpper();

        viewModel.LblHumidity = result.main.humidity.ToString() + "%";
        viewModel.LblVisi = (result.visibility / 1000).ToString() + " Km";
        var wSpeed = (int)(result.wind.speed * 3600 / 1000);
        viewModel.LblWind = wSpeed.ToString() + " km/hr";

        viewModel.WeatherImage = "https://openweathermap.org/img/wn/" + result.weather.FirstOrDefault()?.icon.ToString() + "@2x.png";
    }

    public async void ImageBtn_Clicked(object sender, EventArgs e)
    {
        var response = await DisplayPromptAsync(title: "", message: "", placeholder: "Search by City", accept: "Search", cancel: "Cancel");
        if (response != null)
        {
            await GetWeatherDataByCity(response);
        }
    }
}
