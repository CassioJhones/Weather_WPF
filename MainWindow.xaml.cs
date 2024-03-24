using System.ComponentModel;
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
    public partial class MainWindow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly HttpClient _httpClient;
        private string _apiStatus = "Online";
        public string ApiStatus
        {
            get { return _apiStatus; }
            set
            {
                if (_apiStatus != value)
                {
                    _apiStatus = value;
                    OnPropertyChanged(nameof(ApiStatus));
                }
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            DataContext = this;

            if (ApiKey.Length < 32 || string.IsNullOrEmpty(ApiKey))
                ApiStatus = "Offline";
            Loaded += MainWindow_Loaded;
        }
        
        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await CheckApiStatus();
        }
        protected virtual void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        
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
        private async Task CheckApiStatus()
        {
            HttpResponseMessage response = await GetApi();
            if (!response.IsSuccessStatusCode)
                ApiStatus = "Offline";
        }
        private async Task<HttpResponseMessage> GetApi()
        {
            string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q=brasil&appid={ApiKey}&units=metric&lang=pt";
            HttpResponseMessage response = null;

            try
            {
                response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                
            }

            return response;
        }

        private void Limpar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
