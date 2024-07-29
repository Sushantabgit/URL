using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp;

public partial class MainPage : ContentPage
{
	public double latitude;
	public double longitude;
	public MainPage()
	{
		InitializeComponent();
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

		cityName.Text = result.name.ToString();
		var temperature = (int)(result.main.temp - 273.15);
		currTemp.Text = temperature.ToString() + "°C";
		currFeel.Text = result.weather.FirstOrDefault()?.main.ToString().ToUpper();

		lblHumidity.Text = result.main.humidity.ToString() + "%";
		lblVisi.Text = (result.visibility / 1000).ToString() + " Km";
		var wSpeed = (int)(result.wind.speed * 3600 / 1000);
		lblwind.Text = wSpeed.ToString() + " km/hr";

		String iconUrl = "https://openweathermap.org/img/wn/" + result.weather.FirstOrDefault()?.icon.ToString() + "@2x.png";
		weatherImage.Source = iconUrl;
	}

	public async Task GetWeatherDataByCity(string city)
	{
		var result = await ApiService.getWeatherCity(city);

		cityName.Text = result.name.ToString();
		var temperature = (int)(result.main.temp - 273.15);
		currTemp.Text = temperature.ToString() + "°C";
		currFeel.Text = result.weather.FirstOrDefault()?.main.ToString().ToUpper();

		lblHumidity.Text = result.main.humidity.ToString() + "%";
		lblVisi.Text = (result.visibility / 1000).ToString() + " Km";
		var wSpeed = (int)(result.wind.speed * 3600 / 1000);
		lblwind.Text = wSpeed.ToString() + " km/hr";

		String iconUrl = "https://openweathermap.org/img/wn/" + result.weather.FirstOrDefault()?.icon.ToString() + "@2x.png";
		weatherImage.Source = iconUrl;
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