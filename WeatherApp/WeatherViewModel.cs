using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WeatherApp.ViewModels
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private string _cityName;
        private string _currTemp;
        private string _currFeel;
        private string _lblHumidity;
        private string _lblVisi;
        private string _lblWind;
        private string _weatherImage;

        public string CityName
        {
            get => _cityName;
            set { _cityName = value; OnPropertyChanged(); }
        }

        public string CurrTemp
        {
            get => _currTemp;
            set { _currTemp = value; OnPropertyChanged(); }
        }

        public string CurrFeel
        {
            get => _currFeel;
            set { _currFeel = value; OnPropertyChanged(); }
        }

        public string LblHumidity
        {
            get => _lblHumidity;
            set { _lblHumidity = value; OnPropertyChanged(); }
        }

        public string LblVisi
        {
            get => _lblVisi;
            set { _lblVisi = value; OnPropertyChanged(); }
        }

        public string LblWind
        {
            get => _lblWind;
            set { _lblWind = value; OnPropertyChanged(); }
        }

        public string WeatherImage
        {
            get => _weatherImage;
            set { _weatherImage = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
