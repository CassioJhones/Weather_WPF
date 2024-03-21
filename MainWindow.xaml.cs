using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TimeVersion.Modelo;

namespace TimeVersion
{
    public partial class MainWindow : Window
    {
        private const string apiKey = "f24e3b3b88dc280e84e540c4500113d5";

        Color corVerdeSucesso = Color.FromRgb(22, 120, 48); //Verde 
        Color corVermelhoAviso = Color.FromRgb(145, 10, 10); //Vermelho

        public MainWindow() => InitializeComponent();

        private async void BuscarPrevisaoClick(object sender, RoutedEventArgs evento)
        {
            string campo = txtCidade.Text.Trim();
            try
            {
                string cidade = campo;
                string previsao = await ObterPrevisaoDoTempo(cidade);
                string previsao2 = await ObterDescricaoDoCeu(cidade);
                txtResultado.Text = $"{previsao}";
                txtTituloResultado.Text = $"{previsao2}";

                ((Button)sender).Content = "Concluido";

                Brush verdeClaro = new SolidColorBrush(corVerdeSucesso);
                ((Button)sender).Background = verdeClaro;
                ((Button)sender).Foreground = Brushes.Wheat;
            }
            catch (Exception erro)
            {
                Brush VermelhoClaro = new SolidColorBrush(corVermelhoAviso);
                ((Button)sender).Background = VermelhoClaro;
                ((Button)sender).Foreground = Brushes.Wheat;
                ((Button)sender).Content = erro.Message.Contains("Not Found") ? "Cidade Inexistente" : (object)"Campo Vazio";
            }
        }

        private async Task<string> ObterPrevisaoDoTempo(string cidade)
        {
            string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={cidade}&appid={apiKey}&units=metric&lang=pt";
            using (HttpClient chamada = new HttpClient())
            {
                HttpResponseMessage ResultChamada = await chamada.GetAsync(apiUrl);

                if (ResultChamada.IsSuccessStatusCode)
                {
                    string json = await ResultChamada.Content.ReadAsStringAsync();
                    Root cidadeEscolhida = JsonSerializer.Deserialize<Root>(json);

                    double tempCelcius = cidadeEscolhida.Main.Temp;
                    double veloKM = cidadeEscolhida.Wind.Speed;
                    double tempSensacao = cidadeEscolhida.Main.FeelsLike;
                    double tempMax = cidadeEscolhida.Main.TempMax;
                    double tempMin = cidadeEscolhida.Main.TempMin;
                    int umidade = cidadeEscolhida.Main.Humidity;
                    string nome = cidadeEscolhida.Name.ToString();
                    string OlhaCeu = cidadeEscolhida.Weather[0].Description.ToUpperInvariant();

                    return MostraNaTela(cidadeEscolhida, veloKM, umidade);
                }
                else throw new Exception($"Nao encontrado: {ResultChamada.ReasonPhrase}");
                
            }
        }

        private async Task<string> ObterDescricaoDoCeu(string cidade)
        {
            string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={cidade}&appid={apiKey}&units=metric&lang=pt";

            using (HttpClient chamada = new HttpClient())
            {
                HttpResponseMessage ResultChamada = await chamada.GetAsync(apiUrl);

                if (ResultChamada.IsSuccessStatusCode)
                {
                    string json = await ResultChamada.Content.ReadAsStringAsync();
                    Root cidadeEscolhida = JsonSerializer.Deserialize<Root>(json);

                    string OlhaCeu = cidadeEscolhida.Weather[0].Description.ToUpperInvariant();
                    return OlhaCeu;
                }
                else throw new Exception($"Nao encontrado: {ResultChamada.ReasonPhrase}");
                
            }
        }

        static string MostraNaTela(Root cidade2, double veloKM, int umidade)
        {
            string texto = $"Tempo Local - {cidade2.Name} \nOs ventos estão há {veloKM} Km/H" +
                $"\nTemperatura de {cidade2.Main.Temp}°C\nSensação de {cidade2.Main.FeelsLike}°C" +
                $"\nUmidade do ar: {umidade}%";

            return texto;
        }

        private void Texto_SairComMouse(object sender, MouseEventArgs e)
        {
            ((Button)sender).Content = "Buscar Clima";
            ((Button)sender).Background = Brushes.WhiteSmoke;
            ((Button)sender).Foreground = Brushes.Black;
        }
    }
}
