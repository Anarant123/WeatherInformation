using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WeatherInformation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnGetInfo_Click(object sender, RoutedEventArgs e)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={tbCity.Text}&units=metric&lang=ru&appid=347d205f6746c7f46eaccdf66b5b29f2";

            HttpClient httpClient = new HttpClient();
            using HttpResponseMessage response = await httpClient.GetAsync(url);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true // Установка свойства PropertyNameCaseInsensitive на true
                };

                WeatherResponce weatherResponce = JsonSerializer.Deserialize<WeatherResponce>(jsonResponse, options);

                tbInfo.Text = $"Сегодня на улице {weatherResponce.Weather.First().Description} \n" +
                    $"Температура {weatherResponce.Main.Temp} ℃\n" +
                    $"Ощущается как {weatherResponce.Main.FeelsLike} ℃\n" +
                    $"Влажность воздуха {weatherResponce.Main.Humidity} %\n" +
                    $"Атмосферное давление {weatherResponce.Main.Pressure} ГПа\n" +
                    $"Скорость ветра {weatherResponce.Wind.Speed} метр/сек";
            }
            else
            {
                tbInfo.Text = "Вы указали несуществующий город";
            }
        }
    }
}
