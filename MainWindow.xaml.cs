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
        private const string ApiKey = "f24e3b3b88dc280e84e540c4500113d5";
        private readonly HttpClient _httpClient;

        public MainWindow()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void BuscarPrevisaoClick(object sender, RoutedEventArgs evento)
        {
            string cidade = txtCidade.Text.Trim();

            try
            {
                string previsao = await ObterPrevisaoDoTempo(cidade);
                string previsaoDescricao = await ObterDescricaoDoCeu(cidade);
                txtCidade.Text = "";
                labelResultado.Text = previsao;
                labelTitulo.Text = previsaoDescricao;

                AtualizarBotaoConcluido((Button)sender, true);
            }
            catch (HttpRequestException ex)
            {
                string mensagemErro = ex.Message.Contains("Unauthorized") ? "Falha na API" : ex.Message.Contains("Not Found") ? "Cidade Inexistente" : "Campo Vazio";
                AtualizarBotaoConcluido((Button)sender, false, mensagemErro);

            }
        }

        private async Task<string> ObterPrevisaoDoTempo(string cidade)
        {
            string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={cidade}&appid={ApiKey}&units=metric&lang=pt";
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            Root cidadeEscolhida = JsonSerializer.Deserialize<Root>(json);

            double velocidadeVento = cidadeEscolhida.Vento.Velocidade;
            int umidade = cidadeEscolhida.Main.Umidade;

            return $"{cidadeEscolhida.Nome} - {cidadeEscolhida.Sys.Pais}\nTemperatura: {cidadeEscolhida.Main.Temperatura}°C\nSensação: {cidadeEscolhida.Main.SensacaoTermica}°C\nVento: {velocidadeVento} Km/h\nUmidade: {umidade}%";
        }

        private async Task<string> ObterDescricaoDoCeu(string cidade)
        {
            string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={cidade}&appid={ApiKey}&units=metric&lang=pt";
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            Root cidadeEscolhida = JsonSerializer.Deserialize<Root>(json);

            string titulo = cidadeEscolhida.Clima[0].Descricao.ToUpperInvariant();

            if (titulo.Contains("NUVENS QUEBRADAS"))
                titulo = "PARCIALMENTE NUBLADO";

            return titulo;
        }

        private void AtualizarBotaoConcluido(Button botao, bool sucesso, string mensagemErro = "")
        {
            Brush corFundo = sucesso ? new SolidColorBrush(Color.FromRgb(22, 120, 48)) : new SolidColorBrush(Color.FromRgb(145, 10, 10));
            botao.Background = corFundo;
            botao.Foreground = Brushes.Wheat;
            botao.Content = sucesso ? "Concluído" : mensagemErro;
        }

        private void Texto_SairComMouse(object sender, MouseEventArgs e)
        {
            ((Button)sender).Background = Brushes.WhiteSmoke;
            ((Button)sender).Foreground = Brushes.Black;
            ((Button)sender).Content = "Buscar Local";
        }
    }
}
